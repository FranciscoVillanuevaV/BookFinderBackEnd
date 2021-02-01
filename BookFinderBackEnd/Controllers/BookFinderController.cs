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
		GoogleBooksService googleBooks;

		public BookFinderController(IHttpClientFactory clientFactory)
		{
			openLibrary = new OpenLibraryService(clientFactory);
			googleBooks = new GoogleBooksService(clientFactory);
		}

		/// <summary>For a given isbn, looks for readable matches.</summary>
		/// <param name="isbn"></param>
		/// <response code="200">Success</response>
		[HttpGet("free/isbn/{isbn}")]
		[ProducesResponseType(typeof(FreeBooks), StatusCodes.Status200OK)]
		public IActionResult LookForFreeBooks(string isbn)
		{
			try
			{
				ServiceResponse<FreeBooks> result = openLibrary.FreeBooksByIsbn(isbn);

				if (result.HasIssues)
					return result.ShowIssues();

				if (result.ApiResponse is null)
					return NoContent();

				return Ok(result.ApiResponse);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}


		/// <summary>For a given word, searches for all related books and returns pages of 10 items.</summary>
		/// <param name="theWord">The word</param>
		/// <param name="pageNumber">Page number</param>
		/// <response code="200">Success</response>
		[HttpGet("search/keyword/{theWord}")]
		[ProducesResponseType(typeof(FoundBooks), StatusCodes.Status200OK)]
		public IActionResult SearchBook(int pageNumber, string theWord)
		{
			try
			{
				ServiceResponse<FoundBooks> response = googleBooks.FoundBooks(theWord, pageNumber);

				if (response.HasIssues)
					return response.ShowIssues();
				
				if (response.ApiResponse.items is null)
					return NoContent();

				return Ok(response.ApiResponse);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}
	}
}