using BotBasedChatInfrastructure;
using BotBasedChatInfrastructure.Repositories;
using BotBasedChatWebV5.Services.Csv;
using BotBasedChatWebV5.Services.MessageBroker;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BotBasedChatTest.Broker
{
    public class MessaggeBrokerShould
    {
        [Theory]
        [InlineData("/stock=aapl​")]
        [InlineData("/stock=a​")]
        [InlineData("/stock=b")]
        [InlineData("/stock=c​")]
        [InlineData("/stock=d​")]
        public void IsValidCsvCallApiFormat(string message)
        {
            var dbOpt = new DbContextOptionsBuilder<AppDataContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;
            using (var ctx = new AppDataContext(dbOpt))
            {
                var sut = new MessageBroker(new CsvBot(), new MessageRepository(ctx));
                var okObjectResult = sut.CheckMessageFormat(message);
                Assert.True(okObjectResult.Status);
            }                               
        }

        [Theory]
        [InlineData("/stock=12345​")]
        [InlineData("/stock=")]
        [InlineData("/stock=   ")]        
        [InlineData("/stock=d%(/#$#")]
        public void IsNotValidCsvCallApiFormat(string message)
        {
            var dbOpt = new DbContextOptionsBuilder<AppDataContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;
            using (var ctx = new AppDataContext(dbOpt))
            {
                var sut = new MessageBroker(new CsvBot(), new MessageRepository(ctx));
                var okObjectResult = sut.CheckMessageFormat(message);
                Assert.False(okObjectResult.Status);
            }
        }
    }
}
