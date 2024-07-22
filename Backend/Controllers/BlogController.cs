using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Security.Claims;
using BlogApplication.DataAccess;
using BlogApplication.Models;
namespace BlogApplication.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogService _blogService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlogController(BlogService blogService, IWebHostEnvironment webHostEnvironment)
        {
            _blogService = blogService;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            var blogPosts = _blogService.GetBlogData();
            return View(blogPosts);
        }


        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Blog model, IFormFile pictureColumn)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if an image was uploaded
                    if (pictureColumn != null && pictureColumn.Length > 0)
                    {
                        // Set the PictureColumn property of the model to the uploaded file
                        model.PictureColumn = pictureColumn;
                    }

                    // Retrieve the userID from the claims (you may need to replace this logic with your actual authentication logic)
                    int userID = 1; // Example: Replace with your logic to get the current user ID

                    // Create a Blog object and pass it to the service layer for insertion
                    Blog blog = new Blog
                    {
                        UserID = userID,
                        Title = model.Title,
                        Body = model.Body,
                        PublishedDate = model.PublishedDate,
                        UpvoteCount = 0,
                        DownvoteCount = 0,
                        CommentCount = 0
                    };

                    _blogService.CreateBlog(blog, userID);

                    TempData["SuccessMessage"] = "Your blog has been published successfully!";
                    return RedirectToAction("Index", "Home");
                }

                // If ModelState is not valid, set an error message and return the view
                ViewBag.ErrorMessage = "Error: Please fill in all the required fields.";
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while creating the blog: " + ex.Message;
                return View(model);
            }
        }




        private int GetCurrentUserID()
        {
            // Get the user's ID from the claims
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            // Check if the claim exists and try to parse the user ID
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            else
            {
                // If the user ID cannot be determined, return a default value or handle the error as needed
                return 0;
            }
        }

        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
