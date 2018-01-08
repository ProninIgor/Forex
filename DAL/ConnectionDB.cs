using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DAL
{
    public class ConnectionDb : IDisposable
    {
        private SqlConnection sqlConnection;
        public IEnumerable<CurrencyPoco> GetAllCurrencies()
        {
            var connection = GetDbConnection();
            return connection.Query<CurrencyPoco>("select * from dbo.Currencies");
        }

        public void Insert(CurrencyPoco item)
        {
            GetDbConnection().Insert(item);
        }

        public void Insert(List<CurrencyPoco> items)
        {
            GetDbConnection().Insert(items);
        }

        public void Insert<T>(T item)
            where T : class 
        {
            GetDbConnection().Insert<T>(item);
        }

        public void Insert<T>(List<T> items)
            where T : class
        {
            GetDbConnection().Insert<List<T>>(items);
        }

        public void Update<T>(T item)
           where T : class
        {
            GetDbConnection().Update<T>(item);
        }

        public void Update<T>(List<T> items)
            where T : class
        {
            GetDbConnection().Update<List<T>>(items);
        }

        public IEnumerable<T> GetAll<T>()
            where T : class
        {
            IDbConnection dbConnection = GetDbConnection();
            IEnumerable<T> enumerable = dbConnection.GetAll<T>();
            dbConnection.Close();
            return enumerable;
        }

        private IDbConnection GetDbConnection()
        {
            if (this.sqlConnection != null)
                return this.sqlConnection;

            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "family\\sqlexpress";
            scsb.InitialCatalog = "ExcangeDB";
            scsb.IntegratedSecurity = true;
            //scsb.UserID = "sa";
            //scsb.Password = "123456";

            this.sqlConnection = new SqlConnection(scsb.ConnectionString);
            return this.sqlConnection;
        }

        public void Dispose()
        {
            sqlConnection.Close();
        }
    }
}
