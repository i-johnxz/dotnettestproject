using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogsCore.Models.EntityTypeConfigurations
{
    public class BlogEntityTypeConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(p => p.Timestamp).IsRowVersion();
            builder.Property<DateTime>("LastUpdated");

            builder.HasIndex(b => b.Url).IsUnique();

            builder.HasDiscriminator<BlogType>("BlogType")
                .HasValue<Blog>(BlogType.Basic)
                .HasValue<RssBlog>(BlogType.Rss);

            //builder.Property(e => e.BlogType)
            //    //.HasMaxLength(200)
            //    .HasColumnName("blog_type");
        }
    }
}