using System.Buffers;

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
            return stringArray.Contains(searchItem);
        }

        public static int WordCount(this string str)
        {
            return str.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length;
        }

        /// <summary>
        /// Returns a random set of unique words from the input list of words.
        /// </summary>
        /// <param name="words">When words is null</param>
        /// <param name="numberOfWords"></param>
        /// <exception cref="ArgumentNullException">words is null</exception>"
        /// <returns></returns>
        public static IEnumerable<string> RandomUniqueWords(IEnumerable<string> words, int numberOfWords)
        {
            var uniqueWords = words.RemoveDuplicateWords();
            var maxCount = uniqueWords.Count();

            if (numberOfWords > maxCount)
            {
                return uniqueWords;
            }

            var temp = new string[numberOfWords];
            var rand = new Random();

            for (var i = 0; i < numberOfWords; i++)
            {
                var randomIndex = rand.Next(maxCount);
                temp[i] = words.ElementAt(randomIndex);
            }
            return temp;
        }

        public static IEnumerable<string> RemoveDuplicateWords(this IEnumerable<string> words)
        {
            if (!words.Any())
            {
                return words;
            }
            return words.Select(x => x).Distinct();
        }
    }
}
