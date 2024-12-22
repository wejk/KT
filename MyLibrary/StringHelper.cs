namespace MyLibrary
{
    public static class StringHelper
    {
        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            return char.ToLower(str[0]) + str.Substring(1);
        }

        public static string ToPascalCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        public static string ToSnakeCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            return string.Join("_", str.Split(' ')).ToLower();
        }

        public static bool ContainsString(this Span<string> inputString, string searchItem)
        {
            return inputString.Contains(searchItem);
        }

        public static int WordCount(this string str)
        {
            return str.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static Span<string> RandomWords(this Span<string> words, int numberOfWords)
        {
            if (words.IsEmpty)
            {
                throw new ArgumentNullException("Input cannot be empty");
            }

            if (numberOfWords <= 0)
            {
                throw new ArgumentException("Number of words should be greater than 0");
            }

            var uniqueWords = words.RemoveDuplicate();
            var maxCount = uniqueWords.Length;

            if (numberOfWords >= maxCount)
            {
                return uniqueWords;
            }

            var rand = new Random();
            var temp = new string[numberOfWords];
            for (var i = 0; i < numberOfWords; i++)
            {
                var start = rand.Next(0, maxCount);
                temp[i] = uniqueWords[start];
            }

            return new Span<string>(temp);
        }

        public static Span<string> RemoveDuplicate(this Span<string> input)
        {
            var hashSet = new HashSet<string>();
            var index = 0;

            foreach (var item in input)
            {
                if (hashSet.Add(item))
                {
                    input[index++] = item;
                }
            }

            return input.Slice(0, index);
        }

        public static bool ContainsUpperCaseChar(this string word)
        {
            return word.Any(char.IsUpper);
        }

        public static bool ContainsLowerCaseChar(this string word)
        {
            return word.Any(char.IsLower);
        }

        public static bool ContainsDigit(this string word)
        {
            return word.Any(char.IsDigit);
        }

        public static bool ContainsOnlyDigit(this string word)
        {
            return word.Any(char.IsDigit);
        }

        public static bool ContainsSpecialChar(this string word)
        {
            return word.Any(ch => !char.IsLetterOrDigit(ch));
        }

        public static bool ContainsWhiteSpace(this string word)
        {
            return word.Any(char.IsWhiteSpace);
        }

        public static bool ContainsPunctuation(this string word)
        {
            return word.Any(char.IsPunctuation);
        }

        public static bool ContainsSymbol(this string word)
        {
            return word.Any(char.IsSymbol);
        }

        public static bool ContainsOnlySymbol(this string word)
        {
            return word.Any(char.IsSymbol);
        }

        public static bool ContainsLetter(this string word)
        {
            return word.Any(char.IsLetter);
        }

        public static bool ContainsOnlyLetter(this string word)
        {
            return word.All(char.IsLetter);
        }
    }
}
