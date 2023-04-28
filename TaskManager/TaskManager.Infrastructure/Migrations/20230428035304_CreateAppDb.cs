using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateAppDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    FinishedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Remarks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    UserTaskId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    FinishedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Remarks_Tasks_UserTaskId",
                        column: x => x.UserTaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "43e674b8-ae0e-48a0-a231-e1bc5cf85e9c", "Other" },
                    { "4a9861e0-7884-4d4c-9080-b48de9c883ac", "Family" },
                    { "669fe84c-19ce-41f9-ba64-2c844d2b851b", "Personal" },
                    { "9c24c7ac-b86f-4a30-a0fd-bb3ef6e76308", "Job" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "0a7bead2-d075-4100-b803-05498f07347b", "InProgress" },
                    { "2b381501-15b1-488b-96e2-950869d68d79", "Finished" },
                    { "d920dc1b-fab5-40cc-a387-81f7059da658", "Canceled" },
                    { "f83e8ace-ea2c-48f0-846d-1f86c7eb127d", "Awaiting" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "PasswordHash", "Username" },
                values: new object[] { "90b21bc9-9062-4142-b3f9-774e6797e080", "Peter", "Petrov", "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413", "peter123" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "DeletedOn", "Description", "FinishedOn", "IsDeleted", "IsFinished", "ModifiedOn", "StatusId", "UserId" },
                values: new object[] { "23bf9c00-056c-4c72-9fa7-396c28da66c7", "669fe84c-19ce-41f9-ba64-2c844d2b851b", new DateTime(2023, 4, 28, 6, 53, 3, 666, DateTimeKind.Local).AddTicks(3662), null, "Create final project for the WPF course.", null, false, false, null, "0a7bead2-d075-4100-b803-05498f07347b", "90b21bc9-9062-4142-b3f9-774e6797e080" });

            migrationBuilder.InsertData(
                table: "Remarks",
                columns: new[] { "Id", "Content", "CreatedOn", "DeletedOn", "FinishedOn", "IsDeleted", "IsFinished", "ModifiedOn", "UserTaskId" },
                values: new object[,]
                {
                    { "6eb3a493-8160-475f-a4f3-62089765c162", "Use separate projects for database, business logic and WPF application.", new DateTime(2023, 4, 28, 6, 53, 3, 666, DateTimeKind.Local).AddTicks(3376), null, null, false, false, null, "23bf9c00-056c-4c72-9fa7-396c28da66c7" },
                    { "971138d8-d689-4820-8f23-535d1bac1ade", "Create application database with at least 5 database models.", new DateTime(2023, 4, 28, 6, 53, 3, 666, DateTimeKind.Local).AddTicks(3268), null, null, false, false, null, "23bf9c00-056c-4c72-9fa7-396c28da66c7" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_UserTaskId",
                table: "Remarks",
                column: "UserTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CategoryId",
                table: "Tasks",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_StatusId",
                table: "Tasks",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Remarks");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
