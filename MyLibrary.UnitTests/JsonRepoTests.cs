using Microsoft.Extensions.Logging;
using Moq;

namespace MyLibrary.Data.Tests;

public class JsonRepoTests : IDisposable
{
    private readonly JsonRepo _jsonRepo;
    private readonly Mock<ILogger<JsonRepo>> _loggerMock;
    private readonly string TestFileName = "IntegrationTestData.json";

    public JsonRepoTests()
    {
        _loggerMock = new Mock<ILogger<JsonRepo>>();
        _jsonRepo = new JsonRepo(_loggerMock.Object, TestFileName);
        CleanData();
    }

    private void CleanData()
    {
        File.WriteAllText(TestFileName, string.Empty);
    }

    public void Dispose()
    {
        CleanData();
    }

    [Fact]
    public void Add_ShouldAddNewItem()
    {
        // Arrange
        var key = Guid.NewGuid().ToString();
        List<string> value = ["newValue"];

        // Act
        _jsonRepo.Add(key, value);

        // Assert
        Assert.Equal(value, _jsonRepo.Get(key));
    }

    [Fact]
    public void Add_ShouldThrowException_WhenKeyIsNull()
    {
        // Arrange
        string key = null!;
        List<string> value = ["newValue"];

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _jsonRepo.Add(key, value));
    }

    [Fact]
    public void Add_ShouldThrowException_WhenValueIsNull()
    {
        // Arrange
        var key = "newKey";
        List<string> value = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _jsonRepo.Add(key, value));
    }

    [Fact]
    public void Add_ShouldThrowException_WhenValueIsEmptyList()
    {
        // Arrange
        var key = "newKey";
        List<string> value = new List<string>();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _jsonRepo.Add(key, value));
    }

    [Fact]
    public void Add_ShouldThrowException_WhenKeyAlreadyExists()
    {
        // Arrange
        var key = Guid.NewGuid().ToString();
        List<string> value = ["existingValue"];

        _jsonRepo.Add(key, value);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _jsonRepo.Add(key, ["newValue"]));
    }

    [Fact]
    public void Update_ShouldUpdateExistingItem()
    {
        // Arrange
        var key = Guid.NewGuid().ToString();
        List<string> value = ["existingValue"];
        _jsonRepo.Add(key, value);
        List<string> newValue = ["newValue"];

        // Act
        _jsonRepo.Update(key, newValue);

        // Assert
        Assert.Equal(newValue, _jsonRepo.Get(key));
    }

    [Fact]
    public void Update_ShouldThrowException_WhenKeyDoesNotExist()
    {
        // Arrange
        var key = "nonExistingKey";
        List<string> value = ["newValue"];

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _jsonRepo.Update(key, value));
    }

    [Fact]
    public void Delete_ShouldRemoveItem()
    {
        // Arrange
        var key = Guid.NewGuid().ToString();
        List<string> value = ["existingValue"];
        _jsonRepo.Add(key, value);

        // Act
        _jsonRepo.Delete(key);

        // Assert
        Assert.Throws<ArgumentException>(() => _jsonRepo.Get(key).ToString());
    }

    [Fact]
    public void Delete_ShouldThrowException_WhenKeyDoesNotExist()
    {
        // Arrange
        var key = "nonExistingKey";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _jsonRepo.Delete(key));
    }

    [Fact]
    public void Get_ShouldReturnItem()
    {
        // Arrange
        var key = Guid.NewGuid().ToString();
        List<string> value = ["existingValue"];
        _jsonRepo.Add(key, value);

        // Act
        var result = _jsonRepo.Get(key);

        // Assert
        Assert.Equal(value, result);
    }

    [Fact]
    public void Get_ShouldThrowException_WhenKeyDoesNotExist()
    {
        // Arrange
        var key = "nonExistingKey";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _jsonRepo.Get(key));
    }
}
