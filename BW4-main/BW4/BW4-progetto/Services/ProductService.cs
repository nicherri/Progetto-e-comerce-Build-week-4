using Dapper;
using BW4_progetto.Models;

namespace BW4_progetto.Services
{
    public class ProductService
    {
        private readonly DatabaseService _databaseService;

        public ProductService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            using (var connection = _databaseService.GetConnection())
            {
                return connection.Query<Product>("SELECT * FROM Products").ToList();
            }
        }

        public Product GetProductById(int id)
        {
            using (var connection = _databaseService.GetConnection())
            {
                return connection.QuerySingleOrDefault<Product>("SELECT * FROM Products WHERE ProductId = @Id", new { Id = id });
            }
        }

        public void AddProduct(Product product)
        {
            using (var connection = _databaseService.GetConnection())
            {
                var sql = "INSERT INTO Products (Name, Description, Price, ImageUrl) VALUES (@Name, @Description, @Price, @ImageUrl)";
                connection.Execute(sql, product);
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var connection = _databaseService.GetConnection())
            {
                var sql = "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price, ImageUrl = @ImageUrl WHERE ProductId = @ProductId";
                connection.Execute(sql, product);
            }
        }

        public void DeleteProduct(int id)
        {
            using (var connection = _databaseService.GetConnection())
            {
                connection.Execute("DELETE FROM Products WHERE ProductId = @Id", new { Id = id });
            }
        }
    }
}
