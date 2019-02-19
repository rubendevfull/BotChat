using BotBaseChatDomain.MessagesAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotBasedChatInfrastructure.EntityConfigurations
{
    public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Message");

            builder.Property(p => p.Id)
                .ForSqlServerUseSequenceHiLo("message_seq")
                .IsRequired();

            builder.Property(p => p.Data)
                .IsRequired(true);

            builder.Property(p => p.Room)
                .IsRequired(true);

            builder.Property(p => p.CreatedDate)
                .IsRequired(true);
        }
    }
}
