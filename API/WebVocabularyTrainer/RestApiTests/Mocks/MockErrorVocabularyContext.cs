using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestApiTests.Mocks
{
    public class MockErrorVocabularyContext : MockVocabularyContext
    {
        public void SpecialSaveChanges() => base.SaveChanges();
        public override int SaveChanges() 
            => throw new Exception();
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken)) 
            => throw new Exception();
    }
}
