using System.Collections.Generic;

namespace BookFinderBackEnd.Models
{
    public class VolumeInfo
    {
        public string title { get; set; }
        public List<string> authors { get; set; }
        public string publishedDate { get; set; }
        public IEnumerable<IndustryIdentifier> industryIdentifiers { get; set; }
        public int pageCount { get; set; }
        public ImageLinks imageLinks { get; set; }
        public string publisher { get; set; }
    }
}