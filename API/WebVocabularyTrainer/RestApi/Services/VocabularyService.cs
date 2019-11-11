using NLog;
using RestApi.Data.Models;
using RestApi.DatabaseAccess.Connectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Services
{
    public class VocabularyService
    {
        protected IConnector _connector;
        protected Logger _logger;

        public VocabularyService(IConnector connector)
        {
            _connector = connector;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<Result<IEnumerable<Sentence>>> Get()
        {
            try
            {
                _logger.Info("Selecting all entries.");
                var found = await _connector.GetSentences();
                if (found != null)
                {
                    _logger.Info($"Found <<{found.Count()}>> entries.");
                }
                else
                {
                    _logger.Info("Found <<0>> entries.");
                }
                
                return new Result<IEnumerable<Sentence>>(found);
            }
            catch(Exception e)
            {
                _logger.Error(e);
                return new Result<IEnumerable<Sentence>>(null, e);
            }
            
        }
        public async Task<Result<Sentence>> Get(int id)
        {
            try
            {
                _logger.Info($"Selecting entry with ID: <<{id}>>.");
                var entry = await _connector.GetSentence(id);
                if (entry != null)
                {
                    _logger.Info($"Found entry: <<{entry.ID}>>.");
                }
                else
                {
                    _logger.Info($"Entry not found.");
                }
               
                return new Result<Sentence>(entry);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return new Result<Sentence>(null, e);
            }
        }

        public async Task<Result<IEnumerable<Sentence>>> Get(string pattern)
        {
            _logger.Info($"Selecting entry which contains pattern: <<{pattern}>>.");
            if (string.IsNullOrWhiteSpace(pattern))
            {
                _logger.Info($"Pattern is null or empty. Forwarding to Get() method.");
                return await Get();
            }
            try
            {
                var found = await _connector.GetSentences(pattern);
                if(found != null)
                {
                    _logger.Info($"Found <<{found.Count()}>> entries.");
                }
                else
                {
                    _logger.Info("Found <<0>> entries.");
                }
                
                return new Result<IEnumerable<Sentence>>(found);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return new Result<IEnumerable<Sentence>>(null, e);
            }
        }

        public async Task<Result<int>> Add(Sentence entry)
        {
            _logger.Info("Adding new entry to database.");
            if(entry is null)
            {
                _logger.Info("No entry provided.");
                return new Result<int>(422, new ArgumentNullException("Argument is null.")); //Unprocessable entity
            }
            else if (string.IsNullOrWhiteSpace(entry.Primary))
            {
                _logger.Info($"Property Primary is invalid: <<{entry.Primary}>>.");
                return new Result<int>(422, new ArgumentNullException($"Property Primary is not acceptable: <<{entry.Primary}>>.")); //Unprocessable entity
            }
            else if(string.IsNullOrWhiteSpace(entry.Foreign))
            {
                _logger.Info($"Property Foreign is invalid: <<{entry.Foreign}>>.");
                return new Result<int>(422, new ArgumentNullException($"Property Foreign is not acceptable: <<{entry.Foreign}>>.")); //Unprocessable entity
            }
            else if (string.IsNullOrWhiteSpace(entry.Subject))
            {
                _logger.Info($"Property Subject is invalid: <<{entry.Subject}>>.");
                return new Result<int>(422, new ArgumentNullException($"Property Subject is not acceptable: <<{entry.Subject}>>.")); //Unprocessable entity
            }

            var existingEntry = await _connector.GetSentence(entry.Primary, entry.Foreign);
            if (existingEntry != null)
            {
                _logger.Info($"Entry already exists in database: <<{existingEntry.ID}>>.");
                return new Result<int>(409, new ArgumentException("Entry already exists in database.")); //Conflict
            }

            entry.ID = 0;
            entry.LevelOfRecognition = 0;
            try
            {
                await _connector.Add(entry);
                var addedEntry = await _connector.GetSentence(entry.Primary, entry.Foreign);
                _logger.Info($"Entry added: <<{addedEntry.ID}>>.");
                return new Result<int>(200);
            }
            catch(Exception e)
            {
                _logger.Error(e);
                return new Result<int>(500, e);
            }
        }

        public async Task<Result<int>> Update(Sentence entry)
        {
            _logger.Info("Changing entry in database.");
            if (entry is null)
            {
                _logger.Info("No entry provided.");
                return new Result<int>(422, new ArgumentNullException("Argument is null.")); //Unprocessable entity
            }
            else if (string.IsNullOrWhiteSpace(entry.Primary))
            {
                _logger.Info($"Property Primary is invalid: <<{entry.Primary}>>.");
                return new Result<int>(422, new ArgumentNullException($"Property Primary is not acceptable: <<{entry.Primary}>>.")); //Unprocessable entity
            }
            else if (string.IsNullOrWhiteSpace(entry.Foreign))
            {
                _logger.Info($"Property Foreign is invalid: <<{entry.Foreign}>>.");
                return new Result<int>(422, new ArgumentNullException($"Property Foreign is not acceptable: <<{entry.Foreign}>>.")); //Unprocessable entity
            }
            else if (string.IsNullOrWhiteSpace(entry.Subject))
            {
                _logger.Info($"Property Subject is invalid: <<{entry.Subject}>>.");
                return new Result<int>(422, new ArgumentNullException($"Property Subject is not acceptable: <<{entry.Subject}>>.")); //Unprocessable entity
            }

            var existingEntry = await _connector.GetSentence(entry.ID);
            
            if (existingEntry == null)
            {
                _logger.Info("Entry does not exists in database.");
                return new Result<int>(404, new ArgumentException("Entry does not exists in database.")); //Not found
            }

            _logger.Info($"Entry id: <<{existingEntry.ID}>>.");
            try
            {
                await _connector.Update(entry);
                _logger.Info($"Entry updated: <<{entry.ID}>>.");
                return new Result<int>(200);
            }
            catch(Exception e)
            {
                _logger.Error(e);
                return new Result<int>(500, e);
            }
        }

        public async Task<Result<int>> Delete(int id)
        {
            _logger.Info($"Removing entry from database. Passed id: <<{id}>>.");
            if (id < 0)
            {
                _logger.Info($"ID is negative: <<{id}>>");
                return new Result<int>(422, new ArgumentException("ID is negative.")); //Unprocessable entity
            }

            var existingEntry = await _connector.GetSentence(id);

            if (existingEntry == null)
            {
                _logger.Info("Entry does not exists in database.");
                return new Result<int>(404, new ArgumentException("Entry does not exists in database.")); //Not found
            }

            _logger.Info($"Entry id: <<{existingEntry.ID}>>.");
            try
            {
                await _connector.Delete(existingEntry);
                return new Result<int>(200);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return new Result<int>(500, e);
            }
        }
    }
}
