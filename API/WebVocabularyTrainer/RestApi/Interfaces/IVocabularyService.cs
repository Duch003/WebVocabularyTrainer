using System.Collections.Generic;
using System.Threading.Tasks;
using RestApi.Data.Models;

namespace RestApi.Interfaces
{
    public interface IVocabularyService
    {
        Task<Result<int>> AddAsync(Sentence entry);
        Task<Result<int>> DeleteAsync(int id);
        Task<Result<IEnumerable<Sentence>>> GetAsync();
        Task<Result<Sentence>> GetAsync(int id);
        Task<Result<IEnumerable<Sentence>>> GetAsync(string pattern);
        Task<Result<int>> UpdateAsync(Sentence entry);
    }
}