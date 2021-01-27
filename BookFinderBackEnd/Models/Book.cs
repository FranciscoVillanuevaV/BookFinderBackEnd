using System.Collections.Generic;

namespace BookFinderBackEnd.Models
{
    public class Book
    {
        public BookInfo Records { get; set; }
        public List<BookAvailable> Items { get; set;}
    }
}