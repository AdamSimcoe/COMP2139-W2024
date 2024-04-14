using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GBC_Travel_Group_42.Migrations
{
    /// <inheritdoc />
    public partial class UserIdAddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "CarRentals",
                schema: "Identity",
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
                schema: "Identity",
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
                schema: "Identity",
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
                name: "Role",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsernameChangeLimit = table.Column<int>(type: "int", nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingCarRentals",
                schema: "Identity",
                columns: table => new
                {
                    BookingCarRentalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                        principalSchema: "Identity",
                        principalTable: "CarRentals",
                        principalColumn: "CarRentalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingCarRentals_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookingFlights",
                schema: "Identity",
                columns: table => new
                {
                    BookingFlightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                        principalSchema: "Identity",
                        principalTable: "Flights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingFlights_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookingHotels",
                schema: "Identity",
                columns: table => new
                {
                    BookingHotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                        principalSchema: "Identity",
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingHotels_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "Identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Identity",
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
                schema: "Identity",
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
                schema: "Identity",
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
                schema: "Identity",
                table: "BookingCarRentals",
                columns: new[] { "BookingCarRentalId", "Accommodations", "CarRentalId", "GuestEmail", "GuestFirstName", "GuestLastName", "RentalDuration", "RentalPickupDateTime", "RentalReturnDateTime", "RentalTotalPrice", "UserId" },
                values: new object[] { 1, "extra seating", 1, "fakeemail3@yes.com", "Adam", "Simcoe", 3, new DateTime(2024, 2, 21, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 24, 10, 30, 0, 0, DateTimeKind.Unspecified), 449.97m, null });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "BookingFlights",
                columns: new[] { "BookingFlightId", "Accommodations", "FlightId", "GuestEmail", "GuestFirstName", "GuestLastName", "UserId" },
                values: new object[] { 1, "Need extra luggage space", 1, "fakeemail@yes.com", "John", "Doe", null });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "BookingHotels",
                columns: new[] { "BookingHotelId", "Accommodations", "CheckInDateTime", "CheckOutDateTime", "ExpectedStayDuration", "GuestEmail", "GuestFirstName", "GuestLastName", "HotelId", "HotelTotalPrice", "UserId" },
                values: new object[] { 1, "Need extra towels", new DateTime(2024, 2, 22, 13, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 25, 13, 30, 0, 0, DateTimeKind.Unspecified), 3, "fakeemail2@yes.com", "Jane", "Doe", 1, 1199.97m, null });

            migrationBuilder.CreateIndex(
                name: "IX_BookingCarRentals_CarRentalId",
                schema: "Identity",
                table: "BookingCarRentals",
                column: "CarRentalId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCarRentals_UserId",
                schema: "Identity",
                table: "BookingCarRentals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingFlights_FlightId",
                schema: "Identity",
                table: "BookingFlights",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingFlights_UserId",
                schema: "Identity",
                table: "BookingFlights",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingHotels_HotelId",
                schema: "Identity",
                table: "BookingHotels",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingHotels_UserId",
                schema: "Identity",
                table: "BookingHotels",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Identity",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "Identity",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Identity",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Identity",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "Identity",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "Identity",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Identity",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingCarRentals",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "BookingFlights",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "BookingHotels",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "CarRentals",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Flights",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Hotels",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Identity");
        }
    }
}
