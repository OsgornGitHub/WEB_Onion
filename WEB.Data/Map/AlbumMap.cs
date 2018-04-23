using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace WEB.Data
{
    public class AlbumMap
    {
        public AlbumMap(EntityTypeBuilder<Album> entityBuilder)
        {
            entityBuilder.HasKey(t => t.AlbumId);
            entityBuilder.Property(t => t.NameAlbum).IsRequired();
            entityBuilder.Property(t => t.NameArtist).IsRequired();
            entityBuilder.Property(t => t.Cover).IsRequired();
            //entityBuilder.HasOne(i => i.Artist).WithMany(i => i.Alb).HasForeignKey(i => i.ArtistId);
        }
    }
}
