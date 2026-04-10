using System.Net.Http.Json;

namespace Lektion12HttpClient
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			var client = new HttpClient();
			var responsePost = await client.GetFromJsonAsync<PostDTO>("https://jsonplaceholder.typicode.com/posts/1");
			Console.WriteLine(responsePost.title);

			List<PostDTO> responsePosts = await client
				.GetFromJsonAsAsyncEnumerable<PostDTO>("https://jsonplaceholder.typicode.com/posts")
				.ToListAsync();
			if(responsePosts == null)
			{
				Console.WriteLine("reponsePosts is null");
				return;
			}
			foreach (var post in responsePosts)
			{
				Console.WriteLine($"{post.id} {post.title}");
			}

			int id = 1;
			List<CommentDTO> responsePostComments = await client
				.GetFromJsonAsAsyncEnumerable<CommentDTO>($"https://jsonplaceholder.typicode.com/posts/{id}/comments")
				.ToListAsync();
			if (responsePostComments == null)
			{
				Console.WriteLine("responsePostComments is null");
				return;
			}
			foreach (var comment in responsePostComments)
			{
				Console.WriteLine($"/////////////////////////////////////////////");
				Console.WriteLine($"Comment from {comment.name}: {comment.body}");
			}
		}
	}
}
