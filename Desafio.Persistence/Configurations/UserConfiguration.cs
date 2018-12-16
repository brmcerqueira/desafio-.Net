using Desafio.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(30);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.LastLogin);
        }
    }
}
