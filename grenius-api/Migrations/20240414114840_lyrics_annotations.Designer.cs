﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using grenius_api.Infrastructure.Database;

#nullable disable

namespace grenius_api.Migrations
{
    [DbContext(typeof(GreniusContext))]
    [Migration("20240414114840_lyrics_annotations")]
    partial class lyrics_annotations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("grenius_api.Domain.Entities.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlbumTypeId")
                        .HasColumnType("int")
                        .HasColumnName("album_type_id");

                    b.Property<int>("ArtistId")
                        .HasColumnType("int")
                        .HasColumnName("artist_id");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("releaseDate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("albums", (string)null);
                });

            modelBuilder.Entity("grenius_api.Domain.Entities.Annotation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EndSymbol")
                        .HasColumnType("int")
                        .HasColumnName("end_symbol");

                    b.Property<int>("LyricsId")
                        .HasColumnType("int")
                        .HasColumnName("lyrics_id");

                    b.Property<int>("StartSymbol")
                        .HasColumnType("int")
                        .HasColumnName("start_symbol");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("text");

                    b.HasKey("Id");

                    b.HasIndex("LyricsId");

                    b.ToTable("annotation", (string)null);
                });

            modelBuilder.Entity("grenius_api.Domain.Entities.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2")
                        .HasColumnName("birthday");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("country");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nickname");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("surname");

                    b.HasKey("Id");

                    b.ToTable("artists", (string)null);
                });

            modelBuilder.Entity("grenius_api.Domain.Entities.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtistId")
                        .HasColumnType("int")
                        .HasColumnName("artist_id");

                    b.Property<int>("Priority")
                        .HasColumnType("int")
                        .HasColumnName("priority");

                    b.Property<int>("SongId")
                        .HasColumnType("int")
                        .HasColumnName("song_id");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("SongId");

                    b.ToTable("features", (string)null);
                });

            modelBuilder.Entity("grenius_api.Domain.Entities.Lyrics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("SongId")
                        .HasColumnType("int")
                        .HasColumnName("song_id");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT")
                        .HasColumnName("text");

                    b.HasKey("Id");

                    b.HasIndex("SongId");

                    b.ToTable("lyrics", (string)null);
                });

            modelBuilder.Entity("grenius_api.Domain.Entities.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AlbumId")
                        .HasColumnType("int")
                        .HasColumnName("album_id");

                    b.Property<int>("ArtistId")
                        .HasColumnType("int")
                        .HasColumnName("artist_id");

                    b.Property<bool>("IsFeature")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("isFeature");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("releaseDate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ArtistId");

                    b.ToTable("songs", (string)null);
                });

            modelBuilder.Entity("grenius_api.Domain.Entities.Album", b =>
                {
                    b.HasOne("grenius_api.Domain.Entities.Artist", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("grenius_api.Domain.Entities.Annotation", b =>
                {
                    b.HasOne("grenius_api.Domain.Entities.Lyrics", "Lyrics")
                        .WithMany()
                        .HasForeignKey("LyricsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lyrics");
                });

            modelBuilder.Entity("grenius_api.Domain.Entities.Feature", b =>
                {
                    b.HasOne("grenius_api.Domain.Entities.Artist", "Artist")
                        .WithMany("Features")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("grenius_api.Domain.Entities.Song", "Song")
                        .WithMany("Features")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Artist");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("grenius_api.Domain.Entities.Lyrics", b =>
                {
                    b.HasOne("grenius_api.Domain.Entities.Song", "Song")
                        .WithMany()
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Song");
                });

            modelBuilder.Entity("grenius_api.Domain.Entities.Song", b =>
                {
                    b.HasOne("grenius_api.Domain.Entities.Album", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumId");

                    b.HasOne("grenius_api.Domain.Entities.Artist", "Artist")
                        .WithMany("Songs")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("grenius_api.Domain.Entities.Album", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("grenius_api.Domain.Entities.Artist", b =>
                {
                    b.Navigation("Features");

                    b.Navigation("Songs");
                });

            modelBuilder.Entity("grenius_api.Domain.Entities.Song", b =>
                {
                    b.Navigation("Features");
                });
#pragma warning restore 612, 618
        }
    }
}
