using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Api.DataAccess.Entities;

namespace ToDo.Api.DataAccess.Configurations;

public class ToDoConfiguration : IEntityTypeConfiguration<ToDoEntity>
{
    public void Configure(EntityTypeBuilder<ToDoEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Topic).IsRequired();
        builder.Property(x => x.Deadline).IsRequired();
    }
}
