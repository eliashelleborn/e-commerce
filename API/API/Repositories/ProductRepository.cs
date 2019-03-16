using System.Collections.Generic;
using System.Linq;
using API.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace API.Repositories
{
    public class ProductRepository
    {
        private readonly string connectionString;

        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        
        public List<Product> Get(){
            using (var connection = new MySqlConnection(connectionString))
            {
                return connection.Query<Product>("SELECT * FROM Product").ToList();
            }
        }

        public Product Get(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                return connection.QuerySingleOrDefault<Product>("SELECT * FROM Product WHERE Id = @id", new { id });
            }
        }

        public void Add(Product Product)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Execute("INSERT INTO Product (name, description, price, stock, image) VALUES(@name, @description, @price, @stock, @image)", Product);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Execute("DELETE FROM Product WHERE id=@id", new { id });
            }
        }
    }
}