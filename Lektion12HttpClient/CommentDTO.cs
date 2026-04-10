using System;
using System.Collections.Generic;
using System.Text;

namespace Lektion12HttpClient
{
	internal record CommentDTO(int postid, int id, string name, string email, string body);
}
