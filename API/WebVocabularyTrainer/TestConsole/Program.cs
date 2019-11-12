using RestApi.Data.Models;
using RestApi.DatabaseAccess.Connectors;
using RestApiTests.Factories;
using System;
using System.Linq;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var sentences = SentenceFactory.GetSentences().ToList();
            var connector = new EFConnector();
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
