using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ToDoTaskNotifyPropertyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Notify",
                table: "ToDoTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "ToDoTasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Notify",
                value: true);

            migrationBuilder.UpdateData(
                table: "ToDoTasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DueDate", "Notify" },
                values: new object[] { new DateTime(2023, 1, 6, 13, 30, 11, 811, DateTimeKind.Local).AddTicks(4265), true });

            migrationBuilder.UpdateData(
                table: "ToDoTasks",
                keyColumn: "Id",
                keyValue: 3,
                column: "Notify",
                value: false);

            migrationBuilder.UpdateData(
                table: "ToDoTasks",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DueDate", "Notify" },
                values: new object[] { new DateTime(2023, 1, 5, 13, 30, 11, 811, DateTimeKind.Local).AddTicks(4307), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notify",
                table: "ToDoTasks");

            migrationBuilder.UpdateData(
                table: "ToDoTasks",
                keyColumn: "Id",
                keyValue: 2,
                column: "DueDate",
                value: new DateTime(2023, 1, 5, 22, 32, 7, 386, DateTimeKind.Local).AddTicks(6668));

            migrationBuilder.UpdateData(
                table: "ToDoTasks",
                keyColumn: "Id",
                keyValue: 4,
                column: "DueDate",
                value: new DateTime(2023, 1, 6, 22, 32, 7, 386, DateTimeKind.Local).AddTicks(6702));
        }
    }
}
