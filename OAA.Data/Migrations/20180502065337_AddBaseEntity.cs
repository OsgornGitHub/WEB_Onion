using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OAA.Data.Migrations
{
    public partial class AddBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_Artist_ArtistId",
                table: "Album");

            migrationBuilder.DropForeignKey(
                name: "FK_Similar_Artist_ArtistId",
                table: "Similar");

            migrationBuilder.DropForeignKey(
                name: "FK_Track_Album_AlbumId",
                table: "Track");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Track",
                table: "Track");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Similar",
                table: "Similar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Artist",
                table: "Artist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Album",
                table: "Album");

            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "Track");

            migrationBuilder.DropColumn(
                name: "SimilarId",
                table: "Similar");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Album");

            migrationBuilder.DropColumn(
                name: "NameAlbum",
                table: "Album");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Track",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Similar",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Artist",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Album",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Album",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Track",
                table: "Track",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Similar",
                table: "Similar",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artist",
                table: "Artist",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Album",
                table: "Album",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Artist_ArtistId",
                table: "Album",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Similar_Artist_ArtistId",
                table: "Similar",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Track_Album_AlbumId",
                table: "Track",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_Artist_ArtistId",
                table: "Album");

            migrationBuilder.DropForeignKey(
                name: "FK_Similar_Artist_ArtistId",
                table: "Similar");

            migrationBuilder.DropForeignKey(
                name: "FK_Track_Album_AlbumId",
                table: "Track");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Track",
                table: "Track");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Similar",
                table: "Similar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Artist",
                table: "Artist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Album",
                table: "Album");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Track");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Similar");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Album");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Album");

            migrationBuilder.AddColumn<Guid>(
                name: "TrackId",
                table: "Track",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SimilarId",
                table: "Similar",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ArtistId",
                table: "Artist",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AlbumId",
                table: "Album",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "NameAlbum",
                table: "Album",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Track",
                table: "Track",
                column: "TrackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Similar",
                table: "Similar",
                column: "SimilarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artist",
                table: "Artist",
                column: "ArtistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Album",
                table: "Album",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Artist_ArtistId",
                table: "Album",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Similar_Artist_ArtistId",
                table: "Similar",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Track_Album_AlbumId",
                table: "Track",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "AlbumId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
