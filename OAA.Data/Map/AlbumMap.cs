using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAA.Data
{
    public class AlbumMap
    {
        public AlbumMap(EntityTypeBuilder<Album> entityBuilder)
        {
            entityBuilder.HasKey(t => t.AlbumId);
            entityBuilder.Property(t => t.NameAlbum);
            entityBuilder.Property(t => t.NameArtist);
            entityBuilder.Property(t => t.Cover);
            entityBuilder.HasOne(i => i.Artist).WithMany(i => i.Albums).HasForeignKey(i => i.ArtistId);
        }
    }
}
