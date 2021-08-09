using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PadillionRadio.Data.Entities;

namespace PadillionRadio.Data.Configutations
{
    public class EntitiesConfiguration : IEntityTypeConfiguration<UserIOS>
    {
        // добавленеи сущности книг в бд
        public void Configure(EntityTypeBuilder<UserIOS> builder)
        {
           builder.HasKey(e => e.Id);
           builder.ToTable("UserIOSs");
        }
    }
}