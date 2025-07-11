﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Catalog.Api.Models;

namespace Catalog.Api.Infrastructure.EntityConfigurations;

public sealed class CatalogBrandEntityTypeConfiguration : IEntityTypeConfiguration<CatalogBrand>
{
    public void Configure(EntityTypeBuilder<CatalogBrand> builder)
    {
        builder.ToTable(CatalogBrand.TableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Brand)
               .HasMaxLength(100)
               .IsRequired(true);
    }
}
