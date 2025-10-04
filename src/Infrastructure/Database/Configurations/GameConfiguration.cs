using Domain.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal sealed class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Date)
            .IsRequired();

        builder.Property(g => g.TeamA)
            .IsRequired();

        builder.Property(g => g.TeamB)
            .IsRequired();

        builder.Property(g => g.TeamAName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(g => g.TeamBName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(g => g.TeamAScore)
            .IsRequired();

        builder.Property(g => g.TeamBScore)
            .IsRequired();

        builder.Property(g => g.Winner)
            .IsRequired();

        builder.Property(g => g.CreatedAt)
            .IsRequired();

        builder.Property(g => g.UpdatedAt)
            .IsRequired();
    }
}
