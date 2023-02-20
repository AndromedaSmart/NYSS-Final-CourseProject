using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Course.Tests")]
namespace Course.Tools
{
    public class Cipher
    {
        private readonly int Power;
        private readonly Dictionary<char, int> AlphabetCharToInt = new Dictionary<char, int>();
        private readonly Dictionary<int, char> AlphabetIntToChar = new Dictionary<int, char>();

        public Cipher(char[] alphabet)
        {
            Debug.Assert(alphabet.Length > 0);

            Power = alphabet.Length;
            int i = 0;
            foreach (var c in alphabet)
            {
                AlphabetCharToInt[c] = i;
                AlphabetIntToChar[i] = c;
                i++;
            }
        }

        public string Encrypt(string input, string keyword)
        {
            Debug.Assert(input != null && keyword != null);

            var text = input.ToLower();
            keyword = keyword.ToLower();

            int len = keyword.Length;
            var result = new StringBuilder(text.Length);
            for (int i = 0; i < text.Length; i++)
            {
                if (!AlphabetCharToInt.ContainsKey(text[i]))
                {
                    result.Append(text[i]);
                    text = text.Remove(i, 1);
                    input = input.Remove(i, 1);
                    --i;
                    continue;
                }


                if (char.IsUpper(input[i]))
                {
                    result.Append(
                    char.ToUpper(AlphabetIntToChar
                    [
                        (AlphabetCharToInt[text[i]] + AlphabetCharToInt[keyword[i % len]]) % Power
                    ]
                    ));
                }
                else
                {
                    result.Append(
                    AlphabetIntToChar
                    [
                        (AlphabetCharToInt[text[i]] + AlphabetCharToInt[keyword[i % len]]) % Power
                    ]
                    );
                }
            }

            return result.ToString();
        }

        public string Decrypt(string input, string keyword)
        {
            Debug.Assert(input != null && keyword != null);

            var text = input.ToLower();
            keyword = keyword.ToLower();

            int len = keyword.Length;
            var result = new StringBuilder(input.Length);
            for (int i = 0; i < text.Length; i++)
            {
                if (!AlphabetCharToInt.ContainsKey(text[i]))
                {
                    result.Append(text[i]);
                    text = text.Remove(i, 1);
                    input = input.Remove(i, 1);
                    --i;
                    continue;
                }

                if (char.IsUpper(input[i]))
                {
                    result.Append(
                    char.ToUpper(AlphabetIntToChar
                    [
                        (AlphabetCharToInt[text[i]] - AlphabetCharToInt[keyword[i % len]] + Power) % Power
                    ]
                    ));
                }
                else
                {
                    result.Append(
                    AlphabetIntToChar
                    [
                        (AlphabetCharToInt[text[i]] - AlphabetCharToInt[keyword[i % len]] + Power) % Power
                    ]
                    );
                }
            }

            return result.ToString();
        }
    }
}