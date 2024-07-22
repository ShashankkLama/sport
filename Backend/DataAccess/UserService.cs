
using BlogApplication.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace BlogApplication.DataAccess
{
    public class UserService
    {
        private readonly string _connectionString;

        public UserService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public void InsertUser(User user)
        {
            string connectionString = "Data Source=DESKTOP-TSORS7M\\SQLEXPRESS;Initial Catalog=blogdb;User ID=sa;Password=root;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Username, Email, Password, Name, Role) VALUES (@Username, @Email, @Password, @Name, @Role)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password); // Note: In a real application, hash the password before storing it.
                command.Parameters.AddWithValue("@Name", user.FullName);
                command.Parameters.AddWithValue("@Role", user.Role.ToString()); // Convert UserRole enum to string for storage

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public User GetUserByEmailAndPassword(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            }

            User user = null;
            string query = "SELECT UserID, Username, Email, Password, Name, Role FROM Users WHERE Email = @Email AND Password = @Password";
            string connectionString = "Data Source=DESKTOP-TSORS7M\\SQLEXPRESS;Initial Catalog=blogdb;User ID=sa;Password=root;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password); // Note: In a real application, hash the password before comparing.

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        user = new User
                        {
                            UserID = Convert.ToInt32(reader["UserID"]),
                            Username = reader["Username"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString(), // Note: You may not need to fetch password in real scenarios.
                            FullName = reader["Name"].ToString()
                        };

                        // Parse role enum from string
                        if (Enum.TryParse(reader["Role"].ToString(), out UserRole role))
                        {
                            user.Role = role;
                        }
                        else
                        {
                            // Log or handle invalid role value
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle exceptions as needed
                    Console.WriteLine(ex.Message);
                }
            }

            return user;
        }


        public UserProfileModel GetUserProfileById(int userId)
        {
            UserProfileModel userProfile = null;
            string connectionString = "Data Source=DESKTOP-TSORS7M\\SQLEXPRESS;Initial Catalog=blogdb;User ID=sa;Password=root;TrustServerCertificate=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Email, Name, Role FROM Users WHERE UserID = @UserId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    userProfile = new UserProfileModel
                    {
                        Email = reader["Email"].ToString(),
                       
                    };
                }

                reader.Close();
            }

            return userProfile;
        }
        public void UpdateUserPassword(User user)
        {
            string connectionString = "Data Source=DESKTOP-TSORS7M\\SQLEXPRESS;Initial Catalog=blogdb;User ID=sa;Password=root;TrustServerCertificate=True;";
            string query = "UPDATE Users SET Password = @Password WHERE UserID = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@UserId", user.UserID);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if the update operation was successful
                    if (rowsAffected > 0)
                    {
                    Console.WriteLine("Password updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update password.");
                        // You can throw an exception here or handle the failure accordingly
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions as needed
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public User GetUserByEmail(string email)
        {
            User user = null;
            string connectionString = "Data Source=DESKTOP-TSORS7M\\SQLEXPRESS;Initial Catalog=blogdb;User ID=sa;Password=root;TrustServerCertificate=True;";
            string query = "SELECT UserID, Username, Email, Password, Name FROM Users WHERE Email = @Email";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        user = new User
                        {
                            UserID = Convert.ToInt32(reader["UserID"]),
                            Username = reader["Username"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString(),
                            FullName = reader["Name"].ToString()
                        };
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle exceptions as needed
                    Console.WriteLine(ex.Message);
                }
            }

            return user;
        }
       


    }
}
