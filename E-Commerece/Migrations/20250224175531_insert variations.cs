using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerece.Migrations
{
    /// <inheritdoc />
    public partial class insertvariations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
table: "Variations",
columns: new[] { "Name" , "CategoryId" },
values: new object[,]
{
    // camera
    { "Connectivity" , 3 }, // الاتصال (Wi-Fi, Bluetooth..)
    { "Lens Type" , 3 } ,    // نوع العدسة (للكاميرات)
    { "Battery Life" , 3 }, // عمر البطارية

    // smartphone
    { "Storage" , 2 },      // التخزين
    { "Screen Size" , 2 },  // حجم الشاشة
    { "Color" , 2 },

    { "RAM" , 1 },          // الرام        
    { "Processor" , 1 },    // المعالج
    { "Graphics Card" , 1 },// كرت الشاشة
                            
    {"Material" , 4 },
    {"Charging Type",4 }

});
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
