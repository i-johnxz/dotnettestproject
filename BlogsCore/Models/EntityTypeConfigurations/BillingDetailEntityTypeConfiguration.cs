using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogsCore.Models.EntityTypeConfigurations
{
    public class BillingDetailEntityTypeConfiguration : IEntityTypeConfiguration<BillingDetail>
    {
        public void Configure(EntityTypeBuilder<BillingDetail> builder)
        {
            
        }
    }
}
