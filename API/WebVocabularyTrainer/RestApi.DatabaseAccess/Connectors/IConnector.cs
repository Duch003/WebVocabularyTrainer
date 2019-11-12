using System.Collections.Generic;
using System.Threading.Tasks;
using RestApi.Data.Models;

namespace RestApi.DatabaseAccess.Connectors
{
    public interface IConnector
    {
        Task AddAsync(Sentence sentence);
        Task DeleteAsync(Sentence sentence);
        Task<Sentence> GetSentenceAsync(string primary, string foreign);
        Task<Sentence> GetSentenceAsync(int id);
        Task<IEnumerable<Sentence>> GetSentencesAsync();
        Task<IEnumerable<Sentence>> GetSentencesAsync(string pattern);
        Task UpdateAsync(Sentence sentence);
    }
}