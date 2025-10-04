using Domain.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.SteamId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Nickname)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.TeamId)
            .IsRequired();

        builder.Property(p => p.LeetifyId)
            .HasMaxLength(100);

        builder.Property(p => p.FaceitId)
            .HasMaxLength(100);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .IsRequired();
    }
}
