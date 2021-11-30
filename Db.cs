using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HomeTask_14
{
    class Db
    {
        public SqliteConnection connection;
        public Db() { }
        public Db(string connectionString)
        {
            connection = new SqliteConnection(connectionString);
            connection.Open();
        }

        public void ExecuteSQL(string sql)
        {
            using (connection)
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = sql;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public List<T> GetList<T>(string sql, Func<IDataRecord, T> generator)
        {
            var list = new List<T>();
            using (connection)
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sql, connection);
                try
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            list.Add(generator(reader));
                        }

                    }

                }
                catch
                {
                    return null;
                }
            }



            return list;
        }

    }

    static class DBHelper //extensions
    {
        public static List<Product> GetProductList(this Db db)
        {
            return db.GetList<Product>("select * from PRODUCTS", Product.Create);
        }
        //linq
        public static Product GetProductByName(this Db db, string name)
        {
            return db.GetList<Product>("select * from PRODUCTS", Product.Create).Where(i => i.Name == name).FirstOrDefault();
        }
        //sql
        public static Product GetProduct(this Db db, string name)
        {
            return db.GetList<Product>($"select * from PRODUCTS where Name like '{name}%' limit 1", Product.Create).FirstOrDefault();
        }

    }
}
