﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sozluk.Api.Domain.Models;

namespace Sozluk.Infrastructure.Persistence.Context.EntityConfigurations.EntryComment
{
    public class EntryCommentVoteEntityConfiguration : BaseEntityConfiguartions<EntryCommentVote>
    {
        public override void Configure(EntityTypeBuilder<EntryCommentVote> builder)
        {
            base.Configure(builder);

            builder.ToTable("entrycommentvote", SozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(i => i.EntryComment)
                .WithMany(i => i.EntryCommentVotes)
                .HasForeignKey(i => i.EntryCommentId);

        }
    }
}
