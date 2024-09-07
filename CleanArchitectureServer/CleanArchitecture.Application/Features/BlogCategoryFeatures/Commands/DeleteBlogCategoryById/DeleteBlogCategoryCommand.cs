using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.DeleteBlogCategoryById;
public sealed record DeleteBlogCategoryCommand(
    Guid Id) : IRequest<Result<string>>;
