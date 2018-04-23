using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace WEB.Data
{
    public class TrackMap
    {
        public TrackMap(EntityTypeBuilder<Track> entityBuilder)
        {
            entityBuilder.HasKey(t => t.TrackId);
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.Property(t => t.Link).IsRequired();
            entityBuilder.Property(t => t.Cover).IsRequired();
            //entityBuilder.HasOne(i => i.Album).WithMany(i => i.Music).HasForeignKey(i => i.AlbumId);
        }
    }
}
