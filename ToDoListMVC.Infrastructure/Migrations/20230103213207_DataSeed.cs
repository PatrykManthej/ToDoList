using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoListMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ToDoLists",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "TestList1" },
                    { 2, "TestList2" }
                });

            migrationBuilder.InsertData(
                table: "ToDoTasks",
                columns: new[] { "Id", "Description", "DueDate", "IsCompleted", "Name", "ToDoListId" },
                values: new object[,]
                {
                    { 1, "FirstTaskDescription", null, false, "FirstTaskWithoutDate", 1 },
                    { 2, "SecondTaskDescription", new DateTime(2023, 1, 5, 22, 32, 7, 386, DateTimeKind.Local).AddTicks(6668), false, "SecondTaskWithDate", 1 },
                    { 3, "TaskDescription", null, true, "ThirdTask", 1 },
                    { 4, "TaskDescription", new DateTime(2023, 1, 6, 22, 32, 7, 386, DateTimeKind.Local).AddTicks(6702), false, "FourthTask", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ToDoTasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ToDoTasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ToDoTasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ToDoTasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ToDoLists",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ToDoLists",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
