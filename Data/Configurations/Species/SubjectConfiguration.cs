using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Species
{
    class SubjectConfiguration : IEntityTypeConfiguration<Models.Species.Subject>
    {
        public void Configure(EntityTypeBuilder<Models.Species.Subject> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(s => s.Description)
                .HasMaxLength(1024);

            builder.HasOne(s => s.Group)
                .WithMany(g => g.Subjects);

            builder.HasMany(s => s.HomeworkCollection)
                .WithOne(h => h.Subject);
        }
    }
}
