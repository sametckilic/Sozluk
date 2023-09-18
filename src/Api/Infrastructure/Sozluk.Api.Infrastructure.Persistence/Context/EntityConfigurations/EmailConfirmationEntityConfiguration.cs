using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sozluk.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Infrastructure.Persistence.Context.EntityConfigurations
{
    public class EmailConfirmationEntityConfiguration : BaseEntityConfiguartions<EmailConfirmation>
    {
        public override void Configure(EntityTypeBuilder<EmailConfirmation> builder)
        {
            base.Configure(builder);

            builder.ToTable("emailconfirmation", SozlukDboContext.DEFAULT_SCHEMA);
        }
    }
}
