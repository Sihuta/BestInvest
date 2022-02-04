using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestInvest.API.Migrations
{
    public partial class RenameAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_UsersInfo_UserInfoId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Accounts_UserId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Accounts_UserId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestorCategories_Accounts_UserId",
                table: "InvestorCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Accounts_UserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Accounts_UserId",
                table: "TeamMembers");

            migrationBuilder.DropTable(
                name: "UsersInfo");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TeamMembers",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMembers_UserId",
                table: "TeamMembers",
                newName: "IX_TeamMembers_AccountId");

            migrationBuilder.RenameColumn(
                name: "BusinessPlanPdf",
                table: "Projects",
                newName: "BusinessPlanFilePath");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Messages",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                newName: "IX_Messages_AccountId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "InvestorCategories",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_InvestorCategories_UserId",
                table: "InvestorCategories",
                newName: "IX_InvestorCategories_AccountId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Deals",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Deals_UserId",
                table: "Deals",
                newName: "IX_Deals_AccountId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Chats",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_UserId",
                table: "Chats",
                newName: "IX_Chats_AccountId");

            migrationBuilder.RenameColumn(
                name: "UserInfoId",
                table: "Accounts",
                newName: "AccountInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_UserInfoId",
                table: "Accounts",
                newName: "IX_Accounts_AccountInfoId");

            migrationBuilder.CreateTable(
                name: "AccountsInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkingExperience = table.Column<int>(type: "int", nullable: false),
                    LinkedIn = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsInfo", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountsInfo_AccountInfoId",
                table: "Accounts",
                column: "AccountInfoId",
                principalTable: "AccountsInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Accounts_AccountId",
                table: "Chats",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Accounts_AccountId",
                table: "Deals",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestorCategories_Accounts_AccountId",
                table: "InvestorCategories",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Accounts_AccountId",
                table: "Messages",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Accounts_AccountId",
                table: "TeamMembers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountsInfo_AccountInfoId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Accounts_AccountId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Accounts_AccountId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestorCategories_Accounts_AccountId",
                table: "InvestorCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Accounts_AccountId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Accounts_AccountId",
                table: "TeamMembers");

            migrationBuilder.DropTable(
                name: "AccountsInfo");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "TeamMembers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMembers_AccountId",
                table: "TeamMembers",
                newName: "IX_TeamMembers_UserId");

            migrationBuilder.RenameColumn(
                name: "BusinessPlanFilePath",
                table: "Projects",
                newName: "BusinessPlanPdf");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Messages",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_AccountId",
                table: "Messages",
                newName: "IX_Messages_UserId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "InvestorCategories",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_InvestorCategories_AccountId",
                table: "InvestorCategories",
                newName: "IX_InvestorCategories_UserId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Deals",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Deals_AccountId",
                table: "Deals",
                newName: "IX_Deals_UserId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Chats",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_AccountId",
                table: "Chats",
                newName: "IX_Chats_UserId");

            migrationBuilder.RenameColumn(
                name: "AccountInfoId",
                table: "Accounts",
                newName: "UserInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_AccountInfoId",
                table: "Accounts",
                newName: "IX_Accounts_UserInfoId");

            migrationBuilder.CreateTable(
                name: "UsersInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkedIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkingExperience = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersInfo", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_UsersInfo_UserInfoId",
                table: "Accounts",
                column: "UserInfoId",
                principalTable: "UsersInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Accounts_UserId",
                table: "Chats",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Accounts_UserId",
                table: "Deals",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestorCategories_Accounts_UserId",
                table: "InvestorCategories",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Accounts_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Accounts_UserId",
                table: "TeamMembers",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
