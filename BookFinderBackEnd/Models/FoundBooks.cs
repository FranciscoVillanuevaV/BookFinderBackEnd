using System.Collections.Generic;

namespace BookFinderBackEnd.Models
{
    public class FoundBooks
    {
        public IEnumerable<FoundBook> items { get; set; }
    }
}