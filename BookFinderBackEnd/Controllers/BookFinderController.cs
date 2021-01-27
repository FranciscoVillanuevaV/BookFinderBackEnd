using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using BookFinderBackEnd.Services;
using BookFinderBackEnd.Models;

namespace BookFinderBackEnd.Controllers
{
	[Produces("application/json")]
	[Consumes("application/json")]
	[Route("[controller]")]
	[ApiController]
	public class BookFinderController : ControllerBase
	{
		OpenLibraryService openLibrary;

		public BookFinderController(IHttpClientFactory clientFactory)
		{
			openLibrary = new OpenLibraryService(clientFactory);
		}

		/// <summary>Looks a book for a given isbn and may include readable matches for different editions of the same work.</summary>
		/// <param name="isbn"></param>
		/// <response code="200">Success</response>
		/// <response code="204">No data available</response>
		[HttpGet("isbn/{isbn}")]
		[ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
		public IActionResult LookForBook(string isbn)
		{
			try
			{
				ServiceResponse<Book> result = openLibrary.BookByIsbn(isbn);

				if (result.HasIssues)
					return result.ShowIssues();

				if (result.ApiResponse is null)	
					return StatusCode(StatusCodes.Status204NoContent);

				return Ok(result.ApiResponse);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}
	}
}