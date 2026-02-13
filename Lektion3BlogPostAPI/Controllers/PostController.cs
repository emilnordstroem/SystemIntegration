using Lektion3BlogPostAPI.Data;
using Lektion3BlogPostAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lektion3BlogPostAPI.Controllers
{
	// All methods has async signature in case its needed later

	[Route("api/[controller]")]
	[ApiController]
	public class PostController : ControllerBase
    {
		[HttpGet("posts")]
		public async Task<ActionResult<IEnumerable<BlogPost>>> GetPosts()
		{
			return BlogDataService.GetPosts();
		}

		[HttpPut("posts/{id}")]
		public async Task<ActionResult<BlogPost>> PutPost(long id, BlogPost post)
		{
			return BlogDataService.UpdatePost(id, post);
		}

		[HttpDelete("posts/{id}")]
		public async Task<ActionResult<bool>> DeletePost(long id)
		{
			return BlogDataService.DeletePost(id);
		}

		[HttpGet("authors/{authorId}/posts")]
		public async Task<ActionResult<IEnumerable<BlogPost>>> GetPostsByAuthor(long authorId)
		{
			return BlogDataService.GetPostsByAuthor(authorId);
		}

		[HttpPost("authors/{authorId}/posts")]
		public async Task<ActionResult<BlogPost>> CreatePost(long authorId, BlogPost post)
		{
			return BlogDataService.CreatePost(authorId, post);
		}
	}
}
