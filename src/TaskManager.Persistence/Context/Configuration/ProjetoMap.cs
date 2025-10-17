using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Persistence.Context.Configuration;

public class ProjetoMap: IEntityTypeConfiguration<Projeto>
{
    public void Configure(EntityTypeBuilder<Projeto> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Nome).HasColumnType("varchar").HasMaxLength(180);

        builder.HasMany(e => e.Tarefas)
            .WithOne(e => e.Projeto)
            .HasForeignKey(e => e.ProjetoId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}