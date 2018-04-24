﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAA.Data
{
    public class SimilarMap
    {
        public SimilarMap(EntityTypeBuilder<Similar> entityBuilder)
        {
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.Property(t => t.Photo).IsRequired();
            entityBuilder.Property(t => t.SimilarId).IsRequired();
            entityBuilder.HasOne(i => i.Artist).WithMany(i => i.Similars).HasForeignKey(i => i.ArtistId);
        }
    }
}