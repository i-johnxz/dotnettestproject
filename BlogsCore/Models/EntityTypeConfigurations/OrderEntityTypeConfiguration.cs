using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogsCore.Models.EntityTypeConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.StreetAddress);
        }
    }
}
