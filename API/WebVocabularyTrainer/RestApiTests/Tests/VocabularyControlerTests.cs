using Moq;
using RestApi.Context;
using RestApi.Models;
using RestApiTests.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RestApiTests.Tests
{
    public class VocabularyControlerTests
    {
        private List<Sentence> _dbOutput;
        private IVocabularyContext _context;

        public VocabularyControlerTests()
        {
            _dbOutput = SentenceFactory.GetSentences();
            var mockContext = new Mock<IVocabularyContext>();
            mockContext.As<IQueryable>().Setup(x => x.Provider)

        }

        [Fact]
        public void Get_ReturnsAllEtities()
        {

        }
    }
}
