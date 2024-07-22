namespace BlogApplication.Models;
public class BlogPost
{
    public int BlogID { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime PublishedDate { get; set; }
    public string PictureColumn { get; set; } // Ensure this property exists
}
