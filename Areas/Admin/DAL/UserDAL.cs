using ASM2.Areas.Admin.Models;
using System.Data.SqlClient;

namespace ASM2.Areas.Admin.DAL
{
    public class UserDAL
    {
        private readonly string _connectionString;

        public UserDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public IEnumerable<User> GetAllUsers()
        {
            var users = new List<User>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Name, Email, Password, Role, CreateDate FROM Account", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString(),
                        Role = reader["Role"].ToString(),
                        CreateDate = (DateTime)reader["CreateDate"]
                    });
                }
            }
            return users;
        }
        public void AddUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Account (Name, Email, Password, Role, CreateDate) VALUES (@Name, @Email, @Password, @Role, @CreateDate)", conn);

                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.AddWithValue("@CreateDate", user.CreateDate);

                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Account SET Name = @Name, Email = @Email, Password = @Password, Role = @Role, CreateDate = @CreateDate WHERE Id = @Id", conn);

                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.AddWithValue("@CreateDate", user.CreateDate);

                cmd.ExecuteNonQuery();
            }
        }
        public User GetUserById(int userId)
        {
            User user = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Name, Email, Password, Role, CreateDate FROM Account WHERE Id = @Id", conn);

                cmd.Parameters.AddWithValue("@Id", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString(),
                        Role = reader["Role"].ToString(),
                        CreateDate = (DateTime)reader["CreateDate"]
                    };
                }
            }
            return user;
        }
        public void DeleteUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Account WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", user.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }



    }
}
