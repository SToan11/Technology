using ASM2.Areas.Admin.Models;
using System.Data.SqlClient;

namespace ASM2.Areas.Admin.DAL
{
    public class ProductDAL
    {
        private readonly string _connectionString;

        public ProductDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = new List<Product>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ProductId, Name, Category, Image, Description, Color, UnitPrice, AvailableQuantity FROM Product", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        ProductId = (int)reader["ProductId"],
                        Name = reader["Name"].ToString(),
                        Category = reader["Category"].ToString(),
                        Image = reader["Image"].ToString(),
                        Description = reader["Description"].ToString(),
                        Color = reader["Color"].ToString(),
                        UnitPrice = (double)reader["UnitPrice"],
                        AvailableQuantity = (int)reader["AvailableQuantity"]
                    });
                }
            }
            return products;
        }

        public void AddProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Product (Name, Category, Image, Description, Color, UnitPrice, AvailableQuantity) VALUES (@Name, @Category, @Image, @Description, @Color, @UnitPrice, @AvailableQuantity)", conn);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Category", product.Category);
                cmd.Parameters.AddWithValue("@Image", product.Image);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Color", product.Color);
                cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                cmd.Parameters.AddWithValue("@AvailableQuantity", product.AvailableQuantity);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Product SET Name = @Name, Category = @Category, Image = @Image, Description = @Description, Color = @Color, UnitPrice = @UnitPrice, AvailableQuantity = @AvailableQuantity WHERE ProductId = @ProductId", conn);
                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Category", product.Category);
                cmd.Parameters.AddWithValue("@Image", product.Image);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Color", product.Color);
                cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                cmd.Parameters.AddWithValue("@AvailableQuantity", product.AvailableQuantity);
                cmd.ExecuteNonQuery();
            }
        }

        public Product GetProductById(int ProductId)
        {
            Product product = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ProductId, Name, Category, Image, Description, Color, UnitPrice, AvailableQuantity FROM Product WHERE ProductId = @ProductId", conn);
                cmd.Parameters.AddWithValue("@ProductId", ProductId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product = new Product
                    {
                        ProductId = (int)reader["ProductId"],
                        Name = reader["Name"].ToString(),
                        Category = reader["Category"].ToString(),
                        Image = reader["Image"].ToString(),
                        Description = reader["Description"].ToString(),
                        Color = reader["Color"].ToString(),
                        UnitPrice = (double)reader["UnitPrice"],
                        AvailableQuantity = (int)reader["AvailableQuantity"]
                    };
                }
            }
            return product;
        }

        public void DeleteProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Product WHERE ProductId = @ProductId", conn))
                {
                    cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
