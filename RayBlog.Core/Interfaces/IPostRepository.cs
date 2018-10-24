using RayBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RayBlog.Core.Interfaces
{
   public  interface IPostRepository
    {
        //Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<PaginatedList<Post>> GetAllPostsAsync(PostParameters postParameters);
        Task<Post> GetPostByIdAsync(int id);
        void AddPost(Post post);
        void Delete(Post post);
        void Update(Post post);
    }
}
