using Microsoft.EntityFrameworkCore;
using Moq;
using RestApi.Data.Models;
using RestApi.Services;
using RestApiTests.Factories;
using RestApiTests.Mocks;
using RestApiTests.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace RestApiTests.Tests
{
    public class VocabularyServiceTests
    {
        private VocabularyServiceLayer _errorService;
        private VocabularyServiceLayer _service;
        private MockEFConnector _connector;
        private Sentence _entry;
        private Sentence _fullEntry;
        private Sentence _badIdEntry;
        private Sentence _badSubjectEntry;
        private Sentence _badSourceEntry;
        private Sentence _negativeIdEntry;
        private Sentence _nullPrimaryEntry;
        private Sentence _emptyPrimaryEntry;
        private Sentence _whiteSpacePrimaryEntry;
        private Sentence _nullForeignEntry;
        private Sentence _emptyForeignEntry;
        private Sentence _whiteSpaceForeignEntry;
        private Sentence _nullSubjectEntry;
        private Sentence _emptySubjectEntry;
        private Sentence _whiteSpaceSubjectEntry;
        public VocabularyServiceTests()
        {
            _connector = EFConnectorFactory.Instance();
            _service = new VocabularyServiceLayer(_connector);
            _errorService = new VocabularyServiceLayer(EFConnectorFactory.ErrorInstance());
            _entry = new Sentence
            {
                ID = 0,
                Foreign = "Another one",
                Primary = "Kolejny",
                Subject = "Test"
            };
            _fullEntry = new Sentence
            {
                ID = 0,
                Foreign = "Filled",
                Primary = "Wypełniony",
                Subject = "Test",
                Description = "This is sample description",
                ExamplesArray = new[] {"This is sample sentence with word filled.", "This is another one."},
                Source = "UnitTests"
            };
            _badIdEntry = new Sentence
            {
                ID = 5,
                Foreign = "Invalid",
                Primary = "Nieprawidłowy",
                Subject = "Test"
            };
            _badSubjectEntry = new Sentence
            {
                ID = 5,
                Foreign = "Invalid",
                Primary = "Nieprawidłowy",
                Subject = "All"
            };
            _badSourceEntry = new Sentence
            {
                ID = 5,
                Foreign = "Invalid",
                Primary = "Nieprawidłowy",
                Subject = "Test",
                Source = "All"
            };
            _negativeIdEntry = new Sentence
            {
                ID = -5,
                Foreign = "Negative",
                Primary = "Negatywny",
                Subject = "Test"
            };

            //Primary
            _nullPrimaryEntry = new Sentence
            {
                ID = 0,
                Foreign = "Invalid",
                Primary = null,
                Subject = "Test"
            };
            _emptyPrimaryEntry = new Sentence
            {
                ID = 0,
                Foreign = "Invalid",
                Primary = string.Empty,
                Subject = "Test"
            };
            _whiteSpacePrimaryEntry = new Sentence
            {
                ID = 0,
                Foreign = "Invalid",
                Primary = " ",
                Subject = "Test"
            };

            //Foreign
            _nullForeignEntry = new Sentence
            {
                ID = 0,
                Foreign = null,
                Primary = "Nieprawidłowy",
                Subject = "Test"
            };
            _emptyForeignEntry = new Sentence
            {
                ID = 0,
                Foreign = string.Empty,
                Primary = "Nieprawidłowy",
                Subject = "Test"
            };
            _whiteSpaceForeignEntry = new Sentence
            {
                ID = 0,
                Foreign = " ",
                Primary = "Nieprawidłowy",
                Subject = "Test"
            };

            //Subject
            _nullSubjectEntry = new Sentence
            {
                ID = 0,
                Foreign = "Invalid",
                Primary = "Nieprawidłowy",
                Subject = null
            };
            _emptySubjectEntry = new Sentence
            {
                ID = 0,
                Foreign = "Invalid",
                Primary = "Nieprawidłowy",
                Subject = string.Empty
            };
            _whiteSpaceSubjectEntry = new Sentence
            {
                ID = 0,
                Foreign = "Invalid",
                Primary = "Nieprawidłowy",
                Subject = " "
            };
        }

        [Fact]
        public async void Get_ReturnsAllEntries()
        {
            //Act
            var output = await _service.GetAsync().ConfigureAwait(false);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.True(output.Output.Count() == _connector.Context.Sentences.Count());
        }

        [Fact]
        public async void GetWithInt_ValidIdPassed_ReturnsResultWithEntry()
        {
            //Arrange
            var entry = _connector.Context.Sentences.SingleOrDefault(item => item.ID == 1);

            //Act
            var output = await _service.GetAsync(1).ConfigureAwait(false);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.NotNull(output.Output);
            Assert.Equal(output.Output, entry);
        }
        
        [Fact]
        public async void GetWithInt_NegativeIdPassed_ReturnsResultWithNull()
        {
            //Act
            var output = await _service.GetAsync(-1).ConfigureAwait(false);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.Null(output.Output);
        }
        
        [Fact]
        public async void GetWithInt_NoExistingIdPassed_ReturnsResultWithNullEntry()
        {
            //Act
            var output = await _service.GetAsync(1000).ConfigureAwait(false);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.Null(output.Output);
        }

        [Fact]
        public async void GetWithString_FiltersEntriesByPrimaryForeignSubjectSourceDescription_ReturnsResultWithFilteredEntries()
        {
            //Arrange
            var pattern1 = "Dom"; //Primary
            var output1 = _connector.Context.Sentences.Where(item => item.Description.Contains(pattern1, StringComparison.InvariantCultureIgnoreCase)
                || item.Foreign.Contains(pattern1, StringComparison.InvariantCultureIgnoreCase)
                || item.Primary.Contains(pattern1, StringComparison.InvariantCultureIgnoreCase)
                || item.Subject.Contains(pattern1, StringComparison.InvariantCultureIgnoreCase)
                || item.Source.Contains(pattern1, StringComparison.InvariantCultureIgnoreCase));

            var pattern2 = "Table"; //Foreign
            var output2 = _connector.Context.Sentences.Where(item => item.Description.Contains(pattern2, StringComparison.InvariantCultureIgnoreCase)
                || item.Foreign.Contains(pattern2, StringComparison.InvariantCultureIgnoreCase)
                || item.Primary.Contains(pattern2, StringComparison.InvariantCultureIgnoreCase)
                || item.Subject.Contains(pattern2, StringComparison.InvariantCultureIgnoreCase)
                || item.Source.Contains(pattern2, StringComparison.InvariantCultureIgnoreCase));

            var pattern3 = "Kitchen"; //Subject
            var output3 = _connector.Context.Sentences.Where(item => item.Description.Contains(pattern3, StringComparison.InvariantCultureIgnoreCase)
                || item.Foreign.Contains(pattern3, StringComparison.InvariantCultureIgnoreCase)
                || item.Primary.Contains(pattern3, StringComparison.InvariantCultureIgnoreCase)
                || item.Subject.Contains(pattern3, StringComparison.InvariantCultureIgnoreCase)
                || item.Source.Contains(pattern3, StringComparison.InvariantCultureIgnoreCase));

            var pattern4 = "Dictionary"; //Source
            var output4 = _connector.Context.Sentences.Where(item => item.Description.Contains(pattern4, StringComparison.InvariantCultureIgnoreCase)
                || item.Foreign.Contains(pattern4, StringComparison.InvariantCultureIgnoreCase)
                || item.Primary.Contains(pattern4, StringComparison.InvariantCultureIgnoreCase)
                || item.Subject.Contains(pattern4, StringComparison.InvariantCultureIgnoreCase)
                || item.Source.Contains(pattern4, StringComparison.InvariantCultureIgnoreCase));

            var pattern5 = "machine"; //Description
            var output5 = _connector.Context.Sentences.Where(item => item.Description.Contains(pattern5, StringComparison.InvariantCultureIgnoreCase)
                || item.Foreign.Contains(pattern5, StringComparison.InvariantCultureIgnoreCase)
                || item.Primary.Contains(pattern5, StringComparison.InvariantCultureIgnoreCase)
                || item.Subject.Contains(pattern5, StringComparison.InvariantCultureIgnoreCase)
                || item.Source.Contains(pattern5, StringComparison.InvariantCultureIgnoreCase));
            //Act
            var test1 = await _service.GetAsync(pattern1).ConfigureAwait(false);
            var test2 = await _service.GetAsync(pattern2).ConfigureAwait(false);
            var test3 = await _service.GetAsync(pattern3).ConfigureAwait(false);
            var test4 = await _service.GetAsync(pattern4).ConfigureAwait(false);
            var test5 = await _service.GetAsync(pattern5).ConfigureAwait(false);

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
        public async void GetWithString_PatternIsNull_ReturnsResultWithAllEntries()
        {
            //Act
            var output = await _service.GetAsync(null).ConfigureAwait(false);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.NotNull(output.Output);
            Assert.Equal(output.Output.Count(), _connector.Context.Sentences.Count());
        }

        [Fact]
        public async void GetWithString_PatternIsEmpty_ReturnsResultWithAllEntries()
        {
            //Act
            var output = await _service.GetAsync(string.Empty).ConfigureAwait(false);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.NotNull(output.Output);
            Assert.Equal(output.Output.Count(), _connector.Context.Sentences.Count());
        }

        [Fact]
        public async void GetWithString_PatternIsWhiteSpace_ReturnsResultWithAllEntries()
        {
            //Act
            var output = await _service.GetAsync("   ").ConfigureAwait(false);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.NotNull(output.Output);
            Assert.Equal(output.Output.Count(), _connector.Context.Sentences.Count());
        }

        [Fact]
        public async void GetWithString_PatternIsValidButDoesNotMatchAnything_ReturnsResultWithNoEntries()
        {
            //Act
            var output = await _service.GetAsync("ThisDoesNotMatchAnythingForSure").ConfigureAwait(false);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.NotNull(output.Output);
            Assert.True(!output.Output.Any());
        }

        [Fact]
        public async void Add_ReturnsResult200AndUserHasDefaultValuesInOtherProperties()
        {
            //Act
            var output = await _service.AddAsync(_entry).ConfigureAwait(false);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.Equal(200, output.Output);

            var entry = _connector.Context.Sentences.SingleOrDefault(item => item.Primary.Equals("Kolejny", StringComparison.InvariantCultureIgnoreCase));
            Assert.NotNull(entry);
            Assert.Equal(0d, entry.LevelOfRecognition);
            Assert.True(entry.Examples is null);
            Assert.True(entry.Description is null);
            Assert.True(entry.Source is null);
        }

        [Fact]
        public async void Add_EntryHasFilledOptionalFields_ReturnsResult200AndUserHasProperlyFilledProperties()
        {
            //Act
            var output = await _service.AddAsync(_fullEntry).ConfigureAwait(false);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.Equal(200, output.Output);

            //Description = "This is sample description",
            //ExamplesArray = new[] { "This is sample sentence with word filled.", "This is another one." },
            //Source = "UnitTests"

            var entry = _connector.Context.Sentences.SingleOrDefault(item => item.Primary.Equals("Wypełniony", StringComparison.InvariantCultureIgnoreCase));
            Assert.NotNull(entry);
            Assert.Equal(0d, entry.LevelOfRecognition);
            Assert.True(entry.Examples == "This is sample sentence with word filled.;This is another one.");
            Assert.True(entry.Description == "This is sample description");
            Assert.True(entry.Source == "UnitTests");
        }

        [Fact]
        public async void Add_ForbidsSourceWithValueAll_ReturnsResult422WithError()
        {
            //Act
            var output = await _service.AddAsync(_badSourceEntry).ConfigureAwait(false);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Add_ForbidsSubjectWithValueAll_ReturnsResult422WithError()
        {
            //Act
            var output = await _service.AddAsync(_badSubjectEntry).ConfigureAwait(false);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }


        [Fact]
        public async void Add_InputIsNull_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.AddAsync(null).ConfigureAwait(false);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Add_InputsIdIsLargerThanZero_ReturnsResult200AndAddsUser()
        {
            //Act
            var output = await _service.AddAsync(_negativeIdEntry).ConfigureAwait(false);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.Equal(200, output.Output);
            Assert.NotNull(_connector.Context.Sentences.SingleOrDefault(item => item.Primary.Equals("Nieprawidłowy", StringComparison.InvariantCultureIgnoreCase)));
        }

        [Fact]
        public async void Add_InputsIdIsLesserThanZero_ReturnsResult200AndAddsUser()
        {
            //Act
            var output = await _service.AddAsync(_badIdEntry).ConfigureAwait(false);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.Equal(200, output.Output);
            Assert.NotNull(_connector.Context.Sentences.SingleOrDefault(item => item.Primary.Equals("Nieprawidłowy", StringComparison.InvariantCultureIgnoreCase)));
        }

        [Fact]
        public async void Add_InputHasNullPrimaryProperty_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.AddAsync(_nullPrimaryEntry).ConfigureAwait(false);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Add_InputHasEmptyPrimaryProperty_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.AddAsync(_emptyPrimaryEntry).ConfigureAwait(false);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Add_InputHasWhiteSpacePrimaryProperty_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.AddAsync(_whiteSpacePrimaryEntry).ConfigureAwait(false);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Add_InputHasNullForeignProperty_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.AddAsync(_nullForeignEntry).ConfigureAwait(false);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Add_InputHasEmptyForeignProperty_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.AddAsync(_emptyForeignEntry).ConfigureAwait(false);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Add_InputHasWhiteSpaceForeignProperty_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.AddAsync(_whiteSpaceForeignEntry).ConfigureAwait(false);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Add_InputHasNullSubjectProperty_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.AddAsync(_nullSubjectEntry).ConfigureAwait(false);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Add_InputHasEmptySubjectProperty_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.AddAsync(_emptySubjectEntry).ConfigureAwait(false);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Add_InputHasWhiteSpaceSubjectProperty_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.AddAsync(_whiteSpaceSubjectEntry).ConfigureAwait(false);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Add_ServerError_ReturnsResult500WithException()
        {
            //Act
            var output = await _errorService.AddAsync(_entry).ConfigureAwait(false);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(500, output.Output);
        }

        [Fact]
        public async void Update_InputIsValid_ReturnsResult200()
        {
            //Arrange
            var update = _connector.Context.Sentences.SingleOrDefault(item => item.ID == 1);
            update.Description = "This is updated description.";
            update.ExamplesArray = new[] { "This is updated example array." };
            update.Source = "This is updated source.";
            update.Primary = "This is updated primary.";
            update.Foreign = "This is updated foreign.";
            update.Subject = "This is updated subject.";

            //Act
            var output = await _service.UpdateAsync(update).ConfigureAwait(true);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.Equal(200, output.Output);

            var entity = _connector.Context.Sentences.SingleOrDefault(item => item.ID == 1);
            Assert.NotNull(entity);
            Assert.Equal("This is updated description.", update.Description);
            Assert.Equal("This is updated example array.", update.Examples);
            Assert.Equal("This is updated source.", update.Source);
            Assert.Equal("This is updated primary.", update.Primary);
            Assert.Equal("This is updated subject.", update.Subject);
        }

        [Fact]
        public async void Update_InputIsNull_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.UpdateAsync(null).ConfigureAwait(true);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Update_InputHasNullPrimaryProperty_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.UpdateAsync(_nullPrimaryEntry).ConfigureAwait(true);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Update_InputHasEmptyPrimaryProperty_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.UpdateAsync(_emptyPrimaryEntry).ConfigureAwait(true);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Update_InputHasWhiteSpacePrimaryProperty_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.UpdateAsync(_whiteSpacePrimaryEntry).ConfigureAwait(true);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Update_InputHasNullForeignProperty_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.UpdateAsync(_nullForeignEntry).ConfigureAwait(true);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Update_InputHasEmptyForeignProperty_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.UpdateAsync(_emptyForeignEntry).ConfigureAwait(true);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Update_InputHasWhiteSpaceForeignProperty_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.UpdateAsync(_whiteSpaceForeignEntry).ConfigureAwait(true);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Update_InputHasNullSubjectProperty_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.UpdateAsync(_nullSubjectEntry).ConfigureAwait(true);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Update_InputHasEmptySubjectProperty_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.UpdateAsync(_emptySubjectEntry).ConfigureAwait(true);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Update_InputHasWhiteSpaceSubjectProperty_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.UpdateAsync(_whiteSpaceSubjectEntry).ConfigureAwait(true);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Update_EntryDoesntExists_ReturnsResult404WithException()
        {
            //Arrange
            var noExistingEntry = new Sentence
            {
                ID = 1000,
                Primary = "Test",
                Foreign = "Test",
                Subject = "Test"
            };
            var noExistingEntry2 = new Sentence
            {
                ID = -2,
                Primary = "Test",
                Foreign = "Test",
                Subject = "Test"
            };

            //Act
            Thread.Sleep(100);//Inaczej wali błędem
            var output = await _service.UpdateAsync(noExistingEntry).ConfigureAwait(true);
            var output2 = await _service.UpdateAsync(noExistingEntry2).ConfigureAwait(true);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(404, output.Output);
            Assert.False(output2.IsFine);
            Assert.NotNull(output2.Exception);
            Assert.Equal(404, output2.Output);
        }

        [Fact]
        public async void Update_ServerError_ReturnsResult500WithException()
        {
            //Arrange
            var existingEntry = _connector.Context.Sentences.FirstOrDefault(item => item.ID == 1);

            //Act
            var output = await _errorService.UpdateAsync(existingEntry).ConfigureAwait(false);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(500, output.Output);
        }

        [Fact]
        public async void Delete_RemovedEntryFromDatabase_ReturnsResult200()
        {
            //Act
            var output = await _service.DeleteAsync(1).ConfigureAwait(false);

            //Assert
            Assert.True(output.IsFine);
            Assert.Null(output.Exception);
            Assert.Equal(200, output.Output);
        }

        [Fact]
        public async void Delete_IdIsNegative_ReturnsResult422WithException()
        {
            //Act
            var output = await _service.DeleteAsync(-1).ConfigureAwait(false);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(422, output.Output);
        }

        [Fact]
        public async void Delete_EntryDoesntExists_ReturnsResult404WithException()
        {
            //Act
            var output = await _service.DeleteAsync(500).ConfigureAwait(false);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(404, output.Output);
        }

        [Fact]
        public async void Delete_ServerError_ReturnsResult500WithException()
        {
            //Act
            var output = await _errorService.DeleteAsync(1).ConfigureAwait(false);

            //Assert
            Assert.False(output.IsFine);
            Assert.NotNull(output.Exception);
            Assert.Equal(500, output.Output);
        }
    }
}
