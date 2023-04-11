using Microsoft.EntityFrameworkCore.Migrations;

namespace LSI.HOSP.AlaAllegro.Infrastructure.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOffer_Auctions_AuctionId",
                table: "PurchaseOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOffer_Users_UserId",
                table: "PurchaseOffer");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_PurchaseOffer_AuctionId_UserId",
                table: "PurchaseOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOffer",
                table: "PurchaseOffer");

            migrationBuilder.RenameTable(
                name: "PurchaseOffer",
                newName: "PurchaseOffers");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOffer_UserId",
                table: "PurchaseOffers",
                newName: "IX_PurchaseOffers_UserId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_PurchaseOffers_AuctionId_UserId",
                table: "PurchaseOffers",
                columns: new[] { "AuctionId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOffers",
                table: "PurchaseOffers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOffers_Auctions_AuctionId",
                table: "PurchaseOffers",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOffers_Users_UserId",
                table: "PurchaseOffers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOffers_Auctions_AuctionId",
                table: "PurchaseOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOffers_Users_UserId",
                table: "PurchaseOffers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_PurchaseOffers_AuctionId_UserId",
                table: "PurchaseOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOffers",
                table: "PurchaseOffers");

            migrationBuilder.RenameTable(
                name: "PurchaseOffers",
                newName: "PurchaseOffer");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOffers_UserId",
                table: "PurchaseOffer",
                newName: "IX_PurchaseOffer_UserId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_PurchaseOffer_AuctionId_UserId",
                table: "PurchaseOffer",
                columns: new[] { "AuctionId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOffer",
                table: "PurchaseOffer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOffer_Auctions_AuctionId",
                table: "PurchaseOffer",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOffer_Users_UserId",
                table: "PurchaseOffer",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
