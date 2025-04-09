using EventApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventApp.Data.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.name).IsRequired().HasMaxLength(30);

        builder.HasMany(r => r.Users)
        .WithOne(u => u.Role)
        .HasForeignKey(u => u.RoleId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}