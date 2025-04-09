using EventApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EventApp.Data.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(u => u.Name).IsRequired().HasMaxLength(30);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Phone).IsRequired().HasMaxLength(30);
        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.PasswordSalt).IsRequired();
        builder.Property(u => u.Username).IsRequired().HasMaxLength(25);

        builder.HasOne(r => r.Role)
        .WithMany(r => r.Users)
        .HasForeignKey(u => u.RoleId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.BookedEvents)
       .WithMany(e => e.BookedUsers) // Event tarafını da bağladık
       .UsingEntity<EventUserRel>(
           j => j
               .HasOne(eur => eur.Event)
               .WithMany()
               .HasForeignKey(eur => eur.EventId),

           j => j
               .HasOne(eur => eur.User)
               .WithMany()
               .HasForeignKey(eur => eur.UserId),

           j =>
           {
               j.HasKey(eur => eur.Id);
               j.ToTable("EventUserRels");
           });

    }
}