using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AutoParts.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: false),
                    YearInProductionSince = table.Column<int>(type: "int", nullable: false),
                    YearInProductionTill = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarModels_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartCategoryId = table.Column<int>(type: "int", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OemNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parts_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parts_PartCategories_PartCategoryId",
                        column: x => x.PartCategoryId,
                        principalTable: "PartCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartCompatibilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    CarModelId = table.Column<int>(type: "int", nullable: false),
                    PartCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartCompatibilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartCompatibilities_CarModels_CarModelId",
                        column: x => x.CarModelId,
                        principalTable: "CarModels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PartCompatibilities_PartCategories_PartCategoryId",
                        column: x => x.PartCategoryId,
                        principalTable: "PartCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PartCompatibilities_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "BMW" },
                    { 2, "Volkswagen" },
                    { 3, "Toyota" },
                    { 4, "Ford" },
                    { 5, "Audi" }
                });

            migrationBuilder.InsertData(
                table: "PartCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Brakes" },
                    { 2, "Engine" },
                    { 3, "Filters" },
                    { 4, "Suspension" },
                    { 5, "Electrical" },
                    { 6, "Exhaust" },
                    { 7, "Transmission" },
                    { 8, "Cooling" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "ManufacturerId", "Name", "YearInProductionSince", "YearInProductionTill" },
                values: new object[,]
                {
                    { 1, 1, "E46 320i", 1998, 2005 },
                    { 2, 1, "E90 320d", 2005, 2012 },
                    { 3, 2, "Golf Mk5", 2003, 2009 },
                    { 4, 2, "Passat B6", 2005, 2010 },
                    { 5, 3, "Corolla E120", 2001, 2007 },
                    { 6, 3, "Camry XV50", 2011, 2017 },
                    { 7, 4, "Focus Mk2", 2004, 2011 },
                    { 8, 4, "Mondeo Mk4", 2007, 2014 },
                    { 9, 5, "A4 B7", 2004, 2008 },
                    { 10, 5, "A6 C6", 2004, 2011 }
                });

            migrationBuilder.InsertData(
                table: "Parts",
                columns: new[] { "Id", "Description", "ManufacturerId", "Name", "OemNumber", "PartCategoryId", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "OEM front brake pads for BMW E46", 1, "Front Brake Pads", "34116761244", 1, 28.90m, 15 },
                    { 2, "OEM rear brake pads for BMW E46", 1, "Rear Brake Pads", "34216761245", 1, 24.50m, 12 },
                    { 3, "Front brake disc 300mm", 1, "Front Brake Disc", "34116864056", 1, 45.00m, 8 },
                    { 4, "Front left brake caliper", 1, "Brake Caliper Front Left", "34116750418", 1, 89.99m, 4 },
                    { 5, "Front brake pads for VW Golf Mk5", 2, "Front Brake Pads", "1K0698151B", 1, 22.50m, 20 },
                    { 6, "Rear brake disc for VW Golf Mk5", 2, "Rear Brake Disc", "1K0615601N", 1, 38.00m, 10 },
                    { 7, "Front brake pads for Toyota Corolla", 3, "Front Brake Pads", "04465-02200", 1, 19.90m, 25 },
                    { 8, "Front brake pads for Ford Focus Mk2", 4, "Front Brake Pads", "1229912", 1, 21.00m, 18 },
                    { 9, "Valve cover gasket for BMW E46 M54", 1, "Valve Cover Gasket", "11127512839", 2, 18.50m, 9 },
                    { 10, "Complete timing chain kit", 1, "Timing Chain Kit", "11311748051", 2, 120.00m, 5 },
                    { 11, "Intake manifold gasket for VW 1.8T", 2, "Intake Manifold Gasket", "058129717G", 2, 14.00m, 11 },
                    { 12, "Rear crankshaft seal for Toyota", 3, "Crankshaft Seal", "04465-33040", 2, 9.90m, 14 },
                    { 13, "Engine oil filter for BMW E46", 1, "Oil Filter", "11427512300", 3, 8.50m, 30 },
                    { 14, "Air filter for BMW E46 320i", 1, "Air Filter", "13721730449", 3, 12.00m, 22 },
                    { 15, "Cabin pollen filter for BMW E46", 1, "Cabin Air Filter", "64316823725", 3, 15.00m, 17 },
                    { 16, "Oil filter for VW Golf Mk5", 2, "Oil Filter", "1K0115562", 3, 7.20m, 35 },
                    { 17, "Air filter for VW Golf Mk5", 2, "Air Filter", "1K0129620", 3, 11.00m, 28 },
                    { 18, "Oil filter for Toyota Corolla", 3, "Oil Filter", "04152-YZZA6", 3, 6.90m, 40 },
                    { 19, "Fuel filter for Ford Focus Mk2", 4, "Fuel Filter", "1229894", 3, 13.50m, 16 },
                    { 20, "Front shock absorber for BMW E46", 1, "Front Shock Absorber", "31316750417", 4, 75.00m, 6 },
                    { 21, "Rear shock absorber for BMW E46", 1, "Rear Shock Absorber", "33521091867", 4, 65.00m, 7 },
                    { 22, "Front lower control arm", 1, "Front Control Arm", "31126758510", 4, 55.00m, 8 },
                    { 23, "Front shock absorber for VW Golf Mk5", 2, "Front Shock Absorber", "1K0413031BE", 4, 68.00m, 9 },
                    { 24, "Front stabilizer link for Toyota", 3, "Stabilizer Link", "48820-02030", 4, 18.00m, 13 },
                    { 25, "Alternator 140A for BMW E46", 1, "Alternator", "12317501886", 5, 145.00m, 4 },
                    { 26, "Starter motor for BMW E46", 1, "Starter Motor", "12417527229", 5, 110.00m, 3 },
                    { 27, "Ignition coil for BMW E46 320i", 1, "Ignition Coil", "12131703228", 5, 32.00m, 20 },
                    { 28, "Ignition coil for VW Golf Mk5", 2, "Ignition Coil", "07K905715F", 5, 28.00m, 22 },
                    { 29, "Catalytic converter for BMW E46", 1, "Catalytic Converter", "18307503816", 6, 220.00m, 3 },
                    { 30, "Exhaust manifold gasket", 1, "Exhaust Manifold Gasket", "11621437567", 6, 16.00m, 10 },
                    { 31, "Rear silencer for VW Golf Mk5", 2, "Rear Silencer", "1K0253609AK", 6, 95.00m, 5 },
                    { 32, "Complete clutch kit for BMW E46", 1, "Clutch Kit", "21207526519", 7, 185.00m, 4 },
                    { 33, "MTF-LT-3 gearbox oil 1L", 1, "Gearbox Oil", "83222282583", 7, 22.00m, 25 },
                    { 34, "Clutch kit for VW Golf Mk5", 2, "Clutch Kit", "02T141015D", 7, 160.00m, 5 },
                    { 35, "Coolant radiator for BMW E46", 1, "Radiator", "17117561757", 8, 130.00m, 4 },
                    { 36, "Water pump for BMW E46 M54", 1, "Water Pump", "11517527799", 8, 55.00m, 7 },
                    { 37, "Coolant thermostat for BMW E46", 1, "Thermostat", "11531437040", 8, 25.00m, 10 },
                    { 38, "Radiator for VW Golf Mk5", 2, "Radiator", "1K0121251AT", 8, 110.00m, 5 }
                });

            migrationBuilder.InsertData(
                table: "PartCompatibilities",
                columns: new[] { "Id", "CarModelId", "PartCategoryId", "PartId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 1, 1, 2 },
                    { 3, 1, 1, 3 },
                    { 4, 1, 1, 4 },
                    { 5, 1, 2, 9 },
                    { 6, 1, 2, 10 },
                    { 7, 1, 3, 13 },
                    { 8, 1, 3, 14 },
                    { 9, 1, 3, 15 },
                    { 10, 1, 4, 20 },
                    { 11, 1, 4, 21 },
                    { 12, 1, 4, 22 },
                    { 13, 1, 5, 25 },
                    { 14, 1, 5, 26 },
                    { 15, 1, 5, 27 },
                    { 16, 1, 6, 29 },
                    { 17, 1, 6, 30 },
                    { 18, 1, 7, 32 },
                    { 19, 1, 7, 33 },
                    { 20, 1, 8, 35 },
                    { 21, 1, 8, 36 },
                    { 22, 1, 8, 37 },
                    { 23, 2, 1, 1 },
                    { 24, 2, 1, 2 },
                    { 25, 2, 1, 3 },
                    { 26, 2, 3, 13 },
                    { 27, 2, 3, 14 },
                    { 28, 2, 4, 20 },
                    { 29, 2, 4, 21 },
                    { 30, 2, 8, 35 },
                    { 31, 2, 8, 36 },
                    { 32, 3, 1, 5 },
                    { 33, 3, 1, 6 },
                    { 34, 3, 2, 11 },
                    { 35, 3, 3, 16 },
                    { 36, 3, 3, 17 },
                    { 37, 3, 4, 23 },
                    { 38, 3, 5, 28 },
                    { 39, 3, 6, 31 },
                    { 40, 3, 7, 34 },
                    { 41, 3, 8, 38 },
                    { 42, 4, 1, 5 },
                    { 43, 4, 1, 6 },
                    { 44, 4, 2, 11 },
                    { 45, 4, 3, 16 },
                    { 46, 4, 3, 17 },
                    { 47, 4, 4, 23 },
                    { 48, 4, 8, 38 },
                    { 49, 5, 1, 7 },
                    { 50, 5, 2, 12 },
                    { 51, 5, 3, 18 },
                    { 52, 5, 4, 24 },
                    { 53, 6, 1, 7 },
                    { 54, 6, 2, 12 },
                    { 55, 6, 3, 18 },
                    { 56, 6, 4, 24 },
                    { 57, 7, 1, 8 },
                    { 58, 7, 3, 19 },
                    { 59, 8, 1, 8 },
                    { 60, 8, 3, 19 },
                    { 61, 9, 1, 1 },
                    { 62, 9, 1, 2 },
                    { 63, 9, 2, 11 },
                    { 64, 9, 3, 13 },
                    { 65, 9, 4, 23 },
                    { 66, 9, 5, 28 },
                    { 67, 10, 1, 1 },
                    { 68, 10, 1, 2 },
                    { 69, 10, 2, 11 },
                    { 70, 10, 3, 13 },
                    { 71, 10, 4, 23 },
                    { 72, 10, 8, 38 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarModels_ManufacturerId",
                table: "CarModels",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_PartCompatibilities_CarModelId",
                table: "PartCompatibilities",
                column: "CarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PartCompatibilities_PartCategoryId",
                table: "PartCompatibilities",
                column: "PartCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PartCompatibilities_PartId",
                table: "PartCompatibilities",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_ManufacturerId",
                table: "Parts",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_PartCategoryId",
                table: "Parts",
                column: "PartCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartCompatibilities");

            migrationBuilder.DropTable(
                name: "CarModels");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "PartCategories");
        }
    }
}
