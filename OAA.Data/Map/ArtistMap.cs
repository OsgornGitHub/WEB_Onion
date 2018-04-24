using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAA.Data
{
    public class ArtistMap
    {

        public ArtistMap(EntityTypeBuilder<Artist> entityBuilder)
        {
            entityBuilder.HasKey(t => t.ArtistId);
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.Property(t => t.Biography).IsRequired();
            entityBuilder.Property(t => t.Photo).IsRequired();
        }
    }
}
