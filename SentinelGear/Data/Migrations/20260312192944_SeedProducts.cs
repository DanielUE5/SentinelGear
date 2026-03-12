using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SentinelGear.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Protective Gear" },
                    { 2, "Tactical Backpacks" },
                    { 3, "Field Accessories" },
                    { 4, "Combat Clothing" },
                    { 5, "Boots" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsDeleted", "Manufacturer", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, "Здрава тактическа каска с висока устойчивост.", "/images/products/tactical_ballistic_helmet.png", false, "Sentinel Defense", "Tactical Ballistic Helmet", 249.99m, 12 },
                    { 2, 1, "Тактическа жилетка с MOLLE система.", "/images/products/plate_carrier_vest.png", false, "Iron Shield", "Plate Carrier Vest", 319.50m, 8 },
                    { 3, 1, "Подсилени тактически ръкавици за защита и сцепление.", "/images/products/tactical_protective_gloves.png", false, "TacCore", "Tactical Protective Gloves", 44.99m, 20 },
                    { 4, 2, "Раница с голям капацитет за полеви условия.", "/images/products/recon_tactical_backpack_45L.png", false, "FieldOps", "Recon Tactical Backpack 45L", 189.90m, 15 },
                    { 5, 2, "Компактна тактическа раница за ежедневна употреба.", "/images/products/urban_tactical_backpack_30L.png", false, "FieldOps", "Urban Tactical Backpack 30L", 129.99m, 22 },
                    { 6, 2, "Издръжлива раница за дълги мисии.", "/images/products/heavy_duty_military_backpack_60L.png", false, "Iron Shield", "Heavy Duty Military Backpack 60L", 219.50m, 10 },
                    { 7, 3, "Подсумник за инструменти и оборудване.", "/images/products/compact_utility_pouch.png", false, "TacCore", "Compact Utility Pouch", 34.99m, 30 },
                    { 8, 3, "Здрав колан за носене на оборудване.", "/images/products/heavy_duty_tactical_belt.png", false, "FieldOps", "Heavy Duty Tactical Belt", 49.90m, 22 },
                    { 9, 3, "Мощен LED фенер за тактически операции.", "/images/products/military_flashlight.png", false, "BlackRoute", "Military Flashlight", 59.99m, 25 },
                    { 10, 4, "Комплект бойно облекло за полеви условия.", "/images/products/combat_uniform_set.png", false, "StrikeWear", "Combat Uniform Set", 139.00m, 20 },
                    { 11, 4, "Лека тактическа риза с висока издръжливост.", "/images/products/tactical_combat_shirt.png", false, "StrikeWear", "Tactical Combat Shirt", 79.99m, 18 },
                    { 12, 4, "Тактически панталони с подсилени колена.", "/images/products/military_tactical_pants.png", false, "StrikeWear", "Military Tactical Pants", 99.90m, 16 },
                    { 13, 5, "Тактически обувки с висока стабилност.", "/images/products/tactical_combat_boots.png", false, "BlackRoute", "Tactical Combat Boots", 159.75m, 18 },
                    { 14, 5, "Леки тактически обувки за бързо движение.", "/images/products/lightweight_tactical_boots.png", false, "FieldOps", "Lightweight Tactical Boots", 129.99m, 20 },
                    { 15, 5, "Водоустойчиви обувки за тежки условия.", "/images/products/waterproof_military_boots.png", false, "Iron Shield", "Waterproof Military Boots", 179.99m, 14 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
