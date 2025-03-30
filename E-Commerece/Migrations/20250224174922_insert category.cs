using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerece.Migrations
{
    /// <inheritdoc />
    public partial class insertcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Name", "Description" },
                values: new object[,]
                {
                             {"Laptops", "أحدث أجهزة اللابتوب بمواصفات قوية." },
                             {"Smartphones", "مجموعة واسعة من الهواتف الذكية." },
                             { "Cameras", "أفضل الكاميرات لالتقاط لحظاتك المميزة." },
                             { "Accessories", "إكسسوارات مفيدة لأجهزتك الإلكترونية." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
