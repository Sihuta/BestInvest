using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestInvest.API.Migrations
{
    public partial class ReverseFKAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountsInfo_AccountInfoId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountInfoId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountInfoId",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "AccountsInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AccountsInfo_AccountId",
                table: "AccountsInfo",
                column: "AccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsInfo_Accounts_AccountId",
                table: "AccountsInfo",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsInfo_Accounts_AccountId",
                table: "AccountsInfo");

            migrationBuilder.DropIndex(
                name: "IX_AccountsInfo_AccountId",
                table: "AccountsInfo");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "AccountsInfo");

            migrationBuilder.AddColumn<int>(
                name: "AccountInfoId",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountInfoId",
                table: "Accounts",
                column: "AccountInfoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountsInfo_AccountInfoId",
                table: "Accounts",
                column: "AccountInfoId",
                principalTable: "AccountsInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
