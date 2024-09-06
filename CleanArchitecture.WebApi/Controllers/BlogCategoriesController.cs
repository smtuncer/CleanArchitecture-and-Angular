using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.CreateBlogCategory;
using CleanArchitecture.Application.Features.BlogCategoryFeatures.Queries.GetAllBlogCategory;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.WebApi.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers;

public sealed class BlogCategoriesController : ApiController
{
    public BlogCategoriesController(IMediator mediator) : base(mediator) { }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CreateBlogCategoryCommand request, CancellationToken cancellationToken)
    {
        MessageResponse response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> GetAll(GetAllBlogCategoryQuery request, CancellationToken cancellationToken)
    {
        PaginationResult<BlogCategory> response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }


}
