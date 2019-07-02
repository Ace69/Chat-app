using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Communication.BL;
using Communication.DAL;


namespace Communication.BL.Tests 
{
   public class InMemoryDbContextFactory : CommunicationDbContext
    {
        public CommunicationDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CommunicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("BLtesting");
            return new CommunicationDbContext(optionsBuilder.Options);
        }
    }
}
