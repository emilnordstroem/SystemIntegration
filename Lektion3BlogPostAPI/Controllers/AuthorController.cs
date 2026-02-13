using Lektion3BlogPostAPI.Data;
using Lektion3BlogPostAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lektion3BlogPostAPI.Controllers
{
	// All methods has async signature in case its needed later

	[Route("api/[controller]")]
	[ApiController]
    public class AuthorController : ControllerBase
    {
		[HttpGet("authors")]
		public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
		{
			return BlogDataService.GetAuthors();
		}

		[HttpGet("authors/{id}")]
		public async Task<ActionResult<Author>> GetAuthor(long id)
		{
			return BlogDataService.GetAuthorById(id);
		}

		[HttpPost("authors")]
		public async Task<ActionResult<Author>> PostAuthor(Author author)
		{
			return BlogDataService.CreateAuthor(author);
		}

		[HttpPut("authors/{id}")]
		public async Task<ActionResult<Author>> PutAuthor(long id, Author author)
		{
			return BlogDataService.UpdateAuthor(id, author);
		}

		[HttpDelete("authors/{id}")]
		public async Task<ActionResult<bool>> DeleteAuthor(long id)
		{
			return BlogDataService.DeleteAuthor(id);
		}
	}
}
