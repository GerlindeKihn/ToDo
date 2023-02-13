using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Api.DataAccess.Entities;

namespace ToDo.Api.DataAccess.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Username).IsRequired();
            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.Password).IsRequired();

            builder.HasMany(x => x.ToDos).WithOne(x => x.User).HasForeignKey(x => x.UserId).IsRequired();
        }
    }
}
