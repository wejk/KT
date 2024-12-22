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
    public void ContainsString_ShouldReturnTrueIfStringExists()
    {
        // Arrange
        var searchString = "apple";
        var stringArray = new[] { "apple", "banana", "cherry" };
        var span = new Span<string>(stringArray);
        // Act
        var result = span.ContainsString(searchString);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void RemoveDuplicate_RemovesDuplicatesCorrectly()
    {
        // Arrange
        var input = new Span<string>(["apple", "banana", "apple", "orange", "banana"]);
        var expected = new Span<string>(["apple", "banana", "orange"]);

        // Act
        var result = StringHelper.RemoveDuplicate(input);

        // Assert
        Assert.Equal(expected.ToArray(), result.ToArray());
    }

    [Fact]
    public void RemoveDuplicate_EmptyInput_ReturnsEmpty()
    {
        // Arrange
        var input = new Span<string>(Array.Empty<string>());
        var expected = new Span<string>(Array.Empty<string>());

        // Act
        var result = StringHelper.RemoveDuplicate(input);

        // Assert
        Assert.Equal(expected.ToArray(), result.ToArray());
    }

    [Fact]
    public void RemoveDuplicate_NoDuplicates_ReturnsSameInput()
    {
        // Arrange
        var input = new Span<string>(new string[] { "apple", "banana", "orange" });
        var expected = new Span<string>(new string[] { "apple", "banana", "orange" });

        // Act
        var result = StringHelper.RemoveDuplicate(input);

        // Assert
        Assert.Equal(expected.ToArray(), result.ToArray());
    }

    [Fact]
    public void RandomWords_ShouldReturnSpecifiedNumberOfWords()
    {
        // Arrange
        var words = new string[] { "apple", "banana", "cherry", "date", "elderberry" }.AsSpan();
        int numberOfWords = 3;

        // Act
        var result = words.RandomWords(numberOfWords);

        // Assert
        Assert.Equal(numberOfWords, result.Length);
    }

    [Fact]
    public void RandomWords_ShouldReturnAllWordsIfNumberOfWordsIsGreaterThanOrEqualToLength()
    {
        // Arrange
        var words = new string[] { "apple", "banana", "cherry" }.AsSpan();
        int numberOfWords = 5;

        // Act
        var result = words.RandomWords(numberOfWords);

        // Assert
        Assert.Equal(words.Length, result.Length);
    }

    [Fact]
    public void RandomWords_ShouldThrowArgumentException_WhenInputIsEmpty()
    {
        // Arrange, Act and Assert
        Assert.Throws<ArgumentNullException>(() => StringHelper.RandomWords(new Span<string>(), 3));
    }

    [Fact]
    public void RandomWords_ShouldThrowArgumentException_WhenNumberOfWordsIsLessThanOrEqualToZero()
    {
        // Arrange, Act and Assert
        Assert.Throws<ArgumentException>(() => StringHelper.RandomWords(new Span<string>(["one", "two", "three"]), 0));
    }

    [Fact]
    public void RandomWords_ShouldReturnUniqueWords_WhenNumberOfWordsIsGreaterThanOrEqualToMaxCount()
    {
        Span<string> words = ["one", "two", "three"];
        var result = words.RandomWords(3);
        Assert.Equal(words.ToArray(), result.ToArray());
    }

    [Fact]
    public void RandomWords_ShouldReturnRandomWords_WhenNumberOfWordsIsLessThanMaxCount()
    {
        Span<string> words = ["one", "two", "three", "four", "five"];
        var result = words.RandomWords(3);
        Assert.Equal(3, result.Length);
    }

    [Fact]
    public void RandomWords_ShouldReturnUniqueRandomWords_WhenNumberOfWordsIsLessThanMaxCount()
    {
        Span<string> words = ["one", "one", "three", "three", "five"];
        var result = words.RandomWords(2);
        Assert.Equal(2, result.Length);
        Assert.True(result.Slice(0) != result.Slice(1));
    }
}
