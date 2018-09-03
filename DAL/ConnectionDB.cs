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
    /// <summary>
    /// Класс для работы с БД
    /// </summary>
    public class ConnectionDb : IDisposable
    {
        /// <summary>
        /// IDbConnection в буфере
        /// </summary>
        private SqlConnection sqlConnection;

        /// <summary>
        /// Вставка одного элемента в БД
        /// </summary>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        public void Insert<T>(T item)
            where T : class 
        {
            GetDbConnection().Insert<T>(item);
        }

        /// <summary>
        /// Вставка нескольких элементов в БД
        /// </summary>
        /// <param name="items"></param>
        /// <typeparam name="T"></typeparam>
        public void Insert<T>(List<T> items)
            where T : class
        {
            GetDbConnection().Insert<List<T>>(items);
        }

        /// <summary>
        /// Обновление одного элемента в БД
        /// </summary>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        public void Update<T>(T item)
           where T : class
        {
            GetDbConnection().Update<T>(item);
        }

        /// <summary>
        /// Обновление нескольких элементов в БД
        /// </summary>
        /// <param name="items"></param>
        /// <typeparam name="T"></typeparam>
        public void Update<T>(List<T> items)
            where T : class
        {
            GetDbConnection().Update<List<T>>(items);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetAll<T>()
            where T : class
        {
            IDbConnection dbConnection = GetDbConnection();
            IEnumerable<T> enumerable = dbConnection.GetAll<T>();
            dbConnection.Close();
            return enumerable;
        }

        /// <summary>
        /// Получить IDbConnection для основной базы
        /// </summary>
        /// <returns></returns>
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
