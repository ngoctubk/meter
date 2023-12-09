using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeterApp.Migrations
{
    /// <inheritdoc />
    public partial class Initialize_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Errors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Topic = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StallCode = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Payload = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Errors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GasMeters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StallCode = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Cycle = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<double>(type: "double precision", maxLength: 40, nullable: false),
                    Raw = table.Column<double>(type: "double precision", maxLength: 40, nullable: false),
                    Pre = table.Column<double>(type: "double precision", maxLength: 40, nullable: false),
                    Error = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    FromTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ToTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GasMeters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stalls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StallCode = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    UseWaterMeter = table.Column<bool>(type: "boolean", nullable: false),
                    UseGasMeter = table.Column<bool>(type: "boolean", nullable: false),
                    LastWaterMeterId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastWaterMeterDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastWaterMeter = table.Column<double>(type: "double precision", nullable: true),
                    LatestWaterMeterId = table.Column<Guid>(type: "uuid", nullable: true),
                    LatestWaterMeterDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LatestWaterMeter = table.Column<double>(type: "double precision", nullable: true),
                    LastGasMeterId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastGasMeterDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastGasMeter = table.Column<double>(type: "double precision", nullable: true),
                    LatestGasMeterId = table.Column<Guid>(type: "uuid", nullable: true),
                    LatestGasMeterDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LatestGasMeter = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stalls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WaterMeters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StallCode = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Cycle = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<double>(type: "double precision", maxLength: 40, nullable: false),
                    Raw = table.Column<double>(type: "double precision", maxLength: 40, nullable: false),
                    Pre = table.Column<double>(type: "double precision", maxLength: 40, nullable: false),
                    Error = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    FromTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ToTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterMeters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Errors_StallCode",
                table: "Errors",
                column: "StallCode");

            migrationBuilder.CreateIndex(
                name: "IX_Errors_Timestamp",
                table: "Errors",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_GasMeters_Cycle",
                table: "GasMeters",
                column: "Cycle");

            migrationBuilder.CreateIndex(
                name: "IX_GasMeters_FromTimestamp",
                table: "GasMeters",
                column: "FromTimestamp");

            migrationBuilder.CreateIndex(
                name: "IX_GasMeters_StallCode",
                table: "GasMeters",
                column: "StallCode");

            migrationBuilder.CreateIndex(
                name: "IX_GasMeters_ToTimestamp",
                table: "GasMeters",
                column: "ToTimestamp");

            migrationBuilder.CreateIndex(
                name: "IX_GasMeters_Value",
                table: "GasMeters",
                column: "Value");

            migrationBuilder.CreateIndex(
                name: "IX_Stalls_Name",
                table: "Stalls",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Stalls_StallCode",
                table: "Stalls",
                column: "StallCode");

            migrationBuilder.CreateIndex(
                name: "IX_WaterMeters_Cycle",
                table: "WaterMeters",
                column: "Cycle");

            migrationBuilder.CreateIndex(
                name: "IX_WaterMeters_FromTimestamp",
                table: "WaterMeters",
                column: "FromTimestamp");

            migrationBuilder.CreateIndex(
                name: "IX_WaterMeters_StallCode",
                table: "WaterMeters",
                column: "StallCode");

            migrationBuilder.CreateIndex(
                name: "IX_WaterMeters_ToTimestamp",
                table: "WaterMeters",
                column: "ToTimestamp");

            migrationBuilder.CreateIndex(
                name: "IX_WaterMeters_Value",
                table: "WaterMeters",
                column: "Value");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Errors");

            migrationBuilder.DropTable(
                name: "GasMeters");

            migrationBuilder.DropTable(
                name: "Stalls");

            migrationBuilder.DropTable(
                name: "WaterMeters");
        }
    }
}
