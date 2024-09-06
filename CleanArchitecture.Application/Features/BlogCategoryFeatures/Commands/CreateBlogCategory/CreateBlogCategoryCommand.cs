using MediatR;
using TS.Result;

namespace CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.CreateBlogCategory;
public sealed record class CreateBlogCategoryCommand(
    string BlogCategoryImageUrl,
    string CategoryName,
    string CategoryDescription,
    bool IsPublished) : IRequest<Result<string>>;