using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalk.API.Migrations
{
    /// <inheritdoc />
    public partial class Frist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Difficulties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegioImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Walks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LengthInKm = table.Column<double>(type: "float", nullable: false),
                    WalkImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DifficultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Walks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Walks_Difficulties_DifficultyId",
                        column: x => x.DifficultyId,
                        principalTable: "Difficulties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Walks_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2bafd952-673e-4f83-93a3-f9d5ee64b1b7"), "Hard" },
                    { new Guid("38ef6ec7-7980-4246-8870-03c20a55ca51"), "Easy" },
                    { new Guid("69b761a0-cfbf-433b-9714-5ec3c51f96b0"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name", "RegioImageUrl", "code" },
                values: new object[,]
                {
                    { new Guid("19bfb094-a62d-4a63-a7fe-4d5edff31645"), "Southland", null, "STL" },
                    { new Guid("641e51e6-68c0-4dec-bb40-e85a8b1a9ce8"), "Bayof Plenty", null, "BOP" },
                    { new Guid("68312b7f-21e4-4871-934f-667afb63894d"), "Nelson", "https://media.istockphoto.com/id/1209995566/photo/panorama-of-nelson-city-reflected-in-the-maitai-river-new-zealand.jpg?s=1024x1024&w=is&k=20&c=ynC5H3sKnrq4Tc4swHlao07JPv13wPEmdwuGG6d7IQE=", "NSN" },
                    { new Guid("685dd3c8-c37f-407b-9989-92709bfdd23f"), "Wellington", "https://media.istockphoto.com/id/2066058412/photo/wellington-new-zealands-capital-city.jpg?s=1024x1024&w=is&k=20&c=FasHsT_tNKoUTHwiBsOM1AMjw_u9UCH4Kba1MHmBu0M=", "WGN" },
                    { new Guid("73ec7f3d-d25d-4e1b-94dd-ac98766b410b"), "Auckland", "https://media.istockphoto.com/id/1137079196/photo/auckland-panorama-at-sunrise.jpg?s=1024x1024&w=is&k=20&c=ebyZHJ0BbOh_w4gwzmkgAcfzM_6dvXoZ2D3U1ex_Gpc=", "AKL" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Walks_DifficultyId",
                table: "Walks",
                column: "DifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Walks_RegionId",
                table: "Walks",
                column: "RegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Walks");

            migrationBuilder.DropTable(
                name: "Difficulties");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
