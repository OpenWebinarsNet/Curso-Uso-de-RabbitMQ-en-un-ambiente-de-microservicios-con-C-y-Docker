using PostsRestApiService.Models;

namespace PostsRestApiService.Data
{
    public interface IPostDbService
    {
        Task<IResult> CreatePost(Post post);
        Task<IEnumerable<Post>> GetPosts();
    }
}