using Microsoft.AspNetCore.Identity;
using RestApi.Data.Models;
using RestApi.DatabaseAccess.Connectors;
using RestApi.DatabaseAccess.Context;
using RestApiTests.Factories;
using System;
using System.Linq;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new VocabularyContext();
            //context.Users.Add(null); //ArgumentNullException()
            
            

            context.Users.Add(null);
            //var sentences = SentenceFactory.GetSentences().ToList();
            var connector = new EFSentenceConnector();
            //foreach (var item in sentences)
            //{
            //    connector.Add(item);
            //}

            var valid = new Sentence
            {
                ID = 1,
                Primary = "a",
                Foreign = "b",
                Subject = "c"
            };

            //var entry = connector.GetSentence(55);
            

            //connector.Delete(valid);

            Console.ReadLine();
        }
    }
}
