using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sttb.Entities.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "beritas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    judul = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    slug = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    deskripsi = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    thumbnail_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_published = table.Column<bool>(type: "boolean", nullable: false),
                    kategori = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    konten = table.Column<string>(type: "text", nullable: false),
                    penulis = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    tanggal_publish = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_beritas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "kegiatans",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    judul = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    slug = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    deskripsi = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    thumbnail_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_published = table.Column<bool>(type: "boolean", nullable: false),
                    lokasi = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    tanggal_mulai = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tanggal_selesai = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    penyelenggara = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_kegiatans", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "koleksi_perpustakaans",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    judul = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    slug = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    deskripsi = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    thumbnail_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_published = table.Column<bool>(type: "boolean", nullable: false),
                    penulis = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    penerbit = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    tahun = table.Column<int>(type: "integer", nullable: false),
                    kategori = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    file_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_koleksi_perpustakaans", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "media_artikels",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    judul = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    slug = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    deskripsi = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    thumbnail_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_published = table.Column<bool>(type: "boolean", nullable: false),
                    konten = table.Column<string>(type: "text", nullable: false),
                    kategori = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    penulis = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_media_artikels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "media_videos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    judul = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    slug = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    deskripsi = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    thumbnail_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_published = table.Column<bool>(type: "boolean", nullable: false),
                    video_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    durasi = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    kategori = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_media_videos", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "beritas");

            migrationBuilder.DropTable(
                name: "kegiatans");

            migrationBuilder.DropTable(
                name: "koleksi_perpustakaans");

            migrationBuilder.DropTable(
                name: "media_artikels");

            migrationBuilder.DropTable(
                name: "media_videos");
        }
    }
}
