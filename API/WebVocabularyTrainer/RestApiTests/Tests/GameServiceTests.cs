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

            _validSettings = new Settings();
        }

        [Fact]
        public void GetSettingsForm_ReturnsSettingsForm()
        {
            //Arrange
            var subjectsCount = _connector.Context.Sentences
                .Select(x => x.Subject)
                .Distinct()
                .Count();
            var sourcesCount = _connector.Context.Sentences
                .Select(x => x.Source)
                .Distinct()
                .Count();

            //Act
            var output = _service.GetSettingsForm();

            //Assert
            Assert.True(output != null);
            Assert.Equal(_validSettings.Mode, output.Mode);
            Assert.Equal(_validSettings.PhrasesUpperLimit, output.PhrasesUpperLimit);
            Assert.Equal(_validSettings.Repeats, output.Repeats);
            Assert.True(output.Sources != null);
            Assert.True(output.Sources.Count == sourcesCount);
            Assert.True(output.Subjects != null);
            Assert.True(output.Subjects.Count == sourcesCount);
        }
    }
}
