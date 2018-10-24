using Microsoft.EntityFrameworkCore;
using RayBlog.Core.Entities;
using RayBlog.Core.Interfaces;
using RayBlog.Infrastructure.Database;
using RayBlog.Infrastructure.Extensions;
using RayBlog.Infrastructure.Resources;
using RayBlog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayBlog.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly MyContext _myContext;
        private readonly IPropertyMappingContainer _propertyMappingContainer;

        public PostRepository(MyContext myContext, IPropertyMappingContainer propertyMappingContainer)
        {
            _myContext = myContext;
            _propertyMappingContainer = propertyMappingContainer;
        }

        public async Task<PaginatedList<Post>> GetAllPostsAsync(PostParameters postParameters)
        {
            var query = _myContext.Posts.AsQueryable();
            if (!string.IsNullOrEmpty(postParameters.Title))
            {
                var title = postParameters.Title.ToLowerInvariant();
                query = query.Where(x => x.Title.ToLowerInvariant()==title);
            }
           // query= query.OrderBy(r=>postParameters.OrderBy);
            query = query.ApplySort(postParameters.OrderBy, _propertyMappingContainer.Resolve<PostResource, Post>());
            var count = await query.CountAsync();
            var data = await query.Skip(postParameters.PageIndex * postParameters.PageSize).Take(postParameters.PageSize).ToListAsync();
            return new PaginatedList<Post>(postParameters.PageIndex,postParameters.PageSize,count,data);
        }
        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _myContext.Posts.FindAsync(id);
        }
        public void AddPost(Post post)
        {
            _myContext.Posts.Add(post);
        }


        public void Delete(Post post)
        {
            _myContext.Posts.Remove(post);
        }

        public void Update(Post post)
        {
            _myContext.Entry(post).State = EntityState.Modified;
        }
    }
}
