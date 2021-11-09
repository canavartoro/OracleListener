using OracleQueueService.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OracleQueueService.Data
{
    [DebuggerDisplay("IsConnected = {IsConnected},ConnectionString = {ConnectionString},SqlString = {SqlString}")]
    public class SqlClient : IDisposable
    {
        public const string CONNECTION_STRING = "packet size=4096;data source={0};persist security info=False;initial catalog={1};Connect Timeout=50;User={2};Password={3};Pooling=False;";
        private SqlCommand command = null;
        private SqlConnection connection = null;
        private string message = "";
        private string sqlString = string.Empty;
        private string connectionString = null;
        private Stopwatch stopwatch = null;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Message
        {
            get
            {
                return message;
            }
        }

        public bool IsConnected
        {
            get
            {
                return connection != null && connection.State == System.Data.ConnectionState.Open;
            }
        }

        public string SqlString
        {
            get { return sqlString; }
            set { sqlString = value; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public SqlClient() { }

        public SqlClient(string connstr)
        {
            connectionString = connstr;
            Connect();
        }

        public SqlClient(SqlConnection conn)
        {
            connection = conn;
            Connect();
        }

        public bool Connect()
        {
            try
            {
                if (connection == null)
                {
                    connection = new SqlConnection(connectionString);
                }
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                if (command == null)
                    command = connection.CreateCommand();

                return connection != null && connection.State == System.Data.ConnectionState.Open;
            }
            catch (SqlException sqlexc)
            {
                message = sqlexc.Message;
                Logger.E(sqlexc);
                return false;
            }
            catch (Exception exception)
            {
                message = exception.Message;
                Logger.E(exception);
                return false;
            }
        }

        public void Close()
        {
            try
            {
                if (command != null)
                {
                    if (command.Transaction != null)
                        command.Transaction.Rollback();
                    command.Dispose();
                }

                if (connection != null)
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();

                    connection.Dispose();
                }
            }
            catch (Exception exception)
            {
                message = exception.Message;
                Logger.E(exception);
            }
        }

        public SqlDataReader Select(string commandText, SqlParameter[] parameters)
        {
            sqlString = commandText;
            return Select(parameters);
        }
        public SqlDataReader Select(SqlParameter[] parameters)
        {
            try
            {
                if (!IsConnected) Connect();

                command.CommandText = sqlString;

                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                SqlStringLog();

                SqlDataReader reader = command.ExecuteReader();

                ExecutionLog(0);
                return reader;
            }
            catch (SqlException sqlexc)
            {
                message = sqlexc.Message;
                Logger.E(sqlexc);
            }
            catch (Exception exc)
            {
                message = exc.Message;
                Logger.E(exc);
            }
            finally
            {
                command.Parameters.Clear();
            }
            return null;
        }
        public SqlDataReader Select(string commandText)
        {
            SqlParameter[] parameters = null;
            SqlString = commandText;
            return Select(parameters);
        }
        public SqlDataReader Select()
        {
            return Select((SqlParameter[])null);
        }

        public List<T> Select<T>(string sql)
        {
            SqlString = sql;
            return Select<T>();
        }
        public List<T> Select<T>()
        {
            List<T> list = null;
            try
            {

                if (!IsConnected && !Connect()) return null;

                command.CommandText = SqlString;

                SqlStringLog();

                using (var dr = command.ExecuteReader())
                {
                    if (dr != null)
                    {
                        list = new List<T>();

                        var type = typeof(T);
                        var primitive = IsSimpleType(type);
                        var xmlschema = dr.GetSchemaTable();
                        while (dr.Read())
                        {
                            var newObject = default(T);
                            if (!primitive)
                            {
                                newObject = (T)Activator.CreateInstance(typeof(T));
                            }

                            if (xmlschema != null)
                            {
                                for (var column = 0; column < xmlschema.Rows.Count; column++)
                                {
                                    if (dr.IsDBNull(Convert.ToInt32(xmlschema.Rows[column]["ColumnOrdinal"])))
                                        continue;

                                    if (primitive)
                                    {
                                        if (type.IsAssignableFrom(dr
                                            .GetValue(Convert.ToInt32(xmlschema.Rows[column]["ColumnOrdinal"]))
                                            .GetType()))
                                        {
                                            newObject = (T)Convert.ChangeType(dr.GetValue(Convert.ToInt32(xmlschema.Rows[column]["ColumnOrdinal"])), type);
                                        }
                                    }
                                    else
                                    {
                                        var property = type.GetProperty(xmlschema.Rows[column]["ColumnName"].ToString());
                                        if (property != null)
                                        {
                                            try
                                            {
                                                property.SetValue(newObject, Convert.ChangeType(dr.GetValue(Convert.ToInt32(xmlschema.Rows[column]["ColumnOrdinal"])), property.PropertyType), null);
                                            }
                                            catch (Exception e)
                                            {
                                                Logger.E(e);
                                            }
                                        }
                                    }
                                }
                            }

                            list.Add(newObject);
                        }
                    }
                }
                //ExecutionLog(list?.Count ?? 0);
            }
            catch (SqlException DbException)
            {
                message = DbException.Message;
                Logger.E(DbException);
            }
            catch (Exception exc)
            {
                message = exc.Message;
                Logger.E(exc);
            }
            finally
            {
                ExecutionLog(list?.Count ?? 0);
                command?.Parameters.Clear();
            }
            return list;
        }

        public DataTable FillTable(string commandText, SqlParameter[] parameters)
        {
            sqlString = commandText;
            return FillTable(parameters);
        }
        public DataTable FillTable(SqlParameter[] parameters)
        {
            try
            {
                if (!IsConnected) Connect();

                command.CommandText = sqlString;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                SqlStringLog();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                ExecutionLog(table != null ? table.Rows.Count : 0);

                return table;
            }
            catch (SqlException sqlexc)
            {
                message = sqlexc.Message;
                Logger.E(sqlexc);
            }
            catch (Exception exc)
            {
                message = exc.Message;
                Logger.E(exc);
            }
            finally
            {
                command.Parameters.Clear();
            }
            return null;
        }
        public DataTable FillTable(string commandText)
        {
            SqlParameter[] parameters = null;
            sqlString = commandText;
            return FillTable(parameters);
        }
        public DataTable FillTable()
        {
            SqlParameter[] parameters = null;
            return FillTable(parameters);
        }

        public bool Execute(string commandText, SqlParameter[] parameters)
        {
            sqlString = commandText;
            return Execute(parameters);
        }
        public bool Execute(SqlParameter[] parameters)
        {
            try
            {
                if (!IsConnected) Connect();

                command.CommandText = sqlString;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                SqlStringLog();

                int rowaffected = command.ExecuteNonQuery();

                ExecutionLog(rowaffected);

                return rowaffected > 0;
            }
            catch (SqlException sqlexc)
            {
                message = sqlexc.Message;
                Logger.E(sqlexc);
            }
            catch (Exception exc)
            {
                message = exc.Message;
                Logger.E(exc);
            }
            finally
            {
                command.Parameters.Clear();
            }
            return false;
        }
        public bool Execute(string commandText)
        {
            SqlParameter[] parameters = null;
            sqlString = commandText;
            return Execute(parameters);
        }
        public bool Execute()
        {
            SqlParameter[] parameters = null;
            return Execute(parameters);
        }

        public object ExecuteScalar(string commandText, SqlParameter[] parameters)
        {
            sqlString = commandText;
            return ExecuteScalar(parameters);
        }
        public object ExecuteScalar(SqlParameter[] parameters)
        {
            try
            {
                if (!IsConnected) Connect();

                command.Parameters.Clear();
                command.CommandText = sqlString;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                SqlStringLog();
                object scalar = command.ExecuteScalar();

                ExecutionLog(scalar != null ? 1 : 0);

                return scalar;
            }
            catch (SqlException sqlexc)
            {
                message = sqlexc.Message;
                Logger.E(sqlexc);
            }
            catch (Exception exc)
            {
                message = exc.Message;
                Logger.E(exc);
            }
            finally
            {
                command.Parameters.Clear();
            }
            return null;
        }
        public object ExecuteScalar(string commandText)
        {
            sqlString = commandText;
            return ExecuteScalar();
        }
        public object ExecuteScalar()
        {
            SqlParameter[] parameters = null;
            return ExecuteScalar(parameters);
        }

        public int Count(string sql)
        {
            object obj = ExecuteScalar(sql, null);
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            return -1;
        }

        #region Transaction
        [DebuggerStepThrough()]
        public void Begin()
        {
            try
            {
                if (!IsConnected) Connect();

                command.Transaction = connection.BeginTransaction();
            }
            catch (SqlException sqlexc)
            {
                message = sqlexc.Message;
                Logger.E(sqlexc);
            }
            catch (Exception exc)
            {
                message = exc.Message;
                Logger.E(exc);
            }
        }

        [DebuggerStepThrough()]
        public void Commit()
        {
            try
            {
                if (command != null && command.Transaction != null)
                {
                    command.Transaction.Commit();
                }
            }
            catch (SqlException dbexc)
            {
                message = dbexc.Message;
                Logger.E(dbexc);
            }
            catch (Exception exc)
            {
                message = exc.Message;
                Logger.E(exc);
            }
        }

        [DebuggerStepThrough()]
        public void Rollback()
        {
            try
            {
                if (command != null && command.Transaction != null)
                {
                    command.Transaction.Rollback();
                }
            }
            catch (SqlException dbexc)
            {
                message = dbexc.Message;
                Logger.E(dbexc);
            }
            catch (Exception exc)
            {
                message = exc.Message;
                Logger.E(exc);
            }
        }
        #endregion

        #region Parameters
        [DebuggerStepThrough()]
        public void AddParam(string name, object val)
        {
            try
            {
                if (!IsConnected) Connect();

                command.Parameters.AddWithValue(name, val);
            }
            catch (SqlException dbexc)
            {
                message = dbexc.Message;
                Logger.E(dbexc);
            }
            catch (Exception exc)
            {
                message = exc.Message;
                Logger.E(exc);
            }
        }

        [DebuggerStepThrough()]
        public void AddParam(string name, object val, System.Data.DbType dbType, ParameterDirection direction)
        {
            try
            {
                if (!IsConnected) Connect();

                command.Parameters.Add(new SqlParameter() { ParameterName = name, Value = val, DbType = dbType, Direction = direction });

            }
            catch (SqlException dbexc)
            {
                message = dbexc.Message;
                Logger.E(dbexc);
            }
            catch (Exception exc)
            {
                message = exc.Message;
                Logger.E(exc);
            }
        }

        [DebuggerStepThrough()]
        public void ClearParameters()
        {
            try
            {
                if (!IsConnected) Connect();

                command.Parameters.Clear();
            }
            catch (SqlException dbexc)
            {
                message = dbexc.Message;
                Logger.E(dbexc);
            }
            catch (Exception exc)
            {
                message = exc.Message;
                Logger.E(exc);
            }
        }
        #endregion

        public static implicit operator bool(SqlClient sql)
        {
            return sql != null && sql.IsConnected;
        }

        [DebuggerStepThrough()]
        private void SqlStringLog([CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0)
        {
            stopwatch = Stopwatch.StartNew();
            if (command != null)
            {
                if (command.Parameters.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (IDataParameter prm in command.Parameters) sb.AppendFormat("\tName:{0},Value:{1}\t", prm.ParameterName, prm.Value);
                    System.Diagnostics.Trace.WriteLine(string.Concat("Command:", command.CommandText, "\tParameters:", sb.ToString(), ", Caller: ", callerName, ", lineNumber : ", lineNumber));
                }
                else
                {
                    System.Diagnostics.Trace.WriteLine(string.Concat("Command:", command.CommandText, ", Caller: ", callerName, ", lineNumber : ", lineNumber));

                }
            }
        }

        [DebuggerStepThrough()]
        private void ExecutionLog(int rowaffected, [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0)
        {
            stopwatch.Stop();
            System.Diagnostics.Trace.WriteLine(string.Concat("Execution:", stopwatch.Elapsed.TotalSeconds, ",Rows:", rowaffected, ", Caller: ", callerName, ", lineNumber : ", lineNumber));
        }

        [DebuggerStepThrough()]
        private bool IsSimpleType(Type type)
        {
            return
                type.IsPrimitive ||
                ((IList)new[]
                {
                    typeof(string),
                    typeof(decimal),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan),
                    typeof(Guid)
                }).Contains(type) ||
                type.IsEnum ||
                Convert.GetTypeCode(type) != TypeCode.Object ||
                (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && IsSimpleType(type.GetGenericArguments()[0]))
                ;
        }

        #region IDisposable
        ~SqlClient()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if (command != null)
                {
                    command.Dispose();
                }

                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }

                command = null;
                connection = null;
            }

            disposed = true;
        }

        #endregion
    }
}
