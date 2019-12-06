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
                return new Result<Settings>(null, e);
            }
        }

        public async Task<Result<Game>> GetGame(Settings settings)
        {
            if(settings is null)
            {
                settings = new Settings();
            }

            if(settings.Repeats < 1)
            {
                settings.Repeats = 1;
            }

            if(settings.Sources is null || !settings.Sources.Any())
            {
                settings.Sources = new List<string>();
                settings.Sources.Add("All");
            }

            if (settings.Subjects is null || !settings.Subjects.Any())
            {
                settings.Subjects = new List<string>();
                settings.Subjects.Add("All");
            }

            try
            {
                var result = await _sentenceConnector.GetSentencesAsync(settings);
                var output = new Result<Game>(new Game { Sentences = result, Mode = settings.Mode });
                return output;
            }
            catch(Exception e)
            {
                return new Result<Game>(null, e);
            }
        }
    }
}
