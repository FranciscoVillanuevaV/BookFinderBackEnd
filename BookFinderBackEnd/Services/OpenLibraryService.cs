using System.Net.Http;
using BookFinderBackEnd.Models;

namespace BookFinderBackEnd.Services
{
    public class OpenLibraryService : Service
    {
        public OpenLibraryService(IHttpClientFactory clientFactory)
        {
            this.baseUrl = "http://openlibrary.org/";
            this._clientFactory = clientFactory;
        }
        public virtual ServiceResponse<Book> BookByIsbn(string isbn)
        {
            string pathController = $"/api/volumes/brief/isbn/{isbn}.json";
            return TheGet<Book>(pathController);
        }
    }
}