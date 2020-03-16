using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightManager.Data.Migrations
{
    public partial class fixingflights : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessClassCapacity",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "TakeOffDateTime",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "TakeOffLocation",
                table: "Flights");

            migrationBuilder.AddColumn<int>(
                name: "BusinessSeats",
                table: "Flights",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BusinessSeatsLeft",
                table: "Flights",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureDateTime",
                table: "Flights",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DepartureLocation",
                table: "Flights",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegularSeats",
                table: "Flights",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RegularSeatsLeft",
                table: "Flights",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessSeats",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "BusinessSeatsLeft",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DepartureDateTime",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DepartureLocation",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "RegularSeats",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "RegularSeatsLeft",
                table: "Flights");

            migrationBuilder.AddColumn<int>(
                name: "BusinessClassCapacity",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TakeOffDateTime",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TakeOffLocation",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
