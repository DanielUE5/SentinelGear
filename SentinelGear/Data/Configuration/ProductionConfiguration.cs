using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SentinelGear.Models;

namespace SentinelGear.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(SeedProducts());
        }

        private static IEnumerable<Product> SeedProducts()
        {
            return new List<Product>
            {
                // Protective Gear
                new Product
                {
                    Id = 1,
                    Name = "Tactical Ballistic Helmet",
                    Description = "Здрава тактическа каска с висока устойчивост.",
                    Price = 249.99m,
                    StockQuantity = 12,
                    Manufacturer = "Sentinel Defense",
                    ImageUrl = "/images/products/tactical_ballistic_helmet.png",
                    CategoryId = 1,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 2,
                    Name = "Plate Carrier Vest",
                    Description = "Тактическа жилетка с MOLLE система.",
                    Price = 319.50m,
                    StockQuantity = 8,
                    Manufacturer = "Iron Shield",
                    ImageUrl = "/images/products/plate_carrier_vest.png",
                    CategoryId = 1,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 3,
                    Name = "Tactical Protective Gloves",
                    Description = "Подсилени тактически ръкавици за защита и сцепление.",
                    Price = 44.99m,
                    StockQuantity = 20,
                    Manufacturer = "TacCore",
                    ImageUrl = "/images/products/tactical_protective_gloves.png",
                    CategoryId = 1,
                    IsDeleted = false
                },

                // Tactical Backpacks
                new Product
                {
                    Id = 4,
                    Name = "Recon Tactical Backpack 45L",
                    Description = "Раница с голям капацитет за полеви условия.",
                    Price = 189.90m,
                    StockQuantity = 15,
                    Manufacturer = "FieldOps",
                    ImageUrl = "/images/products/recon_tactical_backpack_45L.png",
                    CategoryId = 2,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 5,
                    Name = "Urban Tactical Backpack 30L",
                    Description = "Компактна тактическа раница за ежедневна употреба.",
                    Price = 129.99m,
                    StockQuantity = 22,
                    Manufacturer = "FieldOps",
                    ImageUrl = "/images/products/urban_tactical_backpack_30L.png",
                    CategoryId = 2,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 6,
                    Name = "Heavy Duty Military Backpack 60L",
                    Description = "Издръжлива раница за дълги мисии.",
                    Price = 219.50m,
                    StockQuantity = 10,
                    Manufacturer = "Iron Shield",
                    ImageUrl = "/images/products/heavy_duty_military_backpack_60L.png",
                    CategoryId = 2,
                    IsDeleted = false
                },

                // Field Accessories
                new Product
                {
                    Id = 7,
                    Name = "Compact Utility Pouch",
                    Description = "Подсумник за инструменти и оборудване.",
                    Price = 34.99m,
                    StockQuantity = 30,
                    Manufacturer = "TacCore",
                    ImageUrl = "/images/products/compact_utility_pouch.png",
                    CategoryId = 3,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 8,
                    Name = "Heavy Duty Tactical Belt",
                    Description = "Здрав колан за носене на оборудване.",
                    Price = 49.90m,
                    StockQuantity = 22,
                    Manufacturer = "FieldOps",
                    ImageUrl = "/images/products/heavy_duty_tactical_belt.png",
                    CategoryId = 3,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 9,
                    Name = "Military Flashlight",
                    Description = "Мощен LED фенер за тактически операции.",
                    Price = 59.99m,
                    StockQuantity = 25,
                    Manufacturer = "BlackRoute",
                    ImageUrl = "/images/products/military_flashlight.png",
                    CategoryId = 3,
                    IsDeleted = false
                },

                // Combat Clothing
                new Product
                {
                    Id = 10,
                    Name = "Combat Uniform Set",
                    Description = "Комплект бойно облекло за полеви условия.",
                    Price = 139.00m,
                    StockQuantity = 20,
                    Manufacturer = "StrikeWear",
                    ImageUrl = "/images/products/combat_uniform_set.png",
                    CategoryId = 4,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 11,
                    Name = "Tactical Combat Shirt",
                    Description = "Лека тактическа риза с висока издръжливост.",
                    Price = 79.99m,
                    StockQuantity = 18,
                    Manufacturer = "StrikeWear",
                    ImageUrl = "/images/products/tactical_combat_shirt.png",
                    CategoryId = 4,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 12,
                    Name = "Military Tactical Pants",
                    Description = "Тактически панталони с подсилени колена.",
                    Price = 99.90m,
                    StockQuantity = 16,
                    Manufacturer = "StrikeWear",
                    ImageUrl = "/images/products/military_tactical_pants.png",
                    CategoryId = 4,
                    IsDeleted = false
                },

                // Boots
                new Product
                {
                    Id = 13,
                    Name = "Tactical Combat Boots",
                    Description = "Тактически обувки с висока стабилност.",
                    Price = 159.75m,
                    StockQuantity = 18,
                    Manufacturer = "BlackRoute",
                    ImageUrl = "/images/products/tactical_combat_boots.png",
                    CategoryId = 5,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 14,
                    Name = "Lightweight Tactical Boots",
                    Description = "Леки тактически обувки за бързо движение.",
                    Price = 129.99m,
                    StockQuantity = 20,
                    Manufacturer = "FieldOps",
                    ImageUrl = "/images/products/lightweight_tactical_boots.png",
                    CategoryId = 5,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 15,
                    Name = "Waterproof Military Boots",
                    Description = "Водоустойчиви обувки за тежки условия.",
                    Price = 179.99m,
                    StockQuantity = 14,
                    Manufacturer = "Iron Shield",
                    ImageUrl = "/images/products/waterproof_military_boots.png",
                    CategoryId = 5,
                    IsDeleted = false
                }
            };
        }
    }
}