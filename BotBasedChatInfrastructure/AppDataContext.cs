using BotBaseChatDomain.MessagesAggregate;
using BotBasedChatInfrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotBasedChatInfrastructure
{
    public class AppDataContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MessageEntityTypeConfiguration());
        }
    }
}
