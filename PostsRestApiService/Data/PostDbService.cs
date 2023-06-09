using Microsoft.EntityFrameworkCore;
using PostsRestApiService.Models;

namespace PostsRestApiService.Data
{
    public class PostDbService : IPostDbService
    {
        private readonly PostsdbContext _postsdbContext;
        public PostDbService(PostsdbContext postsdbContext)
        {
            _postsdbContext = postsdbContext;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _postsdbContext.Posts.ToListAsync();
        }

        public async Task<IResult> CreatePost(Post post)
        {
            _postsdbContext.Posts.Add(post);
            await _postsdbContext.SaveChangesAsync();
            return Results.Ok(post);
        }
    }
}
