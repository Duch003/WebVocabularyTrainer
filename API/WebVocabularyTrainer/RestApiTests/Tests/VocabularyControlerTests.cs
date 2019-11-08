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
        private IQueryable<Sentence> _dbOutput;

        public VocabularyControlerTests()
        {
            _dbOutput = SentenceFactory.GetSentences();

        }

        [Fact]
        public void Get_ReturnsAllEtities()
        {

        }
    }
}
