using CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.CreateBlogCategory;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CleanArchitecture.UnitTest;

public class BlogCategoriesControllerUnitTest
{
    [Fact]
    public async void Create_ReturnsOkResult_WhenRequestIsValid()
    {
        var mediatorMock = new Mock<IMediator>();
        CreateBlogCategoryCommand createBlogCategoryCommand = new(
            "", "Kategori Adý", "Açýklama", false);

        MessageResponse response = new("Blog Kategorisi baþarýyla kaydedildi!");
        CancellationToken cancellationToken = new();

        mediatorMock.Setup(m => m.Send(createBlogCategoryCommand, cancellationToken))
            .ReturnsAsync(response);

        BlogCategoriesController blogCategoriesController = new(mediatorMock.Object);

        //Act
        var result = await blogCategoriesController.Create(createBlogCategoryCommand, cancellationToken);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<MessageResponse>(okResult.Value);

        Assert.Equal(response, returnValue);
        mediatorMock.Verify(m => m.Send(createBlogCategoryCommand, cancellationToken), Times.Once);

    }
}