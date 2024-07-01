using Crud_Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crud_Opreation.Mapping
{
    // ProductMapping configures the entity mapping for the 'Product' table.
    // It defines the table name, primary key, and property constraints.
    // The properties (e.g., Name, ProduceDate, ManufactureEmail, ManufacturePhone)
    // are mapped to corresponding columns in the database.
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ProduceDate).IsRequired();
            builder.Property(x => x.ManufactureEmail).IsRequired();
            builder.Property(x => x.ManufacturePhone).HasMaxLength(30).IsRequired();
        }
    }
}
