using System.ComponentModel.DataAnnotations;
namespace BlogApplication.Models
{
    public class MonthlyStats
    {
        public int TotalBlogPosts { get; set; }
        public int TotalUpvotes { get; set; }
        public int TotalDownvotes { get; set; }
        public int TotalComments { get; set; }
    }
}
