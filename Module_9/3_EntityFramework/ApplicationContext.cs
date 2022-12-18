using System;
using _3_EntityFramework.Entities;
using _3_EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace _3_EntityFramework
{
	public class ApplicationContext : DbContext
	{
        const string ConnectionString = "Server=tcp:learn-m9-db-server.database.windows.net,1433;Initial Catalog=learn-m9-db;Persist Security Info=False;User ID=sk;Password=December2022#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public ApplicationContext()
		{
		}

        public DbSet<Region> Region { get; set; }

        public DbSet<Territory> Territories { get; set; }

        public DbSet<CustOrderHistProcedureResponse> CustOrderHistProcedureResponses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(ConnectionString);
        }
    }
}

