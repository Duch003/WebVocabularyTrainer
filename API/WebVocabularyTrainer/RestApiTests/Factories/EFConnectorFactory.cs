using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using RestApi.Data.Models;
using RestApi.DatabaseAccess.Connectors;
using RestApi.DatabaseAccess.Context;
using RestApiTests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiTests.Factories
{
    public static class EFConnectorFactory
    {
        private static object _lock = new object();
        private static MockEFConnector _instance;

        public static MockEFConnector Instance()
        {
            lock (_lock)
            {
                if(_instance is null)
                {
                    var context = new MockVocabularyContext();
                    var connector = new MockEFConnector(context);
                    connector.Context.AddRange(SentenceFactory.GetSentences());
                    connector.Context.SaveChanges();
                    _instance = connector;
                }
                return _instance;
            }
        }
    }
}
