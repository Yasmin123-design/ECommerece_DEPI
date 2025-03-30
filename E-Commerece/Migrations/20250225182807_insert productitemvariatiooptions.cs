using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerece.Migrations
{
    /// <inheritdoc />
    public partial class insertproductitemvariatiooptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.InsertData(
	table: "ProductItemVariationOption",
	columns: new[] { "ProductItemsId","VariationOptionsId" },
	values: new object[,]
	{
		{ 31, 25 },  // RAM: 8GB - Dell XPS 15
        { 31, 29 },  // Processor: Intel i7
        { 31, 33 },  // Graphics Card: RTX 3060
        
        { 32, 26 },  // RAM: 8GB - MacBook Air M2
        { 32, 30 },  // Processor: Apple M2
        { 32, 34 }, // Material: Metal
        
        { 33, 27 }, // Storage: 128GB - Samsung Galaxy S23
        { 33, 31 }, // Screen Size: 6.1 inch
        { 33, 35 }, // Color: Black
        
        { 34, 28 },  // Storage: 256GB - iPhone 15 Pro
        { 34, 32 },  // Screen Size: 6.7 inch
        { 34, 36 },  // Color: White

        { 35, 25 },  // Connectivity: Wi-Fi - Sony A7 IV
        { 35, 29 },  // Lens Type: Telephoto
        { 35, 33 },  // Battery Life: 4000mAh

        { 36, 26 },  // Material: Plastic - Logitech MX Master 3
        { 36, 30 },  // Charging Type: USB Type-C
        { 36, 34 },  // Charging Type: USB Type-C

        { 37, 27 },  // RAM: 8GB - Dell XPS 15
        { 37, 31 },  // Processor: Intel i7
        { 37, 35 },  // Graphics Card: RTX 3060
        
        { 38, 28 },  // RAM: 8GB - MacBook Air M2
        { 38, 32 },  // Processor: Apple M2
        { 38, 36 }, // Material: Metal
        
        { 39, 28 }, // Storage: 128GB - Samsung Galaxy S23
        { 39, 32 }, // Screen Size: 6.1 inch
        { 39, 36 }, // Color: Black
        
        { 40, 13 },  // Storage: 256GB - iPhone 15 Pro
        { 40, 17 },  // Screen Size: 6.7 inch
        { 40, 21 },  // Color: White

        { 41, 14 },  // Connectivity: Wi-Fi - Sony A7 IV
        { 41, 18 },  // Lens Type: Telephoto
        { 41, 22 },  // Battery Life: 4000mAh

        { 42, 15 },  // Material: Plastic - Logitech MX Master 3
        { 42, 19 },  // Charging Type: USB Type-C
        { 42, 23 },  // Charging Type: USB Type-C

        { 43, 16 },  // RAM: 8GB - MacBook Air M2
        { 43, 20 },  // Processor: Apple M2
        { 43, 24 }, // Material: Metal
        
        { 44, 13 }, // Storage: 128GB - Samsung Galaxy S23
        { 44, 17 }, // Screen Size: 6.1 inch
        { 44, 21 }, // Color: Black
        
        { 45, 14 },  // Storage: 256GB - iPhone 15 Pro
        { 45, 18 },  // Screen Size: 6.7 inch
        { 45, 22 },  // Color: White

        { 46, 15 },  // Connectivity: Wi-Fi - Sony A7 IV
        { 46, 19 },  // Lens Type: Telephoto
        { 46, 23 },  // Battery Life: 4000mAh

        { 47, 1 },  // Material: Plastic - Logitech MX Master 3
        { 47, 5 },  // Charging Type: USB Type-C
        { 47, 9 },  // Charging Type: USB Type-C

        { 48, 2 },  // RAM: 8GB - Dell XPS 15
        { 48, 6 },  // Processor: Intel i7
        { 48, 10 },  // Graphics Card: RTX 3060
        
        { 49, 3 },  // RAM: 8GB - MacBook Air M2
        { 49, 7 },  // Processor: Apple M2
        { 49, 11 }, // Material: Metal
        
        { 50, 4 }, // Storage: 128GB - Samsung Galaxy S23
        { 50, 8 }, // Screen Size: 6.1 inch
        { 50, 12 }, // Color: Black
        
        { 51, 1 },  // Storage: 256GB - iPhone 15 Pro
        { 51, 5 },  // Screen Size: 6.7 inch
        { 51, 9 },  // Color: White

        { 52, 2 },  // Connectivity: Wi-Fi - Sony A7 IV
        { 52, 6 },  // Lens Type: Telephoto
        { 52, 10 },  // Battery Life: 4000mAh

        { 53, 3 },  // Material: Plastic - Logitech MX Master 3
        { 53, 7 },  // Charging Type: USB Type-C
        { 53, 11 },  // Charging Type: USB Type-C

        { 54, 37 }, // Storage: 128GB - Samsung Galaxy S23
        { 54, 41 }, // Screen Size: 6.1 inch
        
        { 55, 38 },  // Storage: 256GB - iPhone 15 Pro
        { 55, 42 },  // Screen Size: 6.7 inch

        { 56, 39 },  // Connectivity: Wi-Fi - Sony A7 IV
        { 27, 43 },  // Lens Type: Telephoto

        { 57, 40 },  // Material: Plastic - Logitech MX Master 3
        { 57, 44 },  // Charging Type: USB Type-C

        { 58, 37 },  // Connectivity: Wi-Fi - Sony A7 IV
        { 58, 41 },  // Lens Type: Telephoto

        { 59, 38 },  // Material: Plastic - Logitech MX Master 3
        { 59, 42 },  // Charging Type: USB Type-C
    });

		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
