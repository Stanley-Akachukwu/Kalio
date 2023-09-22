using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Text.Json;

namespace Kalio.Entities
{
    public abstract class BaseEntityConfiguration<TEntity, IdType> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity<IdType>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> entity)
        {
            entity.UseTpcMappingStrategy();

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnType("nvarchar").HasMaxLength(40); 

            entity.HasQueryFilter(t => t.IsDeleted == false);
            entity.Property(e => e.ViewModelPayload).IsRequired(false);
            entity.Property(e => e.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
        }


    }
}
