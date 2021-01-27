using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookFinderBackEnd.Services
{
    public class ServiceResponse<Response> : ControllerBase where Response : class
    {
        public bool HasIssues { get; set; }
        public int CodeStatus { get; set; }
        public string Message { get; set; }
        public Response ApiResponse { get; set; }

        public IActionResult ShowIssues()
        {
            if (CodeStatus == 404)
            {
                return NotFound(Message);
            }
            else if (CodeStatus == 400)
            {
                return BadRequest(Message);                    
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Message);
            }                    
        }
    }
}