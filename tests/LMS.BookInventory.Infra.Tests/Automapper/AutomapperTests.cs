using LMS.BookInventory.Infra.UnitTests.Helpers;

namespace LMS.BookInventory.Infra.UnitTests.Automapper;

public class AutomapperTests
{
    [Fact]
    public void Mapper_ConfigurationIsValid()
    {
        // Arrange & Act
        var mapper = AutomapperLoader.LoadInfrastructure();

        // Assert
        // This validates all properties on models defined in a mapping profile are mapped 
        // or otherwise accounted for
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}
