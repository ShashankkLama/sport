using BlogApplication.DataAccess;
using BlogApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BlogApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserService _userService;

        public AdminController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            // Fetch total counts
            var totalCounts = GetTotalCounts();

            // Fetch monthly stats for May 2024 (current month)
            var monthlyStats = GetMonthlyStats(DateTime.Now.Year, DateTime.Now.Month);

            // Create a model object to hold both total counts and monthly stats
            var adminDashboardModel = new AdminDashboardModel
            {
                TotalCounts = totalCounts,
                MonthlyStats = monthlyStats
            };

            return View(adminDashboardModel);
        }


        // Method to fetch total counts
        private TotalCounts GetTotalCounts()
        {
            TotalCounts totalCounts = new TotalCounts();

            string query = @"
                SELECT 
                    COUNT(*) AS Total_Blog_Posts,
                    SUM(UpvoteCount) AS Total_Upvotes,
                    SUM(DownvoteCount) AS Total_Downvotes,
                    SUM(CommentCount) AS Total_Comments
                FROM 
                    [blogdb].[dbo].[Blogs];
            ";
            string connectionString = "Data Source=DESKTOP-TSORS7M\\SQLEXPRESS;Initial Catalog=blogdb;User ID=sa;Password=root;TrustServerCertificate=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    totalCounts = new TotalCounts
                    {
                        TotalBlogPosts = Convert.ToInt32(reader["Total_Blog_Posts"]),
                        TotalUpvotes = Convert.ToInt32(reader["Total_Upvotes"]),
                        TotalDownvotes = Convert.ToInt32(reader["Total_Downvotes"]),
                        TotalComments = Convert.ToInt32(reader["Total_Comments"])
                    };
                }
            }

            return totalCounts;
        }

        // Method to fetch monthly stats
        private MonthlyStats GetMonthlyStats(int year, int month)
        {
            MonthlyStats monthlyStats = new MonthlyStats();

            string query = $@"
                SELECT 
                    COUNT(*) AS Total_Blog_Posts,
                    SUM(UpvoteCount) AS Total_Upvotes,
                    SUM(DownvoteCount) AS Total_Downvotes,
                    SUM(CommentCount) AS Total_Comments
                FROM 
                    [blogdb].[dbo].[Blogs]
                WHERE 
                    YEAR(PublishedDate) = {year} AND MONTH(PublishedDate) = {month};
            ";
            string connectionString = "Data Source=DESKTOP-TSORS7M\\SQLEXPRESS;Initial Catalog=blogdb;User ID=sa;Password=root;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    monthlyStats = new MonthlyStats
                    {
                        TotalBlogPosts = Convert.ToInt32(reader["Total_Blog_Posts"]),
                        TotalUpvotes = Convert.ToInt32(reader["Total_Upvotes"]),
                        TotalDownvotes = Convert.ToInt32(reader["Total_Downvotes"]),
                        TotalComments = Convert.ToInt32(reader["Total_Comments"])
                    };
                }
            }

            return monthlyStats;
        }
        public IActionResult TopBloggers()
        {
            var topBloggers = GetTopBloggers();
            return View(topBloggers);
        }

        public IActionResult TopPosts()
        {
            var topPosts = GetTopPosts();
            return View(topPosts);
        }

        private List<TopBloggerModel> GetTopBloggers()
        {
            List<TopBloggerModel> topBloggers = new List<TopBloggerModel>();

            string query = @"
                SELECT TOP 10
                    u.Name AS Blogger_Name,
                    u.Username AS Blogger_Username,
                    COUNT(*) AS Total_Blog_Posts
                FROM 
                    [blogdb].[dbo].[Blogs] b
                JOIN 
                    [blogdb].[dbo].[Users] u ON b.UserID = u.UserID
                GROUP BY 
                    u.UserID,
                    u.Name,
                    u.Username
                ORDER BY 
                    Total_Blog_Posts DESC;
            ";
            string connectionString = "Data Source=DESKTOP-TSORS7M\\SQLEXPRESS;Initial Catalog=blogdb;User ID=sa;Password=root;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TopBloggerModel blogger = new TopBloggerModel
                    {
                        BloggerName = reader["Blogger_Name"].ToString(),
                        BloggerUsername = reader["Blogger_Username"].ToString(),
                        TotalBlogPosts = Convert.ToInt32(reader["Total_Blog_Posts"])
                    };
                    topBloggers.Add(blogger);
                }
            }

            return topBloggers;
        }

        private List<TopPostModel> GetTopPosts()
        {
            List<TopPostModel> topPosts = new List<TopPostModel>();

            string query = @"
                SELECT TOP 10
                    b.BlogID,
                    b.Title,
                    u.Name AS Author_Name,
                    b.UpvoteCount,
                    b.DownvoteCount,
                    b.CommentCount
                FROM 
                    [blogdb].[dbo].[Blogs] b
                JOIN 
                    [blogdb].[dbo].[Users] u ON b.UserID = u.UserID
                ORDER BY 
                    b.UpvoteCount DESC;
            ";
            string connectionString = "Data Source=DESKTOP-TSORS7M\\SQLEXPRESS;Initial Catalog=blogdb;User ID=sa;Password=root;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TopPostModel post = new TopPostModel
                    {
                        BlogID = Convert.ToInt32(reader["BlogID"]),
                        PostTitle = reader["Title"].ToString(),
                        AuthorName = reader["Author_Name"].ToString(),
                        Upvotes = Convert.ToInt32(reader["UpvoteCount"]),
                        Downvotes = Convert.ToInt32(reader["DownvoteCount"]),
                        Comments = Convert.ToInt32(reader["CommentCount"])
                    };
                    topPosts.Add(post);
                }
            }

            return topPosts;
        }

        public IActionResult Create()
        {
            var user = new User(); // Create a new User object
            return View(user);     // Pass the User object to the view
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.InsertUser(user);
                //TempData["SuccessMessage"] = "User registered successfully!";
                return RedirectToAction("Index", "Admin");
            }
            return View(user);
        }

    }
}
