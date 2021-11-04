using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleListener.Data
{
    public class DataSynchronization : OracleProvider
    {
        public DataSynchronization()
        {
            sqlClient = new SqlClient();
        }

        private SqlClient sqlClient = null;

        public void StokSynchronization(string stok)
        {

        }

        #region IDisposable
        ~DataSynchronization()
        {
            Dispose(false);
        }


        private bool disposed = false;

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                sqlClient?.Dispose();
            }

            sqlClient = null;
            disposed = true;
        }
        #endregion
    }
}
