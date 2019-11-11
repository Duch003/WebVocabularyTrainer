using System.Collections.Generic;
using System.Threading.Tasks;
using RestApi.Data.Models;

namespace RestApi.DatabaseAccess.Connectors
{
    public interface IConnector
    {
        Task Add(Sentence sentence);
        Task Delete(Sentence sentence);
        Task<Sentence> GetSentence(string primary, string foreign);
        Task<Sentence> GetSentence(int id);
        Task<IEnumerable<Sentence>> GetSentences();
        Task<IEnumerable<Sentence>> GetSentences(string pattern);
        Task Update(Sentence sentence);
    }
}