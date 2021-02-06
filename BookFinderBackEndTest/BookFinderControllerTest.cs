using System.Text.Encodings.Web;
using System.Text.Json;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using BookFinderBackEnd.Controllers;
using BookFinderBackEnd.Models;
using BookFinderBackEnd.Services;

namespace BookFinderBackEndTest
{
    [TestClass]
    public class BookFinderControllerTest
    {
        private readonly string pathToJsonExpectedResults = @"ExpectedResponses\";
        private BookFinderController controller;
        public BookFinderControllerTest()
        {
            var mockFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientFactory factory = mockFactory.Object;
            controller = new BookFinderController(factory);
        }
        
        [TestMethod]
        public void SearchBook_Data_Expected()
        {
            var response = controller.SearchBook(0, "cat");

            //encoder to serialize special characters
            string jsonResponse = JsonSerializer                      
            .Serialize(
                (FoundBooks)((OkObjectResult)response).Value, 
                new JsonSerializerOptions()
                { 
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                }
            )
            .ToLower();

            string expected = JsonFile.ReadAsJson(
                pathToJsonExpectedResults + 
                "SearchBook_Data_Expected.json")
            .ToLower();

            Assert.AreEqual(jsonResponse,expected);
        }

        [TestMethod]
        public void SearchBook_NotRegisteredData_ExpectedResult()
        {
            var response = controller.SearchBook(19, "crazy90909");

            Assert.AreEqual(
                204,
                ((StatusCodeResult)response).StatusCode
            );
        }
    }
}
