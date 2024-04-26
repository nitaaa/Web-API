using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalksUdemy.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1b2ee604-6129-4d84-bb02-cc3aaffb85f8"), "Medium" },
                    { new Guid("79afb848-95c0-4270-a4ca-4e3e9b67d53a"), "Hard" },
                    { new Guid("d4741157-d06a-4346-bbfb-c205ec460041"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("117a1e26-f604-4334-a257-8640a9d826b4"), "NTL", "Northland", null },
                    { new Guid("8768d5da-d098-4949-a37b-86ba93d6d7dd"), "AKL", "Auckland", "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("b15929e0-10bf-43a6-be10-62d7998796d2"), "BOP", "Bay of Plenty", null },
                    { new Guid("c68d8631-6304-4019-9a50-7ffc9b30b27d"), "NSN", "Nelson", "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("c7167312-0cbe-48a7-a747-eabf8a975325"), "STL", "Southland", null },
                    { new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"), "WGN", "Wellington", "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("1b2ee604-6129-4d84-bb02-cc3aaffb85f8"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("79afb848-95c0-4270-a4ca-4e3e9b67d53a"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("d4741157-d06a-4346-bbfb-c205ec460041"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("117a1e26-f604-4334-a257-8640a9d826b4"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("8768d5da-d098-4949-a37b-86ba93d6d7dd"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b15929e0-10bf-43a6-be10-62d7998796d2"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("c68d8631-6304-4019-9a50-7ffc9b30b27d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("c7167312-0cbe-48a7-a747-eabf8a975325"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"));
        }
    }
}
