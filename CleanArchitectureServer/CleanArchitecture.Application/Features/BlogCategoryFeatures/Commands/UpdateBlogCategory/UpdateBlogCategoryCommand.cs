using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.UpdateBlogCategory;
public sealed record class UpdateBlogCategoryCommand(
    Guid Id,
    string BlogCategoryImageUrl,
    string CategoryName,
    string CategoryDescription,
    bool IsPublished,
    bool IsDeleted = false) : IRequest<Result<string>>;