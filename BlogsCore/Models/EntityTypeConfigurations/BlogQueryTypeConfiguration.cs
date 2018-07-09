using BlogsCore.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogsCore.Models.EntityTypeConfigurations
{
    public class BlogQueryTypeConfiguration : IQueryTypeConfiguration<BlogPostsCount>
    {
        public void Configure(QueryTypeBuilder<BlogPostsCount> builder)
        {
            builder.ToView("View_BlogPostCounts").Property(v => v.BlogName).HasColumnName("Name");
        }
    }
}