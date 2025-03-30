using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerece.Migrations
{
    /// <inheritdoc />
    public partial class insertproductitemsdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.InsertData(
	table: "ProductItems",
	columns: new[] { "Quantity", "ProductId" },
	values: new object[,]
	{
		{ 10, 3 }, // Dell XPS 15
        { 15, 4 },
		{ 12, 2 }, // MacBook Air M2
        { 20, 3 }, // HP Spectre x360
        { 25, 4 }, // Lenovo ThinkPad X1
        { 18, 5 }, // Asus ROG Zephyrus
        { 30, 6 }, // Acer Predator Helios
        { 22, 7 }, // MSI Stealth 15M
        { 15, 8 }, // iPhone 15 Pro
        { 20, 9 }, // Samsung Galaxy S23
        { 17, 10 }, // Google Pixel 7
        { 25, 11 }, // OnePlus 11
        { 30, 12 }, // Xiaomi 13 Pro
        { 28, 13 }, // Oppo Find X5
        { 12, 14 }, // Sony Xperia 1 V
        { 10, 15 }, // Canon EOS R5
        { 14, 16 }, // Sony A7 IV
        { 8, 17 }, // Nikon Z9
        { 15, 18 }, // Fujifilm X-T5
        { 12, 19 }, // Panasonic Lumix S5
        { 6, 20 }, // Leica Q2
        { 30, 21 }, // GoPro Hero 11
        { 40, 22 }, // Logitech MX Master 3
        { 25, 23 }, // Sony WH-1000XM5
        { 20, 24 }, // Apple AirPods Pro
        { 15, 25 }, // Samsung Galaxy Buds2 Pro
        { 18, 26 }, // Razer BlackWidow V4
        { 50, 27 }, // Anker PowerCore 26800
        { 35, 28 }  // SanDisk Extreme SSD
    });


		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
