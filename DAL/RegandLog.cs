using ASM2.Models;
using Microsoft.Data.SqlClient;

namespace ASM2.DAL
{
    public class RegandLog
    {
        private readonly string _connectionString;

        public RegandLog(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public bool Register(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                // Check if the email already exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Customers WHERE Email = @Email", conn);
                checkCmd.Parameters.AddWithValue("@Email", email);
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    return false; // User already exists
                }
                // Add new user
                SqlCommand cmd = new SqlCommand("INSERT INTO Customers (Email, Password) VALUES (@Email, @Password)", conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.ExecuteNonQuery();
                return true; // Registration successful
            }
        }

        public User Login(string email, string password)
        {
            User user = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Customers WHERE Email = @Email AND Password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                Email = reader["Email"].ToString(),
                                Pass = reader["Password"].ToString()
                            };
                        }
                    }
                }
            }
            return user;
        }
    }
}