using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings;

public class ProviderMapping : IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Document)
            .IsRequired()
            .HasMaxLength(14);

        builder.HasOne(p => p.Addreess)
            .WithOne(a => a.Provider);

        builder.HasMany(p => p.Products)
            .WithOne(p => p.Provider)
            .HasForeignKey(p => p.ProviderId);
    }

}
