using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caltec.StudentInfoProject.Persistence.Migrations
{
    public partial class InitDatabase : Migration
    {
        private const string BigIntType = "bigint";
        private const string IdentityAnnotation = "SqlServer:Identity";
        private const string NVarCharMaxType = "nvarchar(max)";
        private const string StudentClassesTable = "StudentClasses";
        private const string StudentsTable = "Students";
        private const string SchoolFeesTable = "SchoolFees";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Degrees",
                columns: table => new
                {
                    Id = table.Column<long>(type: BigIntType, nullable: false)
                        .Annotation(IdentityAnnotation, "1, 1"),
                    Name = table.Column<string>(type: NVarCharMaxType, nullable: true),
                    NbYear = table.Column<int>(type: "int", nullable: false),
                    FeesPerYearPerStudent = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: StudentClassesTable,
                columns: table => new
                {
                    Id = table.Column<long>(type: BigIntType, nullable: false)
                        .Annotation(IdentityAnnotation, "1, 1"),
                    Name = table.Column<string>(type: NVarCharMaxType, nullable: true),
                    Description = table.Column<string>(type: NVarCharMaxType, nullable: true),
                    DegreeId = table.Column<long>(type: BigIntType, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentClasses_Degrees_DegreeId",
                        column: x => x.DegreeId,
                        principalTable: "Degrees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: StudentsTable,
                columns: table => new
                {
                    Id = table.Column<long>(type: BigIntType, nullable: false)
                        .Annotation(IdentityAnnotation, "1, 1"),
                    FirstName = table.Column<string>(type: NVarCharMaxType, nullable: true),
                    LastName = table.Column<string>(type: NVarCharMaxType, nullable: true),
                    Email = table.Column<string>(type: NVarCharMaxType, nullable: true),
                    Phone = table.Column<string>(type: NVarCharMaxType, nullable: true),
                    Address = table.Column<string>(type: NVarCharMaxType, nullable: true),
                    City = table.Column<string>(type: NVarCharMaxType, nullable: true),
                    State = table.Column<string>(type: NVarCharMaxType, nullable: true),
                    Zip = table.Column<string>(type: NVarCharMaxType, nullable: true),
                    Country = table.Column<string>(type: NVarCharMaxType, nullable: true),
                    ClassId = table.Column<long>(type: BigIntType, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_StudentClasses_ClassId",
                        column: x => x.ClassId,
                        principalTable: StudentClassesTable,
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: SchoolFeesTable,
                columns: table => new
                {
                    Id = table.Column<long>(type: BigIntType, nullable: false)
                        .Annotation(IdentityAnnotation, "1, 1"),
                    StudentId = table.Column<long>(type: BigIntType, nullable: true),
                    ClassId = table.Column<long>(type: BigIntType, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatePaid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: NVarCharMaxType, nullable: true),
                    PaymentReference = table.Column<string>(type: NVarCharMaxType, nullable: true),
                    PaymentStatus = table.Column<string>(type: NVarCharMaxType, nullable: true),
                    PaymentNote = table.Column<string>(type: NVarCharMaxType, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolFees_StudentClasses_ClassId",
                        column: x => x.ClassId,
                        principalTable: StudentClassesTable,
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SchoolFees_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: StudentsTable,
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolFees_ClassId",
                table: SchoolFeesTable,
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolFees_StudentId",
                table: SchoolFeesTable,
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClasses_DegreeId",
                table: StudentClassesTable,
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassId",
                table: StudentsTable,
                column: "ClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: SchoolFeesTable);

            migrationBuilder.DropTable(
                name: StudentsTable);

            migrationBuilder.DropTable(
                name: StudentClassesTable);

            migrationBuilder.DropTable(
                name: "Degrees");
        }
    }
}
