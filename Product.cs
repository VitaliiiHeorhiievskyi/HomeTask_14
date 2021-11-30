using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace HomeTask_14
{
    public class Product : IComparable<Product>
    {

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Name is empty");
                name = value;
            }
        }

        private double weight;
        public double Weight
        {
            get { return weight; }
            set
            {
                if (value > 0)
                    weight = value;
                else throw new ArgumentNullException("The weight must be >0!!!");
            }
        }

        private double price;
        public double Price
        {
            get { return price; }
            set
            {
                if (value >= 0)
                    price = value;
                else throw new ArgumentNullException("The price cannot be negative!");
            }
        }
        public int ExpirationDate { get; set; }


        private DateTime dateOfManufacture;

        public DateTime DateOfManufacture
        {
            get
            {
                return dateOfManufacture;
            }
            set
            {
                dateOfManufacture = value;
            }
        }

        public static Product Create(IDataRecord data)
        {
            string name = data["NAME"].ToString();

            bool result = double.TryParse(data["WEIGHT"].ToString(), out double weight);
            if (!result)
            {
                return null;
            }
            result = double.TryParse(data["PRICE"].ToString(), out double price);
            if (!result)
            {
                return null;
            }
            result = int.TryParse(data["EXPIRATION"].ToString(), out int expirationDate);
            if (!result)
            {
                return null;
            }
            result = DateTime.TryParseExact(data["DATEOFMANUFACTURE"].ToString().Substring(0, 10), "dd.MM.yyyy", new CultureInfo(3), DateTimeStyles.None, out DateTime dateOfManufacture);
            if (!result)
            {
                return null;
            }
            return new Product(name, weight, price, expirationDate, dateOfManufacture);
        }

        public bool TryParse(string line)
        {
            if (string.IsNullOrEmpty(line))
                return false;

            string[] data = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length != 5)
                return false;


            Name = data[0];

            bool result = double.TryParse(data[1], out weight);
            if (!result)
            {
                //OnWrongInput?.Invoke(@"D:\Users\vital\source\repos\HomeTask2\log.txt", line, 1);
                return false;
            }
            result = double.TryParse(data[2], out price);
            if (!result)
            {
                //OnWrongInput?.Invoke(@"D:\Users\vital\source\repos\HomeTask2\log.txt", line, 2);
                return false;
            }
            result = int.TryParse(data[3], out int expirationDate);
            if (!result)
            {
                //OnWrongInput?.Invoke(@"D:\Users\vital\source\repos\HomeTask2\log.txt", line, 3);
                return false;
            }
            result = DateTime.TryParseExact(data[4].Substring(0, 10), "dd.MM.yyyy", new CultureInfo(3), DateTimeStyles.None, out dateOfManufacture);
            if (!result)
            {
                //OnWrongInput?.Invoke(@"D:\Users\vital\source\repos\HomeTask2\log.txt", line, 4);
                return false;
            }
            ExpirationDate = expirationDate;
            return true;
        }

        public bool IsSpoiledProduct()
        {
            return DateOfManufacture.AddDays(ExpirationDate) < DateTime.Now;
        }


        public Product()
        {

        }
        public Product(string name, double weight, double price, int expirationDate, DateTime dateOfManifecture)
        {
            Name = name;
            Weight = weight;
            Price = price;
            ExpirationDate = expirationDate;
            DateOfManufacture = dateOfManifecture;

        }

        public Product(Product product)
        {
            Name = product?.Name ?? "Data not entered!!";
            Weight = product.Weight;
            Price = product.Price;
            ExpirationDate = product.ExpirationDate;
            DateOfManufacture = product.DateOfManufacture;
        }

        public override string ToString()
        {
            return $"\nName: {this?.Name ?? "Data not entered!!"}  \nWeight: {weight}g \nPrice: { Price} UAH\n" +
                $"Exparation date: {ExpirationDate}\nDate of manufacture: {DateOfManufacture.ToShortDateString()}\n";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Product);
        }

        public bool Equals(Product other)
        {
            return other != null && Name == other.Name;
        }

        public int CompareTo(Product other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
