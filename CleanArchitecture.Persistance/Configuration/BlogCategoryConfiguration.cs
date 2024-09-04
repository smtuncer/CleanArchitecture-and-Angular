using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Persistance.Configuration;

public sealed class BlogCategoryConfiguration : IEntityTypeConfiguration<BlogCategory>
{
    public void Configure(EntityTypeBuilder<BlogCategory> builder)
    {
        builder.ToTable("BlogCategories");
        builder.HasKey(p => p.Id);
    }
}
