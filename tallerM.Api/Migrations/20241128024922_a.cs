using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tallerM.Api.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Automoviles_AutomovilId",
                table: "Servicios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Servicios",
                table: "Servicios");

            migrationBuilder.RenameTable(
                name: "Servicios",
                newName: "DetalleServicios");

            migrationBuilder.RenameIndex(
                name: "IX_Servicios_AutomovilId",
                table: "DetalleServicios",
                newName: "IX_DetalleServicios_AutomovilId");

            migrationBuilder.AlterColumn<int>(
                name: "AutomovilId",
                table: "DetalleServicios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "DetalleServicios",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "DetalleServicios",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdCambioPieza",
                table: "DetalleServicios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MecanicoId",
                table: "DetalleServicios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "usoRefaccion",
                table: "DetalleServicios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetalleServicios",
                table: "DetalleServicios",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Mecanicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mecanicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Refacciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refacciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CambioPiezas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pieza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdRefaccion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CambioPiezas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CambioPiezas_Refacciones_IdRefaccion",
                        column: x => x.IdRefaccion,
                        principalTable: "Refacciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleServicios_IdCambioPieza",
                table: "DetalleServicios",
                column: "IdCambioPieza");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleServicios_MecanicoId",
                table: "DetalleServicios",
                column: "MecanicoId");

            migrationBuilder.CreateIndex(
                name: "IX_CambioPiezas_IdRefaccion",
                table: "CambioPiezas",
                column: "IdRefaccion");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleServicios_Automoviles_AutomovilId",
                table: "DetalleServicios",
                column: "AutomovilId",
                principalTable: "Automoviles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleServicios_CambioPiezas_IdCambioPieza",
                table: "DetalleServicios",
                column: "IdCambioPieza",
                principalTable: "CambioPiezas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleServicios_Mecanicos_MecanicoId",
                table: "DetalleServicios",
                column: "MecanicoId",
                principalTable: "Mecanicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleServicios_Automoviles_AutomovilId",
                table: "DetalleServicios");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleServicios_CambioPiezas_IdCambioPieza",
                table: "DetalleServicios");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleServicios_Mecanicos_MecanicoId",
                table: "DetalleServicios");

            migrationBuilder.DropTable(
                name: "CambioPiezas");

            migrationBuilder.DropTable(
                name: "Mecanicos");

            migrationBuilder.DropTable(
                name: "Refacciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetalleServicios",
                table: "DetalleServicios");

            migrationBuilder.DropIndex(
                name: "IX_DetalleServicios_IdCambioPieza",
                table: "DetalleServicios");

            migrationBuilder.DropIndex(
                name: "IX_DetalleServicios_MecanicoId",
                table: "DetalleServicios");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "DetalleServicios");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "DetalleServicios");

            migrationBuilder.DropColumn(
                name: "IdCambioPieza",
                table: "DetalleServicios");

            migrationBuilder.DropColumn(
                name: "MecanicoId",
                table: "DetalleServicios");

            migrationBuilder.DropColumn(
                name: "usoRefaccion",
                table: "DetalleServicios");

            migrationBuilder.RenameTable(
                name: "DetalleServicios",
                newName: "Servicios");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleServicios_AutomovilId",
                table: "Servicios",
                newName: "IX_Servicios_AutomovilId");

            migrationBuilder.AlterColumn<int>(
                name: "AutomovilId",
                table: "Servicios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Servicios",
                table: "Servicios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Automoviles_AutomovilId",
                table: "Servicios",
                column: "AutomovilId",
                principalTable: "Automoviles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
