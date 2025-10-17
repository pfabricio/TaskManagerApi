using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Persistence.Context.Configuration;

public class TarefaMap: IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Titulo).HasColumnType("varchar").HasMaxLength(120);
        builder.Property(e => e.Descricao).HasColumnType("varchar").HasMaxLength(180);
        builder.Property(e=>e.Status).HasConversion<int>();


        builder.HasMany(e => e.Comentarios)
            .WithOne(e => e.Tarefa)
            .HasForeignKey(e => e.TarefaId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.Historicos)
            .WithOne(e => e.Tarefa)
            .HasForeignKey(e => e.TarefaId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}