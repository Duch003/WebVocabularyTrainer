using RestApi.DatabaseAccess.Connectors;
using RestApi.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestApiTests.Objects
{
    public class VocabularyServiceLayer : VocabularyService
    {
        public VocabularyServiceLayer(ISentenceConnector connector) : base(connector)
        {

        }

        public ISentenceConnector Connector 
        {
            get
            {
                return _connector;
            }
            set
            {
                _connector = value;
            }
        }
    }
}
