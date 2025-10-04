using Domain.GameStats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal sealed class GameStatConfiguration : IEntityTypeConfiguration<GameStat>
{
    public void Configure(EntityTypeBuilder<GameStat> builder)
    {
        builder.HasKey(gs => gs.Id);

        builder.Property(gs => gs.PlayerId)
            .IsRequired();

        builder.Property(gs => gs.Kills)
            .IsRequired();

        builder.Property(gs => gs.Assists)
            .IsRequired();

        builder.Property(gs => gs.Deaths)
            .IsRequired();

        builder.Property(gs => gs.Adr)
            .IsRequired();

        builder.Property(gs => gs.HsRatio)
            .IsRequired();

        builder.Property(gs => gs.ClutchRatio)
            .IsRequired();

        builder.Property(gs => gs.Fkpr)
            .IsRequired();

        builder.Property(gs => gs.Fdpr)
            .IsRequired();

        builder.Property(gs => gs.CreatedAt)
            .IsRequired();

        builder.Property(gs => gs.UpdatedAt)
            .IsRequired();
    }
}
