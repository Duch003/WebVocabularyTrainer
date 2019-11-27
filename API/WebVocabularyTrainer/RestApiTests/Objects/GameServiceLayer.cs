using RestApi.DatabaseAccess.Connectors;
using RestApi.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestApiTests.Objects
{
    public class GameServiceLayer : GameService
    {
        public GameServiceLayer(ISentenceConnector connector) : base(connector)
        {

        }

        public ISentenceConnector SentenceConnector 
        { 
            get
            {
                return _sentenceConnector;
            }
            set
            {
                _sentenceConnector = value;
            }
        }
    }
}
