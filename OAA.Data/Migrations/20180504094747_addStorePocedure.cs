using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OAA.Data.Migrations
{
    public partial class addStorePocedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE [dbo].[GetAlbumByName]
	                                @name nvarchar(50)
                                AS
	                                SELECT * FROM Album 
	                                WHERE ArtistId = (SELECT Id FROM Artist WHERE Name = @name)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
