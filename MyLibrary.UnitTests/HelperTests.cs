namespace MyLibrary.Tests;
public class HelperTests
{
    [Fact]
    public void ToCamelCase_ShouldConvertToCamelCase()
    {
        // Arrange
        var input = "HelloWorld";
        var expected = "helloWorld";

        // Act
        var result = input.ToCamelCase();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToPascalCase_ShouldConvertToPascalCase()
    {
        // Arrange
        var input = "helloWorld";
        var expected = "HelloWorld";

        // Act
        var result = input.ToPascalCase();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToSnakeCase_ShouldConvertToSnakeCase()
    {
        // Arrange
        var input = "Hello World";
        var expected = "hello_world";

        // Act
        var result = input.ToSnakeCase();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void RandomUniqueWords_WordsIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<string> words = null;
        int count = 5;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => StringHelper.RandomUniqueWords(words, count));
    }

    [Fact]
    public void ContainsString_ShouldReturnTrueIfStringExists()
    {
        // Arrange
        var searchString = "apple";
        var stringArray = new[] { "apple", "banana", "cherry" };

        // Act
        var result = stringArray.ContainsString(searchString);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void RandomWords_ReturnsCorrectNumberOfWords()
    {
        // Arrange
        var words = new[] { "apple", "banana", "cherry", "date", "elderberry" };
        int count = 3;

        // Act
        var result = StringHelper.RandomUniqueWords(words, count);

        // Assert
        Assert.Equal(count, result.Count());
    }

    [Fact]
    public void RandomWords_ReturnsUniqueWords()
    {
        // Arrange
        var words = new[] { "banana", "banana", "banana", "date", "elderberry" };
        var expected = words.RemoveDuplicateWords().Count();
        int count = 3;

        // Act
        var result = StringHelper.RandomUniqueWords(words, count);

        // Assert
        Assert.Equal(expected, result.Count());
    }

    [Fact]
    public void RandomWords_ReturnsAllWordsIfCountIsGreaterThanUniqueArrayLength()
    {
        // Arrange
        var words = new[] { "banana", "banana", "cherry", "date", "elderberry" };
        var expected = words.RemoveDuplicateWords().Count();
        int count = 5;

        // Act
        var result = StringHelper.RandomUniqueWords(words, count);

        // Assert
        Assert.Equal(expected, result.Count());
        Assert.All(words, word => Assert.Contains(word, result));
    }
}
