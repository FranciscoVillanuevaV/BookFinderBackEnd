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
        public void LookForBook_NotRegisteredISBN_204StatusCode()
        {
            string isbn = "dadada";
            var response = controller.LookForBook(isbn);

            Assert.AreEqual(
                204,
                ((StatusCodeResult)response).StatusCode
            );
        }
        
        [TestMethod]
        public void LookForBook_IsbnWhithOutReadables_ExpectedResult()
        {
            var response = controller.LookForBook("9788492837397");

            string jsonResponse = JsonSerializer
            .Serialize(
                (Book)((OkObjectResult)response).Value)
            .ToLower();

            string expected = JsonFile.ReadAsJson(
                pathToJsonExpectedResults + "BookWhitoutReadables.json")
            .ToLower();

            Assert.AreEqual(jsonResponse,expected);
        }

        [TestMethod]
        public void LookForBook_IsbnWhitReadables_ExpectedResult()
        {
            var response = controller.LookForBook("9780486278070");

            string jsonResponse = JsonSerializer                      
            .Serialize(
                (Book)((OkObjectResult)response).Value)
            .ToLower();

            string expected = JsonFile.ReadAsJson(
                pathToJsonExpectedResults + "BookWhitReadables.json")
            .ToLower();

            Assert.AreEqual(jsonResponse,expected);
        }
    }
}
