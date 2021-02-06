using System;
using System.Net.Http;
using System.Linq;
using BookFinderBackEnd.Models;

namespace BookFinderBackEnd.Services
{
    public class GoogleBooksService : Service
    {
        public GoogleBooksService(IHttpClientFactory clientFactory)
        {
            this.baseUrl = "https://www.googleapis.com/";
            this._clientFactory = clientFactory;
        }
        public virtual ServiceResponse<FoundBooks> FoundBooks(string keyWord, int pageNumber)
        {
            int startIndex = 10 * pageNumber;
            string pathController = $"/books/v1/volumes?q={keyWord}&startIndex={startIndex}";
            return TheGet<FoundBooks>(pathController);
        }

        public virtual ServiceResponse<FoundBooks> FoundBooksAdvanced(
            int pageNumber, 
			string isbn, string author, 
			string publisher, string title
        )
        {
            int startIndex = 10 * pageNumber;
            string filter = AdaptFilter(
                new string[]{"isbn:",isbn}, 
                new string[]{"+inauthor:",author}, 
                new string[]{"+inpublisher:",publisher}, 
                new string[]{"+intitle:",title}
            );
            string pathController = $"/books/v1/volumes?q={filter}&startIndex={startIndex}";
            return TheGet<FoundBooks>(pathController);
        }

        private static string AdaptFilter(
            params string[][] data
        )
        {           
            return String.Concat(
                data.Where(
                    x => !String.IsNullOrEmpty(x.Last()) 
                )
                .Select(
                    x => String.Concat(x)
                )
            );
        }
    }
}