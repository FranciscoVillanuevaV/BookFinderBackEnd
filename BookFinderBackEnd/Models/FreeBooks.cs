using System.Collections.Generic;

namespace BookFinderBackEnd.Models
{
    public class FreeBooks
    {
        public IEnumerable<FreeBookAvailable> Items { get; set;}
    }
}