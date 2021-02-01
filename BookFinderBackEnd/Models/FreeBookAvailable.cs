namespace BookFinderBackEnd.Models
{
    public class FreeBookAvailable
    {
        public string Match { get; set; }
        public string Status { get; set; }
        public string PublishDate { get; set; }
        public string ItemUrl { get; set; }
        public FreeBookCover Cover { get; set; }
    }
}