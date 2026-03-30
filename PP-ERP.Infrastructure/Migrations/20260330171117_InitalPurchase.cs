using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PP_ERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitalPurchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VENDOR",
                columns: table => new
                {
                    VENDOR_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VENDOR_CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VENDOR_NAME = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    VENDOR_NAME_ENG = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    VENDOR_FULLNAME = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    VENDOR_FULLNAME_ENG = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    TITLE_ID = table.Column<int>(type: "int", nullable: true),
                    COMMENTS = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
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
                    table.PrimaryKey("PK_VENDOR", x => x.VENDOR_ID);
                    table.ForeignKey(
                        name: "FK_VENDOR_FLEX_ITEM_TITLE_ID",
                        column: x => x.TITLE_ID,
                        principalTable: "FLEX_ITEM",
                        principalColumn: "FLEX_ITEM_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PURCHASE_ORDER",
                columns: table => new
                {
                    PURCHASE_ORDER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COMPANY_ID = table.Column<int>(type: "int", nullable: false),
                    BRANCH_ID = table.Column<int>(type: "int", nullable: false),
                    PURCHASE_ORDER_NO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PURCHASE_ORDER_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VENDOR_ID = table.Column<int>(type: "int", nullable: false),
                    VENDOR_CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VENDOR_FULLNAME = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    VAT_RATE = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    SUB_TOTAL = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    SUB_TOTAL_FC = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DISCOUNT = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DISCOUNT_FC = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PRICE_AFTER_DISCOUNT = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PRICE_AFTER_DISCOUNT_FC = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PRICE_BEFORE_VAT = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PRICE_BEFORE_VAT_FC = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PRICE_BEFORE_VAT_BASE = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PRICE_AFTER_VAT = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PRICE_AFTER_VAT_FC = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    GRAND_TOTAL = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    GRAND_TOTAL_FC = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    REMARK = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    DOCUMENT_STATUS_ID = table.Column<int>(type: "int", nullable: true),
                    DOCUMENT_STATUS_CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DOCUMENT_STATUS_NAME = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    DOCUMENT_STATUS_REASON = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    COMMENTS = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
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
                    table.PrimaryKey("PK_PURCHASE_ORDER", x => x.PURCHASE_ORDER_ID);
                    table.ForeignKey(
                        name: "FK_PURCHASE_ORDER_BRANCH_BRANCH_ID",
                        column: x => x.BRANCH_ID,
                        principalTable: "BRANCH",
                        principalColumn: "BRANCH_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PURCHASE_ORDER_COMPANY_COMPANY_ID",
                        column: x => x.COMPANY_ID,
                        principalTable: "COMPANY",
                        principalColumn: "COMPANY_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PURCHASE_ORDER_FLEX_ITEM_DOCUMENT_STATUS_ID",
                        column: x => x.DOCUMENT_STATUS_ID,
                        principalTable: "FLEX_ITEM",
                        principalColumn: "FLEX_ITEM_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PURCHASE_ORDER_VENDOR_VENDOR_ID",
                        column: x => x.VENDOR_ID,
                        principalTable: "VENDOR",
                        principalColumn: "VENDOR_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SYS_USER_BRANCH_ID",
                table: "SYS_USER",
                column: "BRANCH_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PURCHASE_ORDER_BRANCH_ID",
                table: "PURCHASE_ORDER",
                column: "BRANCH_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PURCHASE_ORDER_COMPANY_ID",
                table: "PURCHASE_ORDER",
                column: "COMPANY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PURCHASE_ORDER_DOCUMENT_STATUS_ID",
                table: "PURCHASE_ORDER",
                column: "DOCUMENT_STATUS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PURCHASE_ORDER_PURCHASE_ORDER_NO",
                table: "PURCHASE_ORDER",
                column: "PURCHASE_ORDER_NO");

            migrationBuilder.CreateIndex(
                name: "IX_PURCHASE_ORDER_VENDOR_ID",
                table: "PURCHASE_ORDER",
                column: "VENDOR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VENDOR_TITLE_ID",
                table: "VENDOR",
                column: "TITLE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VENDOR_VENDOR_CODE",
                table: "VENDOR",
                column: "VENDOR_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_SYS_USER_BRANCH_BRANCH_ID",
                table: "SYS_USER",
                column: "BRANCH_ID",
                principalTable: "BRANCH",
                principalColumn: "BRANCH_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SYS_USER_BRANCH_BRANCH_ID",
                table: "SYS_USER");

            migrationBuilder.DropTable(
                name: "PURCHASE_ORDER");

            migrationBuilder.DropTable(
                name: "VENDOR");

            migrationBuilder.DropIndex(
                name: "IX_SYS_USER_BRANCH_ID",
                table: "SYS_USER");
        }
    }
}
