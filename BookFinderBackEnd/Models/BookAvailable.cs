namespace BookFinderBackEnd.Models
{
    public class BookAvailable
    {
        public string Match { get; set; }
        public string Status { get; set; }
        public string PublishDate { get; set; }
        public string ItemUrl { get; set; }
        public BookCover Cover { get; set; }
    }
}