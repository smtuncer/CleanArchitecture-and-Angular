using CleanArchitecture.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities;
public sealed class Blog : Entity
{
    public Guid BlogCategoryId { get; set; }
    public BlogCategory? BlogCategory { get; set; }
    public string BlogImageUrl { get; set; } = string.Empty;
    public required string Title { get; set; }
    public required string Description { get; set; } 
    public bool CommentsEnabled { get; set; }
    public bool IsPublished { get; set; }
}
