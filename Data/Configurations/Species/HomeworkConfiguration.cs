using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Homework = Data.Models.Species.Homework;

namespace Data.Configurations.Species
{
    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Title)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(h => h.Description)
                .HasMaxLength(2048);

            builder.HasOne(h => h.Subject)
                .WithMany(s => s.HomeworkCollection);
        }
    }
}
