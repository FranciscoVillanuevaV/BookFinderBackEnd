using System.Net.Http;
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
    }
}