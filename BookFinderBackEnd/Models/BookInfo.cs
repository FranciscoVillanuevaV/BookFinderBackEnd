using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace BookFinderBackEnd.Models
{
    public class BookInfo
    {
        [JsonPropertyName("/books/")]
        public BookData Books { get; set; }
    }
    public class BookData
    {
        public List<string> Isbns { get; set; }
        public Data Data { get; set; }
    }
    public class Data
    {
        public List<Names> Authors { get; set; }
        public string Title { get; set; }
        public int Number_Of_Pages { get; set; }
        public List<Names> Publishers { get; set; }
        public string Publish_Date { get; set; }
        public BookCover Cover { get; set; }
    }
    public class Names
    {
        public string Name { get; set; }
    }
}