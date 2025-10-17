using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Persistence.Context.Configuration;

public class ComentarioMap: IEntityTypeConfiguration<Comentario>
{
    public void Configure(EntityTypeBuilder<Comentario> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Conteudo).HasColumnType("text");
    }
}