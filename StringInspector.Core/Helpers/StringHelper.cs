using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StringInspector.Core.Models;

namespace StringInspector.Core.Helpers
{
    public static class StringHelper
    {
        private static StringHelperOptions defaultOptions = new StringHelperOptions() 
        {
            IgnoreSpaces = false
        };

        public static ArticleInfo CalculateMostCommonCharacter(Article input, StringHelperOptions options = null)
        {
            if (input == null) throw new ArgumentNullException("Article cannot be null.");
            if (input.Content == null) throw new ArgumentNullException("Article.Content cannot be null.");
            if (options == null) options = defaultOptions;

            var chars = new Dictionary<char, int>();
            var output = new ArticleInfo();
            output.Article = input;

            foreach (var c in input.Content)
            {
                if (options.IgnoreSpaces && c == ' ') continue;
                if (!chars.ContainsKey(c)) chars[c] = 0;

                chars[c]++;
            }

            foreach (var kvp in chars)
            {
                if (output.MostCommonCharacterCount < kvp.Value)
                {
                    output.MostCommonCharacter = kvp.Key;
                    output.MostCommonCharacterCount = kvp.Value;
                }
            }

            return output;
        }
    }
}
