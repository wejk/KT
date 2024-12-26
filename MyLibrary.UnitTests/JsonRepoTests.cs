namespace MyLibrary.Data.Tests;
using Microsoft.Extensions.Logging;
using Moq;

public class JsonRepoTests
{
    private readonly JsonRepo _jsonRepo;
    private readonly Mock<ILogger<JsonRepo>> _loggerMock;

    public JsonRepoTests()
    {
        _loggerMock = new Mock<ILogger<JsonRepo>>();
        _jsonRepo = new JsonRepo(_loggerMock.Object);
    }

    [Fact]
    public void Add_ShouldAddNewItem()
    {
        // Arrange
        var key = "newKey";
        var value = "newValue";

        // Act
        _jsonRepo.Add(key, value);

        // Assert
        Assert.Equal(value, _jsonRepo.Get(key));
    }

    [Fact]
    public void Add_ShouldThrowException_WhenKeyIsNull()
    {
        // Arrange
        string key = null;
        var value = "newValue";

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _jsonRepo.Add(key, value));
    }

    [Fact]
    public void Add_ShouldThrowException_WhenValueIsNull()
    {
        // Arrange
        var key = "newKey";
        object value = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _jsonRepo.Add(key, value));
    }

    [Fact]
    public void Add_ShouldThrowException_WhenKeyAlreadyExists()
    {
        // Arrange
        var key = "existingKey";
        var value = "existingValue";
        _jsonRepo.Add(key, value);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _jsonRepo.Add(key, "newValue"));
    }

    [Fact]
    public void Update_ShouldUpdateExistingItem()
    {
        // Arrange
        var key = "existingKey";
        var value = "existingValue";
        _jsonRepo.Add(key, value);
        var newValue = "newValue";

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
        var value = "newValue";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _jsonRepo.Update(key, value));
    }

    [Fact]
    public void Delete_ShouldRemoveItem()
    {
        // Arrange
        var key = "existingKey";
        var value = "existingValue";
        _jsonRepo.Add(key, value);

        // Act
        _jsonRepo.Delete(key);

        // Assert
        Assert.Throws<ArgumentException>(() => _jsonRepo.Get(key));
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
        var key = "existingKey";
        var value = "existingValue";
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
