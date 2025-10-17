using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Persistence.Context.Configuration;

public class HistoricoTarefaMap: IEntityTypeConfiguration<HistoricoTarefa>
{
    public void Configure(EntityTypeBuilder<HistoricoTarefa> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.CampoModificado).HasColumnType("varchar").HasMaxLength(200);
        builder.Property(e => e.ValorAnterior).HasColumnType("varchar").HasMaxLength(200);
        builder.Property(e => e.NovoValor).HasColumnType("varchar").HasMaxLength(200);
    }
}