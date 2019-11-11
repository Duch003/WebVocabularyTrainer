using Microsoft.EntityFrameworkCore;
using RestApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestApiTests.Mocks
{
    public class MockVocabularyContext : DbContext
    {
        internal MockVocabularyContext(DbContextOptions<MockVocabularyContext> options) : base(options)
        {

        }

        internal MockVocabularyContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optioasdnsBuilder.UseInMemoryDatabase("MainDb"); // Wykorzystanie Bazy Danych w Pamięci RAM.
            optionsBuilder.UseInMemoryDatabase("TestDb");
        }

        public DbSet<Sentence> Sentences { get; set; }
    }

}
