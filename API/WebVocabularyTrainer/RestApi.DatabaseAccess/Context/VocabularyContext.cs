 using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.DatabaseAccess.Context
{
    public class VocabularyContext : IdentityDbContext
    {
        public VocabularyContext(DbContextOptions<VocabularyContext> options) : base(options)
        {

        }

        public VocabularyContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("TestDb"); // Wykorzystanie Bazy Danych w Pamięci RAM.
            //optionsBuilder.UseSqlServer(@"Data Source=DUCH003\TOLEARNINSTANCE;Initial Catalog=TestDb;Integrated Security=True");
            //@"Data Source=OBONB1024\SQLEXPRESS02;Initial Catalog=TestDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //optionsBuilder.UseSqlServer(@"Data Source=OBONB1024\SQLEXPRESS02;Initial Catalog=TestDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public DbSet<Sentence> Sentences { get; set; }
    }
}
