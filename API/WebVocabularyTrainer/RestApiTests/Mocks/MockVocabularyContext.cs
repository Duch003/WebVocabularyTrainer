using Microsoft.EntityFrameworkCore;
using RestApi.Data.Models;
using RestApi.DatabaseAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestApiTests.Mocks
{
    public class MockVocabularyContext : VocabularyContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optioasdnsBuilder.UseInMemoryDatabase("MainDb"); // Wykorzystanie Bazy Danych w Pamięci RAM.
            optionsBuilder.UseInMemoryDatabase("TestDb");
        }
    }

}
