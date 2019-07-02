using System;
using Microsoft.EntityFrameworkCore;

namespace Communication.DAL.Test.Configuration
{
    public class CommunicationDbContextTestsClassSetupFixture : IDisposable
    {
        public TestCommunicationDbContext CommunicationDbContextSut { get; set; }

        public CommunicationDbContextTestsClassSetupFixture()
        {
            this.CommunicationDbContextSut = CreateCommunicationDbContext();
        }

        public TestCommunicationDbContext CreateCommunicationDbContext()
        {
            return new TestCommunicationDbContext(CreateDbContextOptions());
        }

        private DbContextOptions<TestCommunicationDbContext> CreateDbContextOptions()
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<TestCommunicationDbContext>();
            contextOptionsBuilder.UseInMemoryDatabase("Communication");
            return contextOptionsBuilder.Options;
        }

        public void Dispose()
        {
            CommunicationDbContextSut?.Dispose();
        }
    }
}
