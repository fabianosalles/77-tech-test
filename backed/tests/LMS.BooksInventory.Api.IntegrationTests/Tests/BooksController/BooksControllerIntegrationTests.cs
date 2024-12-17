using System.Net;

namespace LMS.BookInventory.Api.IntegrationTests.Tests.BooksController;

public class BooksControllerIntegrationTests
{
    //TODO: this could be put on a config file to get rid of hardcoded string
    private const string BooksApiUrl = "http://localhost:5001";

    [Fact]
    public async Task GetBooks_WithNoIssues_ShouldReturn200()
    {
        //Arrange
        var client = new HttpClient();
        client.BaseAddress = new Uri(BooksApiUrl);
        var url = $"api/v1/books?offset=0&limit=20";

        //Act
        var response = await client.GetAsync(url);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
