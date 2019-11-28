using Microsoft.EntityFrameworkCore;
using RestApi.Data.Models;
using RestApi.DatabaseAccess.Connectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiTests.Mocks
{
    public class MockEFConnector : ISentenceConnector
    {
        private MockVocabularyContext _context;
        public MockVocabularyContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        internal MockEFConnector(MockVocabularyContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Sentence sentence)
        {
            if (sentence is null)
            {
                throw new ArgumentNullException();
            }
            else if (_context.Sentences.Any(item => item == sentence) || _context.Sentences.Any(item => item.ID == sentence.ID))
            {
                throw new InvalidOperationException();
            }
            else if (sentence.Primary is null || sentence.Foreign is null || sentence.Subject is null)
            {
                var inner = new Exception();
                throw new DbUpdateException("Message", inner);
            }

            _context.Sentences.Add(sentence);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(Sentence sentence)
        {
            if (sentence is null)
            {
                throw new ArgumentNullException();
            }
            else if (sentence.ID == 0)
            {
                throw new InvalidOperationException();
            }
            else if (!_context.Sentences.Any(item => item == sentence) && !_context.Sentences.Any(item => item.ID == sentence.ID))
            {
                var inner = new Exception();
                throw new DbUpdateConcurrencyException("Message", inner);
            }

            var entry = _context.Sentences.First(item => item.ID == sentence.ID);
            _context.Sentences.Remove(entry);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Sentence> GetSentenceAsync(string primary, string foreign)
            => await _context.Sentences.FirstOrDefaultAsync(item => item.Primary == primary && item.Foreign == foreign).ConfigureAwait(false);

        public async Task<Sentence> GetSentenceAsync(int id)
            => await _context.Sentences.SingleOrDefaultAsync(item => item.ID == id).ConfigureAwait(false);

        public async Task<IEnumerable<Sentence>> GetSentencesAsync()
            => await _context.Sentences.ToListAsync().ConfigureAwait(false);

        public async Task<IEnumerable<Sentence>> GetSentencesAsync(string pattern)
            => await _context.Sentences
            .Where(item => item.Description.Contains(pattern, StringComparison.InvariantCultureIgnoreCase)
                || item.Foreign.Contains(pattern, StringComparison.InvariantCultureIgnoreCase)
                || item.Primary.Contains(pattern, StringComparison.InvariantCultureIgnoreCase)
                || item.Subject.Contains(pattern, StringComparison.InvariantCultureIgnoreCase)
                || item.Source.Contains(pattern, StringComparison.InvariantCultureIgnoreCase))
            .ToListAsync()
            .ConfigureAwait(false);

        public async Task UpdateAsync(Sentence sentence)
        {
            if (sentence is null)
            {
                throw new ArgumentNullException();
            }
            else if (!_context.Sentences.Any(item => item == sentence) && !_context.Sentences.Any(item => item.ID == sentence.ID))
            {
                var inner = new Exception();
                throw new DbUpdateException("Message", inner);
            }
            else if (sentence.Primary is null || sentence.Foreign is null || sentence.Subject is null)
            {
                var inner = new Exception();
                throw new DbUpdateException("Message2", inner);
            }

            _context.Update(sentence);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<string>> GetAllSources() =>
             await _context.Sentences
             .Select(item => item.Source)
             .Distinct()
             .ToListAsync()
             .ConfigureAwait(false);

        public async Task<IEnumerable<string>> GetAllSubjects() =>
            await _context.Sentences
            .Select(item => item.Subject)
            .Distinct()
            .ToListAsync()
            .ConfigureAwait(false);
    }
}
