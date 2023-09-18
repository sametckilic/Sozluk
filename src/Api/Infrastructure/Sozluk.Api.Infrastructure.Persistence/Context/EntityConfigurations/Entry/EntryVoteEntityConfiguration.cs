using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sozluk.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Infrastructure.Persistence.Context.EntityConfigurations.Entry
{
    public class EntryVoteEntityConfiguration : BaseEntityConfiguartions<EntryVote>
    {
        public override void Configure(EntityTypeBuilder<EntryVote> builder)
        {
            base.Configure(builder);

            builder.ToTable("entryvote", SozlukDboContext.DEFAULT_SCHEMA);

            builder.HasOne(i => i.Entry)
                .WithMany(i => i.EntryVotes)
                .HasForeignKey(i => i.EntryId);
        }
    }
}
