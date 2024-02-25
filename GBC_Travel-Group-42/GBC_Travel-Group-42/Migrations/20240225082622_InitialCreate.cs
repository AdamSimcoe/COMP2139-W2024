using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GBC_Travel_Group_42.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarRentals",
                columns: table => new
                {
                    CarRentalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarRentalMake = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    MaximumOccupancy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRentals", x => x.CarRentalId);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    MaximumOccupancy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    MaximumOccupancy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.HotelId);
                });

            migrationBuilder.CreateTable(
                name: "BookingCarRentals",
                columns: table => new
                {
                    BookingCarRentalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuestLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuestEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accommodations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarRentalId = table.Column<int>(type: "int", nullable: false),
                    RentalPickupDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RentalDuration = table.Column<int>(type: "int", nullable: false),
                    RentalReturnDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RentalTotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingCarRentals", x => x.BookingCarRentalId);
                    table.ForeignKey(
                        name: "FK_BookingCarRentals_CarRentals_CarRentalId",
                        column: x => x.CarRentalId,
                        principalTable: "CarRentals",
                        principalColumn: "CarRentalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingFlights",
                columns: table => new
                {
                    BookingFlightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuestLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuestEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accommodations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingFlights", x => x.BookingFlightId);
                    table.ForeignKey(
                        name: "FK_BookingFlights_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingHotels",
                columns: table => new
                {
                    BookingHotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuestLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuestEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accommodations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    CheckInDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectedStayDuration = table.Column<int>(type: "int", nullable: false),
                    CheckOutDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HotelTotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingHotels", x => x.BookingHotelId);
                    table.ForeignKey(
                        name: "FK_BookingHotels_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarRentals",
                columns: new[] { "CarRentalId", "CarRentalMake", "MaximumOccupancy", "PricePerDay", "Rating" },
                values: new object[,]
                {
                    { 1, "2024 Nissan Rogue", 2, 149.99m, 5 },
                    { 2, "2019 Chevrolet Malibu", 2, 199.99m, 5 },
                    { 3, "2024 Toyota Sienna", 2, 99.99m, 4 },
                    { 4, "2024 Mitsubishi Mirage", 2, 79.99m, 4 },
                    { 5, "2019 Toyota Corolla", 2, 49.99m, 3 },
                    { 6, "2024 Ford Transit Van", 2, 59.99m, 3 },
                    { 7, "2021 Nissan NV200", 2, 69.99m, 2 },
                    { 8, "2024 Ford Edge", 2, 129.99m, 2 },
                    { 9, "2024 Audi Q3", 2, 119.99m, 1 },
                    { 10, "2023 Chevrolet Express", 2, 89.99m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "ArrivalDateTime", "DepartureDateTime", "Destination", "FlightNumber", "MaximumOccupancy", "Origin", "Price", "Rating" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 1, 22, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 1, 11, 30, 0, 0, DateTimeKind.Unspecified), "Tokyo, JP", "KH734", 2, "Toronto, CA", 1349.99m, 5 },
                    { 2, new DateTime(2024, 3, 2, 1, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 1, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sydney, AU", "KH767", 2, "Montreal, CA", 1299.99m, 4 },
                    { 3, new DateTime(2024, 3, 2, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 2, 13, 30, 0, 0, DateTimeKind.Unspecified), "Los Angeles, US", "KF647", 2, "London, UK", 899.99m, 3 },
                    { 4, new DateTime(2024, 3, 2, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 2, 7, 0, 0, 0, DateTimeKind.Unspecified), "Paris, FR", "KF867", 2, "New York City, US", 1099.99m, 5 },
                    { 5, new DateTime(2024, 3, 3, 20, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 3, 8, 30, 0, 0, DateTimeKind.Unspecified), "Hong Kong, CN", "KR765", 2, "Athens, GR", 799.99m, 2 },
                    { 6, new DateTime(2024, 3, 3, 14, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 3, 6, 30, 0, 0, DateTimeKind.Unspecified), "New York City, US", "KR456", 2, "Tokyo, JP", 859.99m, 2 },
                    { 7, new DateTime(2024, 3, 5, 3, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 4, 12, 30, 0, 0, DateTimeKind.Unspecified), "London, UK", "KG699", 2, "Hong Kong, CN", 749.99m, 1 },
                    { 8, new DateTime(2024, 3, 4, 15, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 4, 10, 30, 0, 0, DateTimeKind.Unspecified), "Montreal, CA", "KG921", 2, "Los Angeles, US", 699.99m, 1 },
                    { 9, new DateTime(2024, 3, 5, 18, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 5, 13, 30, 0, 0, DateTimeKind.Unspecified), "Toronto, CA", "KK432", 2, "Paris, FR", 1349.99m, 3 },
                    { 10, new DateTime(2024, 3, 6, 1, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 5, 11, 30, 0, 0, DateTimeKind.Unspecified), "Athens, GR", "KK420", 2, "Sydney, AU", 1349.99m, 4 }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "HotelId", "HotelLocation", "HotelName", "MaximumOccupancy", "PricePerNight", "Rating" },
                values: new object[,]
                {
                    { 1, "Ag. Dimitriou 18, Athina 105 54, Greece", "Arion Athens Hotel", 2, 399.99m, 4 },
                    { 2, "7 Rue de la Verrerie, 75004 Paris, France", "Hotel Le Grand Mazarin", 2, 499.99m, 5 },
                    { 3, "4-1 Kioicho, Chiyoda City, Tokyo 102-8578, Japan", "Hotel New Otani Tokyo", 2, 549.99m, 5 },
                    { 4, "161 Elizabeth St, Sydney NSW 2000, Australia", "Sheraton Grand Sydney Hyde Park", 2, 349.99m, 4 },
                    { 5, "234 W 42nd St, New York, NY 10036, United States", "Hilton New York Times Square", 2, 449.99m, 3 },
                    { 6, "23 Austin Ave, Tsim Sha Tsui, Hong Kong, China", "Ramada Hong Kong Grand", 2, 399.99m, 3 },
                    { 7, "1 Lancaster Gate, London W2 3LG, United Kingdom", "Corus Hotel Hyde Park", 2, 299.99m, 2 },
                    { 8, "2645 Av. Desjardins, Montréal, QC H1V 2H8, Canada", "Gite du Survenant Montreal", 2, 249.99m, 2 },
                    { 9, "1 King St W, Toronto, ON M5H 1A1, Canada", "One King West Hotel & Residence", 2, 399.99m, 1 },
                    { 10, "9750 Airport Blvd, Los Angeles, CA 90045, United States", "Four Points by Sheraton LA International Airport", 2, 299.99m, 1 }
                });

            migrationBuilder.InsertData(
                table: "BookingCarRentals",
                columns: new[] { "BookingCarRentalId", "Accommodations", "CarRentalId", "GuestEmail", "GuestFirstName", "GuestLastName", "RentalDuration", "RentalPickupDateTime", "RentalReturnDateTime", "RentalTotalPrice" },
                values: new object[] { 1, "extra seating", 1, "fakeemail3@yes.com", "Adam", "Simcoe", 3, new DateTime(2024, 2, 21, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 24, 10, 30, 0, 0, DateTimeKind.Unspecified), 449.97m });

            migrationBuilder.InsertData(
                table: "BookingFlights",
                columns: new[] { "BookingFlightId", "Accommodations", "FlightId", "GuestEmail", "GuestFirstName", "GuestLastName" },
                values: new object[] { 1, "Need extra luggage space", 1, "fakeemail@yes.com", "John", "Doe" });

            migrationBuilder.InsertData(
                table: "BookingHotels",
                columns: new[] { "BookingHotelId", "Accommodations", "CheckInDateTime", "CheckOutDateTime", "ExpectedStayDuration", "GuestEmail", "GuestFirstName", "GuestLastName", "HotelId", "HotelTotalPrice" },
                values: new object[] { 1, "Need extra towels", new DateTime(2024, 2, 22, 13, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 25, 13, 30, 0, 0, DateTimeKind.Unspecified), 3, "fakeemail2@yes.com", "Jane", "Doe", 1, 1199.97m });

            migrationBuilder.CreateIndex(
                name: "IX_BookingCarRentals_CarRentalId",
                table: "BookingCarRentals",
                column: "CarRentalId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingFlights_FlightId",
                table: "BookingFlights",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingHotels_HotelId",
                table: "BookingHotels",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingCarRentals");

            migrationBuilder.DropTable(
                name: "BookingFlights");

            migrationBuilder.DropTable(
                name: "BookingHotels");

            migrationBuilder.DropTable(
                name: "CarRentals");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Hotels");
        }
    }
}
