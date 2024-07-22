using Microsoft.AspNetCore.Http;

namespace BlogApplication.Models
{
    public class Blog
    {
        public int BlogID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime PublishedDate { get; set; }
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }
        public int CommentCount { get; set; }
        public IFormFile PictureColumn { get; set; } // Property for image upload
        public string PictureColumnPath { get; set; } // Store the file path of the uploaded image
    }
}
