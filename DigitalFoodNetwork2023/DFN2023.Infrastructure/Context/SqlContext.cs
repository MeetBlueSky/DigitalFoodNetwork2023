using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace DFN2023.Infrastructure.Context
{
    public class SqlContext : BaseContext<SqlContext>
    {

        private readonly string connectionString = "";

        public SqlContext(string connectionString) : base(new DbContextOptionsBuilder<SqlContext>().UseSqlServer(connectionString).Options)
        {
            this.connectionString = connectionString;
        }

        public SqlContext() : base(new DbContextOptionsBuilder<SqlContext>().Options)
        {

        }

        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (connectionString != "")
                return;
            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json")
                .Build();
            string importDBConnection = configuration.GetConnectionString("DevConnection");
            optionsBuilder.UseSqlServer(importDBConnection);
        }

    }
}
