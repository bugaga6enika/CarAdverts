using CarAdverts.Domain.CarAdvert;
using CarAdverts.Infrastructure.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAdverts.Infrastructure.Configurations
{
    public class CarAdvertConfiguration : IEntityTypeConfiguration<CarAdvert>
    {
        public void Configure(EntityTypeBuilder<CarAdvert> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Title)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(c => c.Price)
                .HasColumnType("decimal(18, 6)")
                .HasConversion<double>()
                .IsRequired();

            builder.Property(x => x.Fuel)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.New)
                .IsRequired();

            builder.Property(x => x.Mileage)
                .IsRequired(false);

            builder.OwnsOne(x => x.FirstRegistration)
                .Property(property => property.Date)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>()
                .HasColumnName("FirstRegistration")
                .HasColumnType("date")
                .IsRequired(false);
            //    , navigationBuilder =>
            //{
            //    navigationBuilder.Property(property => property.Date)
            //    .HasConversion<DateOnlyConverter, DateOnlyComparer>()
            //    .HasColumnName("FirstRegistration")
            //    .HasColumnType("date")
            //    .IsRequired(false);
            //});
        }
    }
}
