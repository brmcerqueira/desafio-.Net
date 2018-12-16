using Desafio.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Persistence.Configurations
{
    internal class UserPhoneConfiguration : IEntityTypeConfiguration<UserPhone>
    {
        public void Configure(EntityTypeBuilder<UserPhone> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AreaCode);
            builder.Property(x => x.CountryCode).HasMaxLength(3);
            builder.Property(x => x.Number);
        }
    }
}