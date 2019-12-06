using RestApi.Data.Models;
using RestApiTests.Factories;
using RestApiTests.Mocks;
using RestApiTests.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RestApiTests.Tests
{
    public class GameServiceTests
    {
        private MockEFConnector _connector;
        private GameServiceLayer _service;
        private Settings _validSettings;
       

        public GameServiceTests()
        {
            _connector = EFConnectorFactory.Instance();
            _service = new GameServiceLayer(_connector);

            _validSettings = new Settings
            {
                Mode = Mode.Random,
                Repeats = 3,
                PhrasesUpperLimit = 10,

            };
        }

        [Fact]
        public async void GetSettingsForm_ReturnsResultWithDefaultSettings()
        {
            //Arrange
            //Both collections has to cantains additional record called "All"
            var subjectsCount = _connector.Context.Sentences
                .Select(x => x.Subject)
                .Distinct()
                .Count() + 1;
            var sourcesCount = _connector.Context.Sentences
                .Select(x => x.Source)
                .Distinct()
                .Count() + 1;

            //Act
            var output = await _service.GetSettingsForm().ConfigureAwait(false);

            //Assert
            Assert.NotNull(output);
            Assert.True(output.IsFine);
            Assert.NotNull(output.Output);
            Assert.Null(output.Exception);

            Assert.Equal(_validSettings.Mode, output.Output.Mode);
            Assert.Equal(_validSettings.PhrasesUpperLimit, output.Output.PhrasesUpperLimit);
            Assert.Equal(_validSettings.Repeats, output.Output.Repeats);
            Assert.NotNull(output.Output.Sources);
            Assert.Equal(output.Output.Sources.Count, sourcesCount);
            Assert.Contains("All", output.Output.Sources);
            Assert.NotNull(output.Output.Subjects);
            Assert.Equal(output.Output.Subjects.Count, subjectsCount);
            Assert.Contains("All", output.Output.Subjects);
        }

        public async void GetGame_ReceivesValidSettings_ReturnsResultWithGame()
        {

        }
    }
}
