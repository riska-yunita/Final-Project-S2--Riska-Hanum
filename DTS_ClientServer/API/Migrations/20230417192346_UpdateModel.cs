using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_M_Employees",
                columns: table => new
                {
                    nik = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    first_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    hiring_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    phone_number = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Employees", x => x.nik);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Universities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Universities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Accounts",
                columns: table => new
                {
                    employee_nik = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Accounts", x => x.employee_nik);
                    table.ForeignKey(
                        name: "FK_TB_M_Accounts_TB_M_Employees_employee_nik",
                        column: x => x.employee_nik,
                        principalTable: "TB_M_Employees",
                        principalColumn: "nik",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Educations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    major = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    degree = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    gpa = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    university_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Educations", x => x.id);
                    table.ForeignKey(
                        name: "FK_TB_M_Educations_TB_M_Universities_university_id",
                        column: x => x.university_id,
                        principalTable: "TB_M_Universities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_TR_Account_Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account_nik = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TR_Account_Roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_TB_TR_Account_Roles_TB_M_Accounts_account_nik",
                        column: x => x.account_nik,
                        principalTable: "TB_M_Accounts",
                        principalColumn: "employee_nik",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_TR_Account_Roles_TB_M_Roles_role_id",
                        column: x => x.role_id,
                        principalTable: "TB_M_Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_TR_Profilings",
                columns: table => new
                {
                    employee_nik = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    education_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TR_Profilings", x => x.employee_nik);
                    table.ForeignKey(
                        name: "FK_TB_TR_Profilings_TB_M_Educations_education_id",
                        column: x => x.education_id,
                        principalTable: "TB_M_Educations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_TR_Profilings_TB_M_Employees_employee_nik",
                        column: x => x.employee_nik,
                        principalTable: "TB_M_Employees",
                        principalColumn: "nik",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Educations_university_id",
                table: "TB_M_Educations",
                column: "university_id");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Employees_email",
                table: "TB_M_Employees",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Employees_phone_number",
                table: "TB_M_Employees",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_TR_Account_Roles_account_nik",
                table: "TB_TR_Account_Roles",
                column: "account_nik");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TR_Account_Roles_role_id",
                table: "TB_TR_Account_Roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TR_Profilings_education_id",
                table: "TB_TR_Profilings",
                column: "education_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_TR_Profilings_education_id1",
                table: "TB_TR_Profilings",
                column: "education_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_TR_Account_Roles");

            migrationBuilder.DropTable(
                name: "TB_TR_Profilings");

            migrationBuilder.DropTable(
                name: "TB_M_Accounts");

            migrationBuilder.DropTable(
                name: "TB_M_Roles");

            migrationBuilder.DropTable(
                name: "TB_M_Educations");

            migrationBuilder.DropTable(
                name: "TB_M_Employees");

            migrationBuilder.DropTable(
                name: "TB_M_Universities");
        }
    }
}
