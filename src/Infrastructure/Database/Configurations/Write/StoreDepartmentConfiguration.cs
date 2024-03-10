using Domain.StoreDepartments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Write;

internal sealed class  StoreDepartmentConfiguration  : IEntityTypeConfiguration<StoreDepartment>
{
    public void Configure(EntityTypeBuilder<StoreDepartment> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .HasMaxLength(65)
            .IsRequired();
    }
}
