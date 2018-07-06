using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogsCore.Models.EntityTypeConfigurations
{
    public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasOne(p => p.Blog).WithMany(s => s.Posts).HasForeignKey(s => s.BlogId)
                .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}