using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Globalization;

namespace HomeTask_14
{
    class Program
    {
        static void Main(string[] args)
        {

            Db db = new Db("Data Source=MyDb.db");

            //db.ExecuteSQL("INSERT INTO PRODUCTS(NAME,PRICE,WEIGHT,EXPIRATION,DATEOFMANUFACTURE) VALUES('Tomato', 20.00, 1000.00, 12, '21.10.2021')");

            Console.WriteLine("List");
            foreach (var product in db.GetProductList())
            {
                Console.WriteLine(product);
            }

            Console.WriteLine(db.GetProduct("LEMON"));


        }
    }
}
