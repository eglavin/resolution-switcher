using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResolutionSwitcher.Migrations
{
	/// <inheritdoc />
	public partial class InitialCreate : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Modes",
				columns: table => new
				{
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Name = table.Column<string>(type: "TEXT", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Modes", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Displays",
				columns: table => new
				{
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					ModeId = table.Column<int>(type: "INTEGER", nullable: false),
					DisplayName = table.Column<string>(type: "TEXT", nullable: false),
					Width = table.Column<int>(type: "INTEGER", nullable: false),
					Height = table.Column<int>(type: "INTEGER", nullable: false),
					Frequency = table.Column<int>(type: "INTEGER", nullable: false),
					PositionX = table.Column<int>(type: "INTEGER", nullable: false),
					PositionY = table.Column<int>(type: "INTEGER", nullable: false),
					IsPrimary = table.Column<bool>(type: "INTEGER", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Displays", x => x.Id);
					table.ForeignKey(
						name: "FK_Displays_Modes_ModeId",
						column: x => x.ModeId,
						principalTable: "Modes",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Displays_ModeId",
				table: "Displays",
				column: "ModeId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Displays");

			migrationBuilder.DropTable(
				name: "Modes");
		}
	}
}
