using System;
using System.Net.Http;

using System.Text.Json;
using System.Threading.Tasks;

namespace BookFinderBackEnd.Services
{
    public abstract class Service
    {
        protected IHttpClientFactory _clientFactory;
        protected string baseUrl;
        protected static string FixJsonResponse(string jsonInput)
        {
            string readResultFixed = jsonInput;
            int start = jsonInput.IndexOf('/');
            if (start >= 0)
            {
                int end = jsonInput.IndexOf('\"', start);
                string toReplace = jsonInput.Substring(start, end - start);
                readResultFixed = jsonInput.Replace(toReplace, "/books/");
            }
            return readResultFixed;
        }
        protected static ServiceResponse<output> ReadResult<output>(Task<HttpResponseMessage> response) 
        where output : class
        {
            ServiceResponse<output> serviceResponse = new ServiceResponse<output>();
            var result = response.Result;
            serviceResponse.Message = result.ReasonPhrase + " in" + result.RequestMessage;          
            serviceResponse.CodeStatus = (int)result.StatusCode;
            serviceResponse.HasIssues = !result.IsSuccessStatusCode;

            if (serviceResponse.HasIssues)
                return serviceResponse;
            
            string readResult = result.Content.ReadAsStringAsync().Result;
            
            if (serviceResponse.CodeStatus != 200 || readResult.Equals("[]"))
                return new ServiceResponse<output>();        
            
            string readResultFixed = FixJsonResponse(readResult);

            serviceResponse.ApiResponse = JsonSerializer.Deserialize<output>(
                readResultFixed, 
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return serviceResponse;
        }
        protected ServiceResponse<output> TheGet<output>(string pathController) 
        where output : class
        {
            try
            {
                var client = _clientFactory.CreateClient();
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync(pathController);
                response.Wait();
                return ReadResult<output>(response);
            }
            catch (Exception e)
            {
                return ReturnFail<output>(e.Message);
            }
        }
        protected ServiceResponse<output> ReturnFail<output>(string message) 
        where output : class
        {
            ServiceResponse<output> serviceResponse = new ServiceResponse<output>()
            {
                HasIssues = true,
                CodeStatus = 500,
                Message = message
            };
            return serviceResponse;
        }
    }
}