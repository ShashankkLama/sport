using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BlogApplication.Models; // Add this line if the Blog class is in the BlogApplication.Models namespace

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BlogApplication.DataAccess
{
    public class BlogService
    {
        // Connection string to your database
        private readonly string _connectionString;
        string connectionString = "Data Source=DESKTOP-TSORS7M\\SQLEXPRESS;Initial Catalog=blogdb;User ID=sa;Password=root;TrustServerCertificate=True;";
        public BlogService(string connectionString)
        {
            _connectionString = connectionString;
        }


        public List<Blog> GetBlogData()
        {
            List<Blog> blogs = new List<Blog>();

            // SQL query to select required columns from the Blogs table
            string query = "SELECT [Title], [Body], [PublishedDate], [PictureColumn] FROM [blogdb].[dbo].[Blogs]";

            // Open connection and execute query
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Read blog data from the database
                while (reader.Read())
                {
                    Blog blog = new Blog
                    {
                        Title = reader["Title"].ToString(),
                        Body = reader["Body"].ToString(),
                        PublishedDate = Convert.ToDateTime(reader["PublishedDate"]),
                        PictureColumn = reader["PictureColumn"] != DBNull.Value ? (IFormFile)reader["PictureColumn"] : null

                    };
                    blogs.Add(blog);
                }
            }

            return blogs;
        }


        public void CreateBlog(Blog blog, int userID)
        {
            try
            {
                string query = "INSERT INTO Blogs (UserID, Title, Body, PublishedDate, UpvoteCount, DownvoteCount, CommentCount, PictureColumn) " +
                               "VALUES (@UserID, @Title, @Body, @PublishedDate, @UpvoteCount, @DownvoteCount, @CommentCount, @PictureColumn)";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@Title", blog.Title);
                    command.Parameters.AddWithValue("@Body", blog.Body);
                    command.Parameters.AddWithValue("@PublishedDate", blog.PublishedDate);
                    command.Parameters.AddWithValue("@UpvoteCount", blog.UpvoteCount);
                    command.Parameters.AddWithValue("@DownvoteCount", blog.DownvoteCount);
                    command.Parameters.AddWithValue("@CommentCount", blog.CommentCount);

                    // Convert the image file to a byte array
                    if (blog.PictureColumn != null && blog.PictureColumn.Length > 0)
                    {
                        byte[] imageBytes;
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            blog.PictureColumn.CopyTo(memoryStream);
                            imageBytes = memoryStream.ToArray();
                        }
                        command.Parameters.AddWithValue("@PictureColumn", imageBytes);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@PictureColumn", DBNull.Value);
                    }

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine("Error occurred while creating the blog in the database: " + ex.Message);
                throw;
            }
        }




        // Other methods for CRUD operations on blogs can be added here
    }
}
