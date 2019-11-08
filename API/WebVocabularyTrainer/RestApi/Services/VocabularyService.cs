using NLog;
using RestApi.Context;
using RestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Services
{
    public class VocabularyService
    {
        private readonly VocabularyContext _context;
        private readonly Logger _logger;

        public VocabularyService(VocabularyContext dbContext)
        {
            _context = dbContext;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public Result<IEnumerable<Sentence>> Get()
        {
            try
            {
                _logger.Info("Selecting all entries.");
                var found = _context.Sentences
                    .ToArray();
                _logger.Info($"Found <<{found.Count()}>> entries.");
                return new Result<IEnumerable<Sentence>>(found);
            }
            catch(Exception e)
            {
                _logger.Error(e);
                return new Result<IEnumerable<Sentence>>(null, e);
            }
            
        }
        public Result<Sentence> Get(int id)
        {
            try
            {
                _logger.Info($"Selecting entry with ID: <<{id}>>.");
                var entry = _context.Sentences
                    .SingleOrDefault(item => item.ID == id);
                _logger.Info($"Found entry: <<{entry.ID}>>.");
                return new Result<Sentence>(entry);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return new Result<Sentence>(null, e);
            }
        }

        public Result<IEnumerable<Sentence>> Get(string pattern)
        {
            _logger.Info($"Selecting entry which contains pattern: <<{pattern}>>.");
            if (string.IsNullOrEmpty(pattern))
            {
                _logger.Info($"Pattern is null or empty. Forwarding to Get() method.");
                return Get();
            }
            try
            {
                var found = _context.Sentences
                .Where(item => item.Description.Contains(pattern)
                || item.Foreign.Contains(pattern)
                || item.Primary.Contains(pattern)
                || item.Subject.Contains(pattern)
                || item.Source.Contains(pattern));
                _logger.Info($"Found <<{found.Count()}>> entries.");
                return new Result<IEnumerable<Sentence>>(found);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return new Result<IEnumerable<Sentence>>(null, e);
            }
        }

        public Result<int> Add(Sentence entry)
        {
            _logger.Info("Adding new entry to database.");
            if(entry is null)
            {
                _logger.Info("No entry provided.");
                return new Result<int>(422, new ArgumentNullException("Argument is null.")); //Unprocessable entity
            }

            var existingEntry = _context.Sentences
                .SingleOrDefault(item => item.Foreign.Equals(entry.Foreign) && item.Primary.Equals(entry.Primary));
            if (existingEntry != null)
            {
                _logger.Info($"Entry already exists in database: <<{existingEntry.ID}>>.");
                return new Result<int>(409, new ArgumentException("Entry already exists in database.")); //Conflict
            }

            entry.ID = 0;
            try
            {
                var newEntry = _context.Sentences.Add(entry);
                _context.SaveChanges();
                _logger.Info($"Entry added: <<{newEntry.Entity.ID}>>.");
                return new Result<int>(200);
            }
            catch(Exception e)
            {
                _logger.Error(e);
                return new Result<int>(500, e);
            }
        }

        public Result<int> Update(Sentence entry)
        {
            _logger.Info("Changing entry in database.");
            if (entry is null)
            {
                _logger.Info("No entry provided.");
                return new Result<int>(422, new ArgumentNullException("Argument is null.")); //Unprocessable entity
            }

            var existingEntry = _context.Sentences
                .SingleOrDefault(item => item.ID == entry.ID);
            if (existingEntry == null)
            {
                _logger.Info("Entry does not exists in database.");
                return new Result<int>(404, new ArgumentException("Entry does not exists in database.")); //Not found
            }

            _logger.Info($"Entry id: <<{existingEntry.ID}>>.");
            try
            {
                _context.Sentences.Update(entry);
                _context.SaveChanges();
                return new Result<int>(200);
            }
            catch(Exception e)
            {
                _logger.Error(e);
                return new Result<int>(500, e);
            }
        }

        public Result<int> Delete(int id)
        {
            _logger.Info("Removing entry from database.");
            if (id < 0)
            {
                _logger.Info($"ID is negative: <<{id}>>");
                return new Result<int>(422, new ArgumentException("ID is negative.")); //Unprocessable entity
            }

            var existingEntry = _context.Sentences
                .SingleOrDefault(item => item.ID == id);
            if (existingEntry == null)
            {
                _logger.Info("Entry does not exists in database.");
                return new Result<int>(404, new ArgumentException("Entity does not exists in database.")); //Not found
            }

            _logger.Info($"Entry id: <<{existingEntry.ID}>>.");
            try
            {
                _context.Sentences.Remove(existingEntry);
                _context.SaveChanges();
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
