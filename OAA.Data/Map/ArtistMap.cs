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
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Name);
            entityBuilder.Property(t => t.Biography);
            entityBuilder.Property(t => t.Photo);
        }
    }
}
