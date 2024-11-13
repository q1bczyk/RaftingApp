using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_EquipmentType_EquipmentTypeId",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Reservation_ReservationId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationEquipment_Equipment_EquipmentId",
                table: "ReservationEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationEquipment_Reservation_ReservationId",
                table: "ReservationEquipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationEquipment",
                table: "ReservationEquipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentType",
                table: "EquipmentType");

            migrationBuilder.RenameTable(
                name: "ReservationEquipment",
                newName: "ReservationsEquipment");

            migrationBuilder.RenameTable(
                name: "Reservation",
                newName: "Reservations");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "EquipmentType",
                newName: "EquipmentTypes");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationEquipment_ReservationId",
                table: "ReservationsEquipment",
                newName: "IX_ReservationsEquipment_ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationEquipment_EquipmentId",
                table: "ReservationsEquipment",
                newName: "IX_ReservationsEquipment_EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_ReservationId",
                table: "Payments",
                newName: "IX_Payments_ReservationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationsEquipment",
                table: "ReservationsEquipment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentTypes",
                table: "EquipmentTypes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    DiscountName = table.Column<string>(type: "text", nullable: false),
                    DiscountDescription = table.Column<string>(type: "text", nullable: false),
                    DicountType = table.Column<string>(type: "text", nullable: false),
                    DiscountValue = table.Column<int>(type: "integer", nullable: false),
                    MinCondition = table.Column<int>(type: "integer", nullable: true),
                    MaxCondition = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    HoursRentalTime = table.Column<int>(type: "integer", nullable: false),
                    SeasonStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SeasonEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DayEarliestBookingTime = table.Column<int>(type: "integer", nullable: false),
                    DayLatestBookingTime = table.Column<int>(type: "integer", nullable: false),
                    OpeningTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CloseTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_EquipmentTypes_EquipmentTypeId",
                table: "Equipment",
                column: "EquipmentTypeId",
                principalTable: "EquipmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Reservations_ReservationId",
                table: "Payments",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationsEquipment_Equipment_EquipmentId",
                table: "ReservationsEquipment",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationsEquipment_Reservations_ReservationId",
                table: "ReservationsEquipment",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_EquipmentTypes_EquipmentTypeId",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Reservations_ReservationId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationsEquipment_Equipment_EquipmentId",
                table: "ReservationsEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationsEquipment_Reservations_ReservationId",
                table: "ReservationsEquipment");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationsEquipment",
                table: "ReservationsEquipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentTypes",
                table: "EquipmentTypes");

            migrationBuilder.RenameTable(
                name: "ReservationsEquipment",
                newName: "ReservationEquipment");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "Reservation");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameTable(
                name: "EquipmentTypes",
                newName: "EquipmentType");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationsEquipment_ReservationId",
                table: "ReservationEquipment",
                newName: "IX_ReservationEquipment_ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationsEquipment_EquipmentId",
                table: "ReservationEquipment",
                newName: "IX_ReservationEquipment_EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_ReservationId",
                table: "Payment",
                newName: "IX_Payment_ReservationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationEquipment",
                table: "ReservationEquipment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentType",
                table: "EquipmentType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_EquipmentType_EquipmentTypeId",
                table: "Equipment",
                column: "EquipmentTypeId",
                principalTable: "EquipmentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Reservation_ReservationId",
                table: "Payment",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationEquipment_Equipment_EquipmentId",
                table: "ReservationEquipment",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationEquipment_Reservation_ReservationId",
                table: "ReservationEquipment",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
