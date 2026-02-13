using Lektion3BlogPostAPI.Models;

namespace Lektion3BlogPostAPI.Data
{
    public class BlogDataService
    {
		private static List<Author> _authors = new List<Author>();
		private static long _nextAuthorId = 1;
		public static List<Author> GetAuthors()
		{
			return _authors;
		}
		public static Author GetAuthorById(long id)
		{
			return _authors.Where(author => author.Id == id).FirstOrDefault();
        }
		public static Author CreateAuthor(Author author)
		{
			if (_authors.Contains(author))
			{
				return null;
			}
			author.Id = _nextAuthorId++;
			_authors.Add(author);
			return author;
		}
		public static Author UpdateAuthor(long id, Author author)
		{
			Author oldAuthor = _authors.Where(author => author.Id == id).FirstOrDefault();
			if (DeletePost(oldAuthor.Id))
			{
				_authors.Add(author);
				return author;

			}
			return null;
		}
		public static bool DeleteAuthor(long id)
		{
			Author author = _authors.Where(author => author.Id == id).FirstOrDefault();
			return _authors.Remove(author);
		}


		private static List<BlogPost> _posts = new List<BlogPost>();
		private static long _nextPostId = 1;
		public static List<BlogPost> GetPosts()
		{
			return _posts;
		}
		public static List<BlogPost> GetPostsByAuthor(long authorId)
		{
			return _posts.Where(post => post.AuthorId == authorId).ToList();
        }
		public static BlogPost CreatePost(long authorId, BlogPost post)
		{
			if (_posts.Contains(post))
			{
				return null;
			}
			post.Id = _nextPostId++;
			_posts.Add(post);
			return post;
		}
		public static BlogPost UpdatePost(long id, BlogPost post)
		{
			BlogPost oldPost = _posts.Where(post => post.Id == id).FirstOrDefault();
			if (DeletePost(oldPost.Id))
			{
				_posts.Add(post);
				return post;
			}
			return null;
		}
		public static bool DeletePost(long id)
		{
			BlogPost post = _posts.Where(post => post.Id == id).FirstOrDefault();
			return _posts.Remove(post);;
		}

	}
}
