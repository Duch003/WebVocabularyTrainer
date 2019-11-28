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

        public async Task<Result<Settings>> GetSettingsForm()
        {
            try
            {
                var sources = (await _sentenceConnector.GetAllSources()).ToList();
                sources.Add("All");
                var subjects = (await _sentenceConnector.GetAllSubjects()).ToList();
                subjects.Add("All");
                var output = new Result<Settings>(new Settings(subjects, sources));
                return output;
            }
            catch(Exception e)
            {
                var output = new Result<Settings>(null, e);
                return output;
            }
        }
    }
}
