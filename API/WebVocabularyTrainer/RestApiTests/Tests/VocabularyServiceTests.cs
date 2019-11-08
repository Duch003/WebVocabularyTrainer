using Microsoft.EntityFrameworkCore;
using Moq;
using RestApi.Context;
using RestApi.Models;
using RestApi.Services;
using RestApiTests.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RestApiTests.Tests
{
    public class VocabularyServiceTests
    {
        private VocabularyService _errorService;
        private VocabularyService _fineService;
        private IQueryable<Sentence> _entries;
        private Sentence _entry;
        private Sentence _badIdEntry;
        public VocabularyServiceTests()
        {
            _fineService = new VocabularyService(VocabularyContextFactory.FineInstance());
            _errorService = new VocabularyService(VocabularyContextFactory.ErrorInstance());
            _entries = SentenceFactory.GetSentences();
            _entry = new Sentence
            {
                ID = 0,
                Foreign = "Another one",
                Primary = "Kolejny",
                Subject = "Test"
            };
            _badIdEntry = new Sentence
            {
                ID = 5,
                Foreign = "Invalid",
                Primary = "Nieprawidłowy",
                Subject = "Test"
            };
        }

        [Fact]
        public void Get_ReturnsAllEntries()
        {
            //Act
            var output = _fineService.Get();

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.True(output.Output.Count() == _entries.Count());
        }

        [Fact]
        public void GetWithInt_ValidIdPassed_ReturnsResultWithEntry()
        {
            //Arrange
            var entry = _entries.SingleOrDefault(item => item.ID == 1);

            //Act
            var output = _fineService.Get(1);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.NotNull(output.Output);
            Assert.Equal(output.Output, entry);
        }

        [Fact]
        public void GetWithInt_NegativeIdPassed_ReturnsResultWithException()
        {
            //Act
            var output = _fineService.Get(-1);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Null(output.Output);
        }

        [Fact]
        public void GetWithInt_NoExistingIdPassed_ReturnsResultWithNullEntry()
        {
            //Act
            var output = _fineService.Get(-1);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.Null(output.Output);
        }

        [Fact]
        public void GetWithString_FiltersEntriesByPrimaryForeignSubjectSourceDescription_ReturnsResultWithFilteredEntries()
        {
            //Arrange
            var pattern1 = "Dom"; //Primary
            var output1 = _entries.Where(item => item.Description.Contains(pattern1)
                || item.Foreign.Contains(pattern1)
                || item.Primary.Contains(pattern1)
                || item.Subject.Contains(pattern1)
                || item.Source.Contains(pattern1));

            var pattern2 = "Table"; //Foreign
            var output2 = _entries.Where(item => item.Description.Contains(pattern2)
                || item.Foreign.Contains(pattern2)
                || item.Primary.Contains(pattern2)
                || item.Subject.Contains(pattern2)
                || item.Source.Contains(pattern2));

            var pattern3 = "Kitchen"; //Subject
            var output3 = _entries.Where(item => item.Description.Contains(pattern3)
                || item.Foreign.Contains(pattern3)
                || item.Primary.Contains(pattern3)
                || item.Subject.Contains(pattern3)
                || item.Source.Contains(pattern3));

            var pattern4 = "Dictionary"; //Source
            var output4 = _entries.Where(item => item.Description.Contains(pattern4)
                || item.Foreign.Contains(pattern4)
                || item.Primary.Contains(pattern4)
                || item.Subject.Contains(pattern4)
                || item.Source.Contains(pattern4));

            var pattern5 = "machine"; //Description
            var output5 = _entries.Where(item => item.Description.Contains(pattern5)
                || item.Foreign.Contains(pattern5)
                || item.Primary.Contains(pattern5)
                || item.Subject.Contains(pattern5)
                || item.Source.Contains(pattern5));
            //Act
            var test1 = _fineService.Get(pattern1);
            var test2 = _fineService.Get(pattern2);
            var test3 = _fineService.Get(pattern3);
            var test4 = _fineService.Get(pattern4);
            var test5 = _fineService.Get(pattern5);

            //Assert
            Assert.True(test1.IsFine);
            Assert.Null(test1.Exception);
            Assert.NotNull(test1.Output);
            Assert.Equal(test1.Output, output1);

            Assert.True(test2.IsFine);
            Assert.Null(test2.Exception);
            Assert.NotNull(test2.Output);
            Assert.Equal(test2.Output, output2);

            Assert.True(test3.IsFine);
            Assert.Null(test3.Exception);
            Assert.NotNull(test3.Output);
            Assert.Equal(test3.Output, output3);

            Assert.True(test4.IsFine);
            Assert.Null(test4.Exception);
            Assert.NotNull(test4.Output);
            Assert.Equal(test4.Output, output4);

            Assert.True(test5.IsFine);
            Assert.Null(test5.Exception);
            Assert.NotNull(test5.Output);
            Assert.Equal(test5.Output, output5);
        }

        [Fact]
        public void GetWithString_PatternIsNull_ReturnsResultWithAllEntries()
        {
            //Act
            var output = _fineService.Get(null);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.NotNull(output.Output);
            Assert.Equal(output.Output.Count(), _entries.Count());
        }

        [Fact]
        public void GetWithString_PatternIsEmpty_ReturnsResultWithAllEntries()
        {
            //Act
            var output = _fineService.Get(string.Empty);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.NotNull(output.Output);
            Assert.Equal(output.Output.Count(), _entries.Count());
        }

        [Fact]
        public void GetWithString_PatternIsWhiteSpace_ReturnsResultWithAllEntries()
        {
            //Act
            var output = _fineService.Get("   ");

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.NotNull(output.Output);
            Assert.Equal(output.Output.Count(), _entries.Count());
        }

        [Fact]
        public void GetWithString_PatternIsValidButDoesNotMatchAnything_ReturnsResultWithNoEntries()
        {
            //Act
            var output = _fineService.Get("ThisDoesNotMatchAnythingForSure");

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.NotNull(output.Output);
            Assert.True(0 == output.Output.Count());
        }

        [Fact]
        public void Add_ReturnsResult200AndAddsUser()
        {
            //Act
            var output = _fineService.Add(_entry);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.Equal(200, output.Output);
            Assert.NotNull(_entries.SingleOrDefault(item => item.Primary.Equals("Kolejny")));
        }

        [Fact]
        public void Add_InputIsNull_ReturnsResult422WithException()
        {
            //Act
            var output = _fineService.Add(null);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public void Add_InputIdIsDifferentFromZero_ReturnsResult200AndAddsUser()
        {
            //Act
            var output = _fineService.Add(_badIdEntry);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.Equal(200, output.Output);
            Assert.NotNull(_entries.SingleOrDefault(item => item.Primary.Equals("Nieprawidłowy")));
        }
    }
}
