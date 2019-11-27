using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RestApi.Data.Models;
using RestApi.DatabaseAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.DatabaseAccess.Connectors
{
    public class EFSentenceConnector : ISentenceConnector
    {
        private readonly VocabularyContext _context;

        public EFSentenceConnector()
        {
            _context = new VocabularyContext();
        }

        //-1 => null
        //11 => null
        public async Task<Sentence> GetSentenceAsync(int id) 
            => await _context.Sentences
            .SingleOrDefaultAsync(item => item.ID == id);
        
        //null => ArgumentNullException
        //empty => All
        public async Task<IEnumerable<Sentence>> GetSentencesAsync(string pattern)
            => await _context.Sentences
            .Where(item => item.Description.Contains(pattern)
                || item.Foreign.Contains(pattern)
                || item.Primary.Contains(pattern)
                || item.Subject.Contains(pattern)
                || item.Source.Contains(pattern))
            .ToListAsync();

        //zły, zły => null
        //null, zły => null
        //null, null => null
        //empty, empty => null
        public async Task<Sentence> GetSentenceAsync(string primary, string foreign)
            => await _context.Sentences
            .Where(item => item.Primary.Equals(primary) && item.Foreign.Equals(foreign))
            .FirstOrDefaultAsync();
            
        public async Task<IEnumerable<Sentence>> GetSentencesAsync() 
            => await _context.Sentences
            .ToListAsync();

        //ID 5 => InvalidOperationException
        //ID -1 - just adds id
        //null => ArgumentNullException
        //Prop null => Microsoft.EntityFrameworkCore.DbUpdateException => SqlException 
        public async Task AddAsync(Sentence sentence)
        {
            _context.Sentences.Add(sentence);
            await _context.SaveChangesAsync();
        }

        //Noexisting => Microsoft.EntityFrameworkCore.DbUpdateException => SqlException 
        //Property set to null => Microsoft.EntityFrameworkCore.DbUpdateException => SqlException 
        //null => ArgumentNullException: 
        public async Task UpdateAsync(Sentence sentence)
        {
            var current = _context.Sentences.First(item => item.ID == sentence.ID);
            if(current != null)
            {
                _context.Entry(current).State = EntityState.Detached;
            } 

            _context.Entry(sentence).State = EntityState.Modified;
            
            await _context.SaveChangesAsync();
        }

        //Noexisting with invalid id (1) => Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException: 
        //Noexisting with valid ID (0) => InvalidOperationException
        //Null => ArgumentNullException
        public async Task DeleteAsync(Sentence sentence)
        {
            _context.Sentences.Remove(sentence);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetAllSources() => 
            await _context.Sentences
            .Select(item => item.Source)
            .Distinct()
            .ToListAsync();
        public async Task<IEnumerable<string>> GetAllSubjects() =>
            await _context.Sentences
            .Select(item => item.Subject)
            .Distinct()
            .ToListAsync();
    }
}
