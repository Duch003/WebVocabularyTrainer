using RestApi.DatabaseAccess.Connectors;
using RestApi.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestApiTests.Objects
{
    public class VocabularyServiceLayer : VocabularyService
    {
        public VocabularyServiceLayer(IConnector connector) : base(connector)
        {

        }

        public IConnector Connector 
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
