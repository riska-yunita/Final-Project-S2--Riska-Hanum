using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AlterOnDeleteBehvior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Accounts_TB_M_Employees_employee_nik",
                table: "TB_M_Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Educations_TB_M_Universities_university_id",
                table: "TB_M_Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_TR_Account_Roles_TB_M_Accounts_account_nik",
                table: "TB_TR_Account_Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_TR_Account_Roles_TB_M_Roles_role_id",
                table: "TB_TR_Account_Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_TR_Profilings_TB_M_Educations_education_id",
                table: "TB_TR_Profilings");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_TR_Profilings_TB_M_Employees_employee_nik",
                table: "TB_TR_Profilings");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Accounts_TB_M_Employees_employee_nik",
                table: "TB_M_Accounts",
                column: "employee_nik",
                principalTable: "TB_M_Employees",
                principalColumn: "nik");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Educations_TB_M_Universities_university_id",
                table: "TB_M_Educations",
                column: "university_id",
                principalTable: "TB_M_Universities",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TR_Account_Roles_TB_M_Accounts_account_nik",
                table: "TB_TR_Account_Roles",
                column: "account_nik",
                principalTable: "TB_M_Accounts",
                principalColumn: "employee_nik");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TR_Account_Roles_TB_M_Roles_role_id",
                table: "TB_TR_Account_Roles",
                column: "role_id",
                principalTable: "TB_M_Roles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TR_Profilings_TB_M_Educations_education_id",
                table: "TB_TR_Profilings",
                column: "education_id",
                principalTable: "TB_M_Educations",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TR_Profilings_TB_M_Employees_employee_nik",
                table: "TB_TR_Profilings",
                column: "employee_nik",
                principalTable: "TB_M_Employees",
                principalColumn: "nik");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Accounts_TB_M_Employees_employee_nik",
                table: "TB_M_Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Educations_TB_M_Universities_university_id",
                table: "TB_M_Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_TR_Account_Roles_TB_M_Accounts_account_nik",
                table: "TB_TR_Account_Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_TR_Account_Roles_TB_M_Roles_role_id",
                table: "TB_TR_Account_Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_TR_Profilings_TB_M_Educations_education_id",
                table: "TB_TR_Profilings");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_TR_Profilings_TB_M_Employees_employee_nik",
                table: "TB_TR_Profilings");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Accounts_TB_M_Employees_employee_nik",
                table: "TB_M_Accounts",
                column: "employee_nik",
                principalTable: "TB_M_Employees",
                principalColumn: "nik",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Educations_TB_M_Universities_university_id",
                table: "TB_M_Educations",
                column: "university_id",
                principalTable: "TB_M_Universities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TR_Account_Roles_TB_M_Accounts_account_nik",
                table: "TB_TR_Account_Roles",
                column: "account_nik",
                principalTable: "TB_M_Accounts",
                principalColumn: "employee_nik",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TR_Account_Roles_TB_M_Roles_role_id",
                table: "TB_TR_Account_Roles",
                column: "role_id",
                principalTable: "TB_M_Roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TR_Profilings_TB_M_Educations_education_id",
                table: "TB_TR_Profilings",
                column: "education_id",
                principalTable: "TB_M_Educations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TR_Profilings_TB_M_Employees_employee_nik",
                table: "TB_TR_Profilings",
                column: "employee_nik",
                principalTable: "TB_M_Employees",
                principalColumn: "nik",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
