using ASM2.Areas.Admin.Models;
using System.Data.SqlClient;

namespace ASM2.Areas.Admin.DAL
{
    public class CustomersDAL
    {
        private readonly string _connectionString;

        public CustomersDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public IEnumerable<Customers> GetAllCustomers()
        {
            var customers = new List<Customers>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT CustomerId, Email, Password FROM Customers", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(new Customers
                    {
                        CustomerId = (int)reader["CustomerId"],
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString(),
                    });
                }
            }
            return customers;
        }

        public void AddCustomer(Customers customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Customers (Email, Password) VALUES (@Email, @Password)", conn);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Password", customer.Password);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateCustomer(Customers customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Customers SET Email = @Email, Password = @Password WHERE CustomerId = @CustomerId", conn);
                cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Password", customer.Password);
                cmd.ExecuteNonQuery();
            }
        }

        public Customers GetCustomerById(int customerId)
        {
            Customers customer = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT CustomerId, Email, Password FROM Customers WHERE CustomerId = @CustomerId", conn);
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    customer = new Customers
                    {
                        CustomerId = (int)reader["CustomerId"],
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString(),
                    };
                }
            }
            return customer;
        }

        public void DeleteCustomer(Customers customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Customers WHERE CustomerId = @CustomerId", conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
