using ASM2.Areas.Admin.Models;
using System.Data.SqlClient;

namespace ASM2.Areas.Admin.DAL
{
    public class AccountDAL
    {
        private readonly string _connectionString;

        public AccountDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Account Login(string username, string password)
        {
            Account account = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Account WHERE Name=@Name AND Password=@Password", conn);
                cmd.Parameters.AddWithValue("@Name", username);
                cmd.Parameters.AddWithValue("@Password", password);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    account = new Account
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Email = (string)reader["Email"],
                        Password = (string)reader["Password"],
                        Role = (string)reader["Role"],
                        CreateDate = (DateTime)reader["CreateDate"]
                    };
                }
            }
            return account;
        }
    }
}
