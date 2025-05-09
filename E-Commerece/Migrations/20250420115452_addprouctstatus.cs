﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerece.Migrations
{
    /// <inheritdoc />
    public partial class addprouctstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "Products");
        }
    }
}
