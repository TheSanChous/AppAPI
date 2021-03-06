using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Species
{
    class GroupConfiguration : IEntityTypeConfiguration<Models.Species.Group>
    {
        public void Configure(EntityTypeBuilder<Models.Species.Group> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Identifier)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(g => g.Identifier)
                .IsUnique();

            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(g => g.Description)
                .HasMaxLength(1024);

            builder.HasMany(g => g.Subjects)
                .WithOne(s => s.Group);
        }
    }
}
