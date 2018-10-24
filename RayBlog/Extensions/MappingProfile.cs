using AutoMapper;
using RayBlog.Core.Entities;
using RayBlog.Infrastructure.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RayBlog.APi.Extensions
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostResource>()
                .ForMember(dest=>dest.UpdateTime,opt=>opt.MapFrom(src=>src.LastModified));
            CreateMap<PostResource, Post>();
            CreateMap<PostAddResource, Post>();
            CreateMap<PostUpdateResource, Post>();
        }
    }
}
