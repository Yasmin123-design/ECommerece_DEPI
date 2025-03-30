using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerece.Migrations
{
    /// <inheritdoc />
    public partial class variationoptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
table: "VariationOptions",
columns: new[] { "VariationId", "Value" },
values: new object[,]
{
    // Connectivity (الاتصال)
    { 1, "Wi-Fi" },
    { 1, "Bluetooth" },
    { 1, "NFC" },
    { 1, "5G" },

    // Lens Type (نوع العدسة)
    { 2, "Wide" },
    { 2, "Telephoto" },
    { 2, "Macro" },
    { 2, "Ultra-Wide" },

    // Battery Life (عمر البطارية)
    { 3, "3000mAh" },
    { 3, "4000mAh" },
    { 3, "5000mAh" },
    { 3, "6000mAh" },

    // Storage (التخزين)
    { 4, "64GB" },
    { 4, "128GB" },
    { 4, "256GB" },
    { 4, "512GB" },

    // Screen Size (حجم الشاشة)
    { 5, "5.5 inch" },
    { 5, "6.1 inch" },
    { 5, "6.7 inch" },
    { 5, "7.2 inch" },

    // Color (اللون)
    { 6, "Black" },
    { 6, "White" },
    { 6, "Red" },
    { 6, "Blue" },

    // RAM
    { 7, "4GB" },
    { 7, "6GB" },
    { 7, "8GB" },
    { 7, "12GB" },

    // Processor (المعالج)
    { 8, "Snapdragon" },
    { 8, "MediaTek" },
    { 8, "Exynos" },
    { 8, "Apple A-Series" },

    // Graphics Card (كرت الشاشة)
    { 9, "Integrated" },
    { 9, "GTX 1650" },
    { 9, "RTX 3060" },
    { 9, "RTX 4080" },

    // Material (المادة)
    { 10, "Plastic" },
    { 10, "Metal" },
    { 10, "Glass" },
    { 10, "Ceramic" },

        { 11, "Micro USB" },
    { 11, "Lightning" },
    { 11, "USB Type-C" },
    { 11, "Fast Charging " },
});
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
