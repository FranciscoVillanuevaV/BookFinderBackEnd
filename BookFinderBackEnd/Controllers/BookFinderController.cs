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
		GoogleBooksService googleBooks;

		public BookFinderController(IHttpClientFactory clientFactory)
		{
			googleBooks = new GoogleBooksService(clientFactory);
		}

		/// <summary>Addvanced search for books.</summary>
		/// <param name="pageNumber">Page number</param>
		/// <param name="isbn"></param>
		/// <param name="author"></param>
		/// <param name="publisher"></param>
		/// <param name="title"></param>
		/// <response code="200">Success</response>
		[HttpGet("search/advanced")]
		[ProducesResponseType(typeof(FoundBooks), StatusCodes.Status200OK)]
		public IActionResult AdvancedSearch(
			int pageNumber, 
			string isbn, string author, 
			string publisher, string title
		)
		{
			try
			{
				ServiceResponse<FoundBooks> response = googleBooks.FoundBooksAdvanced(
					pageNumber, 
					isbn, author, 
					publisher, title
				);

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