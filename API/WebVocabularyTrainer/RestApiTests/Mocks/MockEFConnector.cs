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
    public class MockEFConnector : IConnector
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

        public async Task Add(Sentence sentence)
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
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Sentence sentence)
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
            await _context.SaveChangesAsync();
        }

        public async Task<Sentence> GetSentence(string primary, string foreign)
            => await _context.Sentences.FirstOrDefaultAsync(item => item.Primary == primary && item.Foreign == foreign);

        public async Task<Sentence> GetSentence(int id)
            => await _context.Sentences.SingleOrDefaultAsync(item => item.ID == id);

        public async Task<IEnumerable<Sentence>> GetSentences()
            => await _context.Sentences.ToListAsync();

        public async Task<IEnumerable<Sentence>> GetSentences(string pattern)
            => await _context.Sentences
            .Where(item => item.Description.Contains(pattern)
                || item.Foreign.Contains(pattern)
                || item.Primary.Contains(pattern)
                || item.Subject.Contains(pattern)
                || item.Source.Contains(pattern))
            .ToListAsync();

        public async Task Update(Sentence sentence)
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
                throw new DbUpdateException("Message", inner);
            }

            _context.Update(sentence);
            await _context.SaveChangesAsync();
        }
    }
}
