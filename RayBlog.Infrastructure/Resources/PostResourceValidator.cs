using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayBlog.Infrastructure.Resources
{
   public  class PostResourceValidator: AbstractValidator<PostResource>
    {
        public PostResourceValidator()
        {
            RuleFor(x => x.Author).NotNull().WithName("作者").WithMessage("{PropertyName}必要的");
        }
    }
}
