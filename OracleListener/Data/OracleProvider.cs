using Oracle.ManagedDataAccess.Client;
using OracleListener.Log;
using OracleListener.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OracleListener.Data
{
    public class OracleProvider : IDisposable
    {
        public const string CONNECTION_STRING = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={2})));User Id=uyumsoft;Password=uyumsoft";
        private OracleCommand command = null;
        private OracleConnection connection = null;
        private string message = "";
        private string sqlString = string.Empty;

        public OracleConnection Connection => connection;
        public OracleCommand Command => command;

        public string Message => message;

        public bool IsConnected => connection != null && connection.State == System.Data.ConnectionState.Open;

        public string SqlString
        {
            get { return sqlString; }
            set { sqlString = value; }
        }

        public OracleProvider() { }

        public bool Connect()
        {
            try
            {
                if (connection == null)
                {
                    connection = new OracleConnection(AppConfig.Default.GetOracleConnectionString());
                }
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                if (command == null)
                    command = connection.CreateCommand();

                return connection != null && connection.State == System.Data.ConnectionState.Open;
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
                    command.Transaction?.Rollback();
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

        public OracleDataReader Select(string commandText, OracleParameter[] parameters)
        {
            sqlString = commandText;
            return Select((OracleParameter[])parameters);
        }
        public OracleDataReader Select(OracleParameter[] parameters)
        {
            try
            {
                if (!IsConnected && Connect()) return null;

                command.CommandText = sqlString;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                SqlStringLog();

                return command.ExecuteReader();
            }
            catch (Exception exc)
            {
                Logger.E(exc);
            }
            finally
            {
                command?.Parameters.Clear();
            }
            return null;
        }
        public OracleDataReader Select(string commandText)
        {
            sqlString = commandText;
            return Select((OracleParameter[])null);
        }
        public OracleDataReader Select()
        {
            return Select((OracleParameter[])null);
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
            catch (OracleException DbException)
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
                //ExecutionLog(list?.Count ?? 0);
                command?.Parameters.Clear();
            }
            return list;
        }

        public DataTable FillTable(string commandText, DbParameter[] parameters)
        {
            sqlString = commandText;
            return FillTable((OracleParameter[])parameters);
        }
        public DataTable FillTable(OracleParameter[] parameters)
        {
            try
            {
                if (!IsConnected && !Connect()) return null;

                command.CommandText = sqlString;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                SqlStringLog();

                OracleDataAdapter adapter = new OracleDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                return table;
            }
            catch (Exception exc)
            {
                Logger.E(exc);
            }
            finally
            {
                command?.Parameters.Clear();
            }
            return null;
        }
        public DataTable FillTable(string commandText)
        {
            sqlString = commandText;
            return FillTable((OracleParameter[])null);
        }
        public DataTable FillTable()
        {
            return FillTable((OracleParameter[])null);
        }

        public bool Execute(string commandText, DbParameter[] parameters)
        {
            sqlString = commandText;
            return Execute((OracleParameter[])parameters);
        }
        public bool Execute(OracleParameter[] parameters)
        {
            try
            {
                if (!IsConnected && !Connect()) return false;

                command.CommandText = sqlString;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                SqlStringLog();
                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception exc)
            {
                Logger.E(exc);
            }
            finally
            {
                command?.Parameters.Clear();
            }
            return false;
        }
        public bool Execute(string commandText)
        {
            sqlString = commandText;
            return Execute((OracleParameter[])null);
        }
        public bool Execute()
        {
            return Execute((OracleParameter[])null);
        }

        public object ExecuteScalar(string commandText, DbParameter[] parameters)
        {
            sqlString = commandText;
            return ExecuteScalar((OracleParameter[])parameters);
        }
        public object ExecuteScalar(OracleParameter[] parameters)
        {
            try
            {
                if (!IsConnected && !Connect()) return null;

                command.CommandText = sqlString;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                SqlStringLog();
                return command.ExecuteScalar();
            }
            catch (Exception exc)
            {
                Logger.E(exc);
            }
            finally
            {
                command?.Parameters.Clear();
            }
            return null;
        }
        public object ExecuteScalar(string commandText)
        {
            sqlString = commandText;
            return ExecuteScalar((OracleParameter[])null);
        }
        public object ExecuteScalar()
        {
            return ExecuteScalar((OracleParameter[])null);
        }

        public object ExecuteProc(string name, List<OracleParameter> paramaters)
        {
            try
            {
                if (!IsConnected && !Connect()) return null;

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = name;
                if (paramaters != null && paramaters.Count > 0)
                    command.Parameters.AddRange(paramaters.ToArray());

                SqlStringLog();
                return command.ExecuteScalar();
            }
            catch (Exception exc)
            {
                Logger.E(exc);
            }
            finally
            {
                command?.Parameters.Clear();
            }
            return null;
        }

        #region Transaction
        public void Begin()
        {
            try
            {
                if (!IsConnected && !Connect()) return;

                command.Transaction = connection.BeginTransaction();
            }
            catch (DbException dbexc)
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

        public void Commit()
        {
            try
            {
                if (command != null && command.Transaction != null)
                {
                    command.Transaction.Commit();
                }
            }
            catch (DbException dbexc)
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

        public void Rollback()
        {
            try
            {
                if (command != null && command.Transaction != null)
                {
                    command.Transaction.Rollback();
                }
            }
            catch (DbException dbexc)
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
        public void AddParam(string name, object val)
        {
            try
            {
                if (!IsConnected && !Connect()) return;

                OracleParameter param1 = command.CreateParameter();
                param1.ParameterName = name;
                param1.Value = val;
                command.Parameters.Add(param1);
            }
            catch (DbException dbexc)
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
        public void AddParam(string name, object val, ParameterDirection direction)
        {
            try
            {
                if (!IsConnected && !Connect()) return;

                IDbDataParameter param1 = command.CreateParameter();
                param1.ParameterName = name;
                param1.Value = val;
                param1.Direction = direction;
                command.Parameters.Add(param1);
            }
            catch (DbException dbexc)
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
        public void AddParam(string name, System.Data.DbType dbType, ParameterDirection direction)
        {
            try
            {
                if (!IsConnected && !Connect()) return;

                command.Parameters.Add(new OracleParameter() { ParameterName = name, DbType = dbType, Direction = direction });
            }
            catch (DbException dbexc)
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
        public void ClearParameters()
        {
            try
            {
                if (!IsConnected && !Connect()) return;

                command.Parameters.Clear();
            }
            catch (DbException dbexc)
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

        public static implicit operator bool(OracleProvider ora)
        {
            return ora != null && ora.IsConnected;
        }

        [DebuggerStepThrough]
        private void SqlStringLog([CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0)
        {
            if (command != null)
            {
                if (command.Parameters.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (IDataParameter prm in command.Parameters) sb.AppendFormat("\tName:{0},Value:{1}\t", prm.ParameterName, prm.Value);
                    Logger.I(string.Concat(callerName, "(", lineNumber, ")\t", "Command:", command.CommandText, "\tParameters:", sb.ToString()));
                }
                else
                {
                    Logger.I(string.Concat(callerName, "(", lineNumber, ")\t", "Command:", command.CommandText));

                }
            }

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
        ~OracleProvider()
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
                OracleConnection.ClearAllPools();
            }

            disposed = true;
        }
        #endregion
    }
}
