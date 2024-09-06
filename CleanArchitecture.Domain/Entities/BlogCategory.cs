using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Entities;

public sealed class BlogCategory : Entity
{
    public string BlogCategoryImageUrl { get; set; } = string.Empty;
    public required string CategoryName { get; set; }
    public required string CategoryDescription { get; set; }
    public bool IsPublished { get; set; }
}