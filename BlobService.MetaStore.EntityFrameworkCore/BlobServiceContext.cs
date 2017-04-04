﻿using BlobService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlobService.MetaStore.EntityFrameworkCore
{
    public class BlobServiceContext : DbContext
    {
        public DbSet<ContainerMeta> ContainersMetaData { get; set; }
        public DbSet<BlobMeta> BlobsMetaData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContainerMeta>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasIndex(x => x.Name).HasName("ContainerNameIndex").IsUnique();
                c.Property(x => x.Name).HasMaxLength(256);
                c.ToTable("BlobServiceContainers");
                c.HasMany(x => x.Blobs).WithOne().HasForeignKey(y => y.ContainerId).IsRequired();
            });

            modelBuilder.Entity<BlobMeta>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.MimeType).HasMaxLength(256);
                b.Property(x => x.OrigFileName).HasMaxLength(256);
                b.Property(x => x.StorageSubject).HasMaxLength(256);
                b.ToTable("BlobServiceBlobs");
            });
        }
    }
}
