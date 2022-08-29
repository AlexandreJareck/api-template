using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings;

public class AddressMapping : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Street)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Number)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Zipcode)
            .IsRequired()
            .HasMaxLength(8);

        builder.Property(p => p.Complement)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(p => p.Neighborhood)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.State)
            .IsRequired()
            .HasMaxLength(50);
    }

}
