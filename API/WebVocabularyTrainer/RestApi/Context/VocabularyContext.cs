using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Context
{
    public class VocabularyContext : IdentityDbContext, IVocabularyContext
    {
        public VocabularyContext(DbContextOptions<VocabularyContext> options) : base(options)
        {

        }

        public VocabularyContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             //optionsBuilder.UseInMemoryDatabase("MainDb"); // Wykorzystanie Bazy Danych w Pamięci RAM.
        }

        public DbSet<Sentence> Sentences { get; set; }
    }
}
