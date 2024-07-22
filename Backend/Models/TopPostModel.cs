namespace BlogApplication.Models
{
    public class TopPostModel
    {
        public int BlogID { get; set; }
        public string PostTitle { get; set; }
        public string AuthorName { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public int Comments { get; set; }
    }
}
