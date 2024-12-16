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

    [Fact]
    public void MapDomainBook_ToDbBook_Succeed()
    {
        //arrange
        var mapper = AutomapperLoader.LoadInfrastructure();
        var domainBook = new Domain.Entities.Book()
        {
            Id = Guid.NewGuid(),
            Isbn = "0000000000000",
            Author = "Some Author",
            Name = "Some Name",
            Description = "Description",
            Edition = 2,
            Publisher = "Some pblishser"
        };

        //act
        var dbBook = mapper.Map<Database.Models.Book>(domainBook);

        //assert
        Assert.Equal(domainBook.Id, dbBook.Id);
        Assert.Equal(domainBook.Isbn, dbBook.Isbn);
        Assert.Equal(domainBook.Author, dbBook.Author);
        Assert.Equal(domainBook.Edition, dbBook.Edition);
        Assert.Equal(domainBook.Publisher, dbBook.Publisher);
    }
}
