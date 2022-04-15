using Challenge.Domain.Core.Enums;
using Challenge.Services.Dtos.Commands;
using Challenge.UnitTests.Constants;
using System.Collections.Generic;
using Xunit;

namespace Challenge.UnitTests.Validations
{
    public class CreateCensusCommandValidationsTests
    {
        [Trait(TraitsConstants.TraitService, nameof(CreateCensusCommandValidationsTests))]
        [Fact(DisplayName = "Create Census - Success")]
        public void CreateCensus_Success()
        {
            // Arrange
            var command = new CreateCensusCommand("firstName", "lastName", "color", new ParentsCommand("father", "mother"), new List<SonCommand>
            {
                new SonCommand("name",10),
                new SonCommand("Name",15)
            }, "schooling", Regions.CentroOeste);

            // Act
            var validate = new CreateCensusCommandValidations().Validate(command);

            // Asserts
            Assert.True(validate.IsValid);
        }

        [Trait(TraitsConstants.TraitService, nameof(CreateCensusCommandValidationsTests))]
        [Fact(DisplayName = "Create Census - No Parents - Fail")]
        public void CreateCensus_NoParents_Fail()
        {
            // Arrange
            var command = new CreateCensusCommand("firstName", "lastName", "color", new ParentsCommand("", ""), new List<SonCommand>
            {
                new SonCommand("name",10),
                new SonCommand("Name",15)
            }, "schooling", Regions.CentroOeste);

            // Act
            var validate = new CreateCensusCommandValidations().Validate(command);

            // Asserts
            Assert.False(validate.IsValid);
        }
    }
}
