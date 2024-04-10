﻿using grenius_api.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace grenius_api.Domain.Configurations
{
    public class SongConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.ToTable("songs");

            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.ArtistId).IsRequired();
            builder.Property(p => p.ReleaseDate).IsRequired();
            builder.Property(p => p.IsFeature).HasDefaultValue(false);
            builder.Property(p => p.AlbumId).IsRequired(false);

            builder.Property(p => p.Title).HasColumnType("varchar(50)");

            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Title).HasColumnName("title");
            builder.Property(p => p.ReleaseDate).HasColumnName("releaseDate");
            builder.Property(p => p.IsFeature).HasColumnName("nickname");
            builder.Property(p => p.ArtistId).HasColumnName("artist_id");
            builder.Property(p => p.AlbumId).HasColumnName("album_id");
        }
    }
}
