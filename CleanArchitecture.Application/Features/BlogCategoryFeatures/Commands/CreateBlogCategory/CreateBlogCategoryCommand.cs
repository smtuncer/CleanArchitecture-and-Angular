using CleanArchitecture.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.CreateBlogCategory;
public sealed record class CreateBlogCategoryCommand(
    string BlogCategoryImageUrl,
    string CategoryName,
    string CategoryDescription,
    bool IsPublished) : IRequest<MessageResponse>;