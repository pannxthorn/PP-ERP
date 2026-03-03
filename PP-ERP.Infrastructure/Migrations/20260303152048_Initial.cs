using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PP_ERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COMPANY",
                columns: table => new
                {
                    COMPANY_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COMPANY_CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    COMPANY_NAME = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TAX_NO = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    PHONE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FAX = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LINE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FACEBOOK = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WEBSITE = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    LOGO = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    COMMENT = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    CREATED_BY_ID = table.Column<int>(type: "int", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LAST_UPDATE_ID = table.Column<int>(type: "int", nullable: false),
                    LAST_UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ROW_UN = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPANY", x => x.COMPANY_ID);
                });

            migrationBuilder.CreateTable(
                name: "FLEX",
                columns: table => new
                {
                    FLEX_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FLEX_CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FLEX_NAME = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    CREATED_BY_ID = table.Column<int>(type: "int", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LAST_UPDATE_ID = table.Column<int>(type: "int", nullable: false),
                    LAST_UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ROW_UN = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FLEX", x => x.FLEX_ID);
                });

            migrationBuilder.CreateTable(
                name: "BRANCH",
                columns: table => new
                {
                    BRANCH_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COMPANY_ID = table.Column<int>(type: "int", nullable: false),
                    BRANCH_CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BRANCH_NAME = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PHONE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LINE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FACEBOOK = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    COMMENT = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    IS_HEADQUARTER = table.Column<bool>(type: "bit", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    CREATED_BY_ID = table.Column<int>(type: "int", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LAST_UPDATE_ID = table.Column<int>(type: "int", nullable: false),
                    LAST_UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ROW_UN = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BRANCH", x => x.BRANCH_ID);
                    table.ForeignKey(
                        name: "FK_BRANCH_COMPANY_COMPANY_ID",
                        column: x => x.COMPANY_ID,
                        principalTable: "COMPANY",
                        principalColumn: "COMPANY_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SYS_USER",
                columns: table => new
                {
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COMPANY_ID = table.Column<int>(type: "int", nullable: true),
                    BRANCH_ID = table.Column<int>(type: "int", nullable: true),
                    USERNAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    COMMENT = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    CREATED_BY_ID = table.Column<int>(type: "int", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LAST_UPDATE_ID = table.Column<int>(type: "int", nullable: false),
                    LAST_UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ROW_UN = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_USER", x => x.USER_ID);
                    table.ForeignKey(
                        name: "FK_SYS_USER_COMPANY_COMPANY_ID",
                        column: x => x.COMPANY_ID,
                        principalTable: "COMPANY",
                        principalColumn: "COMPANY_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FLEX_ITEM",
                columns: table => new
                {
                    FLEX_ITEM_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FLEX_ID = table.Column<int>(type: "int", nullable: false),
                    FLEX_ITEM_CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FLEX_ITEM_NAME = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    CREATED_BY_ID = table.Column<int>(type: "int", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LAST_UPDATE_ID = table.Column<int>(type: "int", nullable: false),
                    LAST_UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ROW_UN = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FLEX_ITEM", x => x.FLEX_ITEM_ID);
                    table.ForeignKey(
                        name: "FK_FLEX_ITEM_FLEX_FLEX_ID",
                        column: x => x.FLEX_ID,
                        principalTable: "FLEX",
                        principalColumn: "FLEX_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BRANCH_COMPANY_ID",
                table: "BRANCH",
                column: "COMPANY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLEX_FLEX_CODE",
                table: "FLEX",
                column: "FLEX_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_FLEX_ITEM_FLEX_ID",
                table: "FLEX_ITEM",
                column: "FLEX_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLEX_ITEM_FLEX_ITEM_CODE",
                table: "FLEX_ITEM",
                column: "FLEX_ITEM_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_USER_COMPANY_ID",
                table: "SYS_USER",
                column: "COMPANY_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BRANCH");

            migrationBuilder.DropTable(
                name: "FLEX_ITEM");

            migrationBuilder.DropTable(
                name: "SYS_USER");

            migrationBuilder.DropTable(
                name: "FLEX");

            migrationBuilder.DropTable(
                name: "COMPANY");
        }
    }
}
