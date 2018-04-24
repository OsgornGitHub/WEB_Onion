﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using OAA.Data;
using System;

namespace OAA.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OAA.Data.Album", b =>
                {
                    b.Property<Guid>("AlbumId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ArtistId");

                    b.Property<string>("Cover")
                        .IsRequired();

                    b.Property<string>("NameAlbum")
                        .IsRequired();

                    b.Property<string>("NameArtist")
                        .IsRequired();

                    b.HasKey("AlbumId");

                    b.HasIndex("ArtistId");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("OAA.Data.Artist", b =>
                {
                    b.Property<Guid>("ArtistId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Biography")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Photo")
                        .IsRequired();

                    b.HasKey("ArtistId");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("OAA.Data.Similar", b =>
                {
                    b.Property<Guid>("SimilarId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ArtistId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Photo")
                        .IsRequired();

                    b.HasKey("SimilarId");

                    b.HasIndex("ArtistId");

                    b.ToTable("Similar");
                });

            modelBuilder.Entity("OAA.Data.Track", b =>
                {
                    b.Property<Guid>("TrackId");

                    b.Property<Guid>("AlbumId");

                    b.Property<string>("Cover")
                        .IsRequired();

                    b.Property<string>("Link")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("TrackId");

                    b.ToTable("Track");
                });

            modelBuilder.Entity("OAA.Data.Album", b =>
                {
                    b.HasOne("OAA.Data.Artist", "Artist")
                        .WithMany("Albums")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OAA.Data.Similar", b =>
                {
                    b.HasOne("OAA.Data.Artist", "Artist")
                        .WithMany("Similars")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OAA.Data.Track", b =>
                {
                    b.HasOne("OAA.Data.Album", "Album")
                        .WithMany("Tracks")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}