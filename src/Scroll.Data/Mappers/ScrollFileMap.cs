using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scroll.Library.Models.Entities;
using Scroll.Service.Data;

namespace Scroll.Data.Mappers;

public class ScrollFileInfoMap : IEntityTypeConfiguration<ScrollFileInfo>
{
    public void Configure(EntityTypeBuilder<ScrollFileInfo> builder)
    {
        builder
            .ToTable("ScrollFiles");

        builder
            .HasDiscriminator()
            .HasValue<ScrollFileInfo>(nameof(ScrollFileInfo))
            .HasValue<ScrollFile>(nameof(ScrollFile));
        
        builder
            .HasKey(sf => sf.Id);

        builder
            .Property(sf => sf.Name)
            .HasMaxLength(200);
        
        builder
            .Property(sf => sf.AddedOn)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<CurrentDateTimeValueGenerator>();

        builder
            .Property(sf => sf.ContentType)
            .HasMaxLength(50);

        builder
            .HasIndex(sf => sf.Name)
            .IsUnique()
            .HasDatabaseName("UIX_ScrollFile_Name");
    }
}

public class ScrollFileMap : IEntityTypeConfiguration<ScrollFile>
{
    public void Configure(EntityTypeBuilder<ScrollFile> builder)
    {
        builder
            .Property(sf => sf.Content)
            .IsRequired();
    }
}