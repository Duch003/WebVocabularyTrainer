using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using NSubstitute;
using RestApi.Context;
using RestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestApiTests.Factories
{
    public static class VocabularyContextFactory
    {
        private static VocabularyContext _errorInstance = null;
        private static object _errorLock = new object();
        private static VocabularyContext _fineInstance = null;
        private static object _fineLock = new object();
        public static VocabularyContext FineInstance()
        {
            lock (_fineLock)
            {
                if(_fineInstance == null)
                {
                    var options = new DbContextOptionsBuilder<VocabularyContext>()
                        .UseInMemoryDatabase(databaseName: "TestDb")
                        .Options;

                    var context = new VocabularyContext(options);
                    context.Sentences.AddRange(SentenceFactory.GetSentences());
                    context.SaveChanges();

                    _fineInstance = context;
                }
                return _fineInstance;
            }
        }

        public static VocabularyContext ErrorInstance()
        {
            lock (_errorLock)
            {
                if (_errorInstance == null)
                {
                    var sentences = SentenceFactory.GetSentences();
                    var vocabularyMock = new Mock<DbSet<Sentence>>();
                    vocabularyMock.As<IQueryable<Sentence>>().Setup(m => m.Provider).Returns(sentences.Provider);
                    vocabularyMock.As<IQueryable<Sentence>>().Setup(m => m.Expression).Returns(sentences.Expression);
                    vocabularyMock.As<IQueryable<Sentence>>().Setup(m => m.ElementType).Returns(sentences.ElementType);
                    vocabularyMock.As<IQueryable<Sentence>>().Setup(m => m.GetEnumerator()).Returns(sentences.GetEnumerator());

                    var vocabularyContextMock = new Mock<VocabularyContext>();
                    vocabularyContextMock.Setup(x => x.Set<Sentence>()).Returns(vocabularyMock.Object);
                    vocabularyContextMock.Setup(x => x.SaveChanges()).Throws<Exception>();

                    _errorInstance = vocabularyContextMock.Object;
                }
                return _errorInstance;
            }
        }
    }
}
