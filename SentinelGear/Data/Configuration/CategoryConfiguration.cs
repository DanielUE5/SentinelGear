using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SentinelGear.Models;

namespace SentinelGear.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(SeedCategories());
        }

        private static IEnumerable<Category> SeedCategories()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Protective Gear"
                },
                new Category
                {
                    Id = 2,
                    Name = "Tactical Backpacks"
                },
                new Category
                {
                    Id = 3,
                    Name = "Field Accessories"
                },
                new Category
                {
                    Id = 4,
                    Name = "Combat Clothing"
                },
                new Category
                {
                    Id = 5,
                    Name = "Boots"
                }
            };
        }
    }
}