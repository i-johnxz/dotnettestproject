using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogsCore.Models.EntityTypeConfigurations
{
    public class RecordOfSaleEntityTypeConfiguration : IEntityTypeConfiguration<RecordOfSale>
    {
        public void Configure(EntityTypeBuilder<RecordOfSale> builder)
        {
            builder.HasOne(s => s.Car).WithMany(c => c.SaleHistory)
                .HasForeignKey(s => new { s.CarState, s.CarLicensePlate });
        }
    }
}