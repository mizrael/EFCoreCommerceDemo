using System;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCommerceDemo.Example1.Infrastructure
{
    public static class DbContextUtils
    {
        public static TDb Create<TDb>(string connStr, Func<DbContextOptions<TDb>, TDb> creator) where TDb : DbContext
        {
            var options = new DbContextOptionsBuilder<TDb>()
                .UseSqlServer(connStr)
                .Options;

            var dbContext = creator(options);
            return dbContext;
        }
    }
}