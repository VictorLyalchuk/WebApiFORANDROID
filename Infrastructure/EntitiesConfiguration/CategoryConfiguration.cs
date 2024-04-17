using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            //Set Primary Key
            builder.HasKey(c => c.Id);

            //Set Property configurations
            builder.Property(c => c.Name)
                   .HasMaxLength(180)
                   .IsRequired();
        }
    }
}
