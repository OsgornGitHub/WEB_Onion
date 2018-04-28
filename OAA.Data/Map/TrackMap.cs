using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAA.Data
{
    public class TrackMap
    {
        public TrackMap(EntityTypeBuilder<Track> entityBuilder)
        {
            entityBuilder.HasKey(t => t.TrackId);
            entityBuilder.Property(t => t.Name);
            entityBuilder.Property(t => t.NameAlbum);
            entityBuilder.Property(t => t.Link);
            entityBuilder.Property(t => t.Cover);
            entityBuilder.HasOne(i => i.Album).WithMany(i => i.Tracks).HasForeignKey(i => i.AlbumId);
        }
    }
}
