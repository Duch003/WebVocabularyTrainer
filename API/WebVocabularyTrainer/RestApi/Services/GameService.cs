using NLog;
using RestApi.Data.Models;
using RestApi.DatabaseAccess.Connectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Services
{
    public class GameService
    {
        protected ISentenceConnector _sentenceConnector;
        protected Logger _logger;

        public GameService(ISentenceConnector sentenceConnector)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _sentenceConnector = sentenceConnector;
        }

        public Settings GetSettingsForm()
        {
            var sources = _sentenceConnector.
            return new Settings();
        }
    }
}
