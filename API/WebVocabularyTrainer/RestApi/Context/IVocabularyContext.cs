using Microsoft.EntityFrameworkCore;
using RestApi.Models;

namespace RestApi.Context
{
    public interface IVocabularyContext
    {
        DbSet<Sentence> Sentences { get; set; }
    }
}