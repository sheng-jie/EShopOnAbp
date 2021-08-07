using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopOnAbp.Migrations
{
    public partial class Add_Vip_AggregateRoot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vips",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(36)",maxLength: 36, nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VipScoreRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VipId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    RecordType = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    RecordStatus = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Before = table.Column<int>(type: "int", nullable: false),
                    Changed = table.Column<int>(type: "int", nullable: false),
                    After = table.Column<int>(type: "int", nullable: false),
                    Left = table.Column<int>(type: "int", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VipScoreRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VipScoreRecords_Vips_VipId",
                        column: x => x.VipId,
                        principalTable: "Vips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vips_CustomerId",
                table: "Vips",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VipScoreRecords_VipId",
                table: "VipScoreRecords",
                column: "VipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "VipScoreRecords");

            migrationBuilder.DropTable(
                name: "Vips");
        }
    }
}
