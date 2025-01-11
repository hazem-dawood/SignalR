using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SignalR.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Auth");

            migrationBuilder.EnsureSchema(
                name: "Chats");

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_ApplicationUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalSchema: "Auth",
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OnlineUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnlineUsers_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserChats",
                schema: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ToUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserChats_ApplicationUsers_ToUserId",
                        column: x => x.ToUserId,
                        principalSchema: "Auth",
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChats_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "GroupMessages",
                schema: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupMessages_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMessages_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "Chats",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "GroupUsers",
                schema: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupUsers_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUsers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "Chats",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserChatMessages",
                schema: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserChatId = table.Column<int>(type: "int", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserChatMessages_ApplicationUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalSchema: "Auth",
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChatMessages_UserChats_UserChatId",
                        column: x => x.UserChatId,
                        principalSchema: "Chats",
                        principalTable: "UserChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "ApplicationUsers",
                columns: new[] { "Id", "CreatedDate", "FullName", "ImageUrl", "IsDeleted", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hazem Dawood", "https://bootdey.com/img/Content/avatar/avatar2.png", false, "123456", "hazem" },
                    { 2, new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ahmed Emad", "https://bootdey.com/img/Content/avatar/avatar1.png", false, "123456", "ahmed" },
                    { 3, new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kareem Belal", "https://bootdey.com/img/Content/avatar/avatar4.png", false, "123456", "kareem" },
                    { 4, new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mahmoud Bahrawy", "https://bootdey.com/img/Content/avatar/avatar7.png", false, "123456", "bahrawy" }
                });

            migrationBuilder.InsertData(
                table: "OnlineUsers",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "LastModified", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessages_GroupId",
                schema: "Chats",
                table: "GroupMessages",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessages_UserId",
                schema: "Chats",
                table: "GroupMessages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreatedUserId",
                schema: "Chats",
                table: "Groups",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_GroupId",
                schema: "Chats",
                table: "GroupUsers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_UserId",
                schema: "Chats",
                table: "GroupUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineUsers_UserId",
                table: "OnlineUsers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserChatMessages_CreatedUserId",
                schema: "Chats",
                table: "UserChatMessages",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChatMessages_UserChatId",
                schema: "Chats",
                table: "UserChatMessages",
                column: "UserChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_ToUserId",
                schema: "Chats",
                table: "UserChats",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_UserId",
                schema: "Chats",
                table: "UserChats",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupMessages",
                schema: "Chats");

            migrationBuilder.DropTable(
                name: "GroupUsers",
                schema: "Chats");

            migrationBuilder.DropTable(
                name: "OnlineUsers");

            migrationBuilder.DropTable(
                name: "UserChatMessages",
                schema: "Chats");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "Chats");

            migrationBuilder.DropTable(
                name: "UserChats",
                schema: "Chats");

            migrationBuilder.DropTable(
                name: "ApplicationUsers",
                schema: "Auth");
        }
    }
}
