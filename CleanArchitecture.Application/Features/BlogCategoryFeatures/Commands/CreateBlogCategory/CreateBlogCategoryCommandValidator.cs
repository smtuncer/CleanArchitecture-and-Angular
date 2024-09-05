using CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.CreateBlogCategory;
using FluentValidation;

namespace CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;

public sealed class CreateBlogCategoryCommandValidator : AbstractValidator<CreateBlogCategoryCommand>
{
    public CreateBlogCategoryCommandValidator()
    {
        RuleFor(p => p.CategoryName).NotEmpty().WithMessage("Kategori adı boş olamaz!");
        RuleFor(p => p.CategoryName).NotNull().WithMessage("Kategori adı boş olamaz!");
        RuleFor(p => p.CategoryName).MinimumLength(2).WithMessage("Kategori adı en az 2 karakter olmalıdır!");
    }
}
