using EventApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventApp.Infrastracture.Configurations;

internal class EventUserRelConfiguration : IEntityTypeConfiguration<EventUserRel>
{
    public void Configure(EntityTypeBuilder<EventUserRel> builder)
    {
        builder.HasKey(eur => eur.Id);

        builder.HasOne(eur => eur.Event)
                .WithMany()
                .HasForeignKey(eur => eur.EventId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(eur => eur.User)
                .WithMany()
                .HasForeignKey(eur => eur.UserId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}