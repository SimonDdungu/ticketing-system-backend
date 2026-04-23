using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing_backend.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Order_User_Event_models_april_23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionID",
                table: "Orders",
                newName: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Orders",
                newName: "TransactionID");
        }
    }
}
