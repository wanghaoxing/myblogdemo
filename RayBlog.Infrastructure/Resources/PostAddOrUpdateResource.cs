using System;
using System.Collections.Generic;
using System.Text;

namespace RayBlog.Infrastructure.Resources
{
    public class PostAddOrUpdateResource
    {
        public string Title { get; set; }
        public string Body { get; set; }

        public string Remark { get; set; }
    }
}
