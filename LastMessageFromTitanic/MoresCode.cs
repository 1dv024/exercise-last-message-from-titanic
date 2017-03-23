using System;
using System.Collections.Generic;
using System.Threading;

namespace LastMessageFromTitanic
{
    /// <summary>
    /// Specifies whether applicable MorseCode object will play sounds and/or write to the console.
    /// </summary>
    [Flags]
    internal enum MorseCodeOptions { Sound = 0x01, Verbally = 0x02 };

    /// <summary>
    /// Represents a Morse code converter.
    /// </summary>
    internal class MorseCode
    {
        /// <summary>
        /// The duration of a dot length in milliseconds.
        /// </summary>
        public const int DotLength = 75;

        /// <summary>
        /// The duration, in milliseconds, of Morse code elements.
        /// </summary>
        public enum ElementLenght : int
        {
            Di             = 1 * DotLength,
            Da             = 3 * DotLength,
            IntraCharacter = 1 * DotLength,
            InterCharacter = 3 * DotLength,
            InterWord      = 7 * DotLength
        };

        /// <summary>
        /// Maps characters to Morse codes.
        /// </summary>
        public static readonly IReadOnlyDictionary<char, string> Codes = 
            new Dictionary<char, string>
        {
            { 'A', ".-      " }, { 'B', "-...    " }, { 'C', "-.-.    " },
            { 'D', "-..     " }, { 'E', ".       " }, { 'F', "..-.    " },
            { 'G', "--.     " }, { 'H', "....    " }, { 'I', "..      " },
            { 'J', ".---    " }, { 'K', "-.-     " }, { 'L', ".-..    " },
            { 'M', "--      " }, { 'N', "-.      " }, { 'O', "---     " },
            { 'P', ".--.    " }, { 'Q', "--.-    " }, { 'R', ".-.     " },
            { 'S', "...     " }, { 'T', "-       " }, { 'U', "..-     " },
            { 'V', "...-    " }, { 'W', ".--     " }, { 'X', "-..-    " },
            { 'Y', "-.--    " }, { 'Z', "--..    " }, { 'Å', ".--.-   " },
            { 'Ä', ".-.-    " }, { 'Ö', "---.    " }, { 'É', "..-..   " },
            { 'Ñ', "--.--   " }, { 'Ü', "..--    " }, { 'Û', "..--    " },
            { 'Š', "----    " }, { 'ß', "...---.." }, { 'Ç', "-.-..   " },
            { 'Ŝ', "-.-..   " }, { '1', ".----   " }, { '2', "..---   " },
            { '3', "...--   " }, { '4', "....-   " }, { '5', ".....   " },
            { '6', "-....   " }, { '7', "--...   " }, { '8', "---..   " },
            { '9', "----.   " }, { '0', "-----   " }, { '?', "..--..  " },
            { '!', "..--.   " }, { ',', "--..--  " }, { '.', ".-.-.-  " },
            { '=', "-...-   " }, { '-', "-....-  " }, { '(', "-.--.   " },
            { ')', "-.--.-  " }, { '~', "........" }, { '+', ".-.-.   " },
            { '@', ".--.-.  " }, { '/', "-..-.   " }, { '%', ".--..   " },
            { '"',".-..-.   " }, { ';', "-.-.-.  " }, { ':', "---...  " },
            { '¿', "..-.-   " }, { '\'',".----.  " }, { '#', ".-..-   " },
            { '&', ".-...   " }, { '$', ".-...   " }, { '§', ".-...   " },
            { '*', ".. ..   " }, { '¡', "--...-  " }
        };

        /// <summary>
        /// Initializes a new instance of the MorseCode class.
        /// </summary>
        /// <param name="morseCodeOptions"></param>
        public MorseCode(MorseCodeOptions morseCodeOptions = 
            MorseCodeOptions.Sound | MorseCodeOptions.Verbally)
        {
            MorseCodeOptions = morseCodeOptions;
        }

        /// <summary>
        /// MorseCodeOptions.Sound to play sound; and/or MorseCodeOptions.Verbally to write to the console.
        /// </summary>
        public MorseCodeOptions MorseCodeOptions { get; set; }

        /// <summary>
        /// Converts a character to its equivalent Morse code.
        /// </summary>
        /// <param name="ch">A character to convert.</param>
        /// <returns>A string that contains a Morse code of dots and dashes; null if no Morse code is found.</returns>
        public string ConvertCharacter(char ch)
        {
            string value;
            Codes.TryGetValue(ch, out value);

            return value?.Trim();
        }

        /// <summary>
        /// Converts a message into Morse code, character by character.
        /// </summary>
        /// <param name="message">String to convert.</param>
        public void Play(string message)
        {
            // Split on any (multiple) whitespace character.
            var words = message.ToUpper()
                .Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < words.Length; i++)
            {
                var characters = words[i].ToCharArray();
                for (var j = 0; j < characters.Length; j++)
                {
                    var code = ConvertCharacter(characters[j]);
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        PlayCode(code);

                        // Inter-character spacing; skip spacing after the last character.
                        if (j < characters.Length - 1)
                        {
                            if (MorseCodeOptions.HasFlag(MorseCodeOptions.Verbally))
                            {
                                Console.Write(" ");
                            }
                            Thread.Sleep((int) ElementLenght.InterCharacter);
                        }
                    }
                }

                // Inter-word spacing; skip spacing after the last word.
                if (i < words.Length - 1)
                {
                    if (MorseCodeOptions.HasFlag(MorseCodeOptions.Verbally))
                    {
                        Console.Write(" ");
                    }
                    Thread.Sleep((int) ElementLenght.InterWord);
                }
            }

            Thread.Sleep((int)ElementLenght.InterWord);
        }

        /// <summary>
        /// Plays a Morse code.
        /// </summary>
        /// <param name="code">A string containing dots and dashes describing a Morse code.</param>
        private void PlayCode(string code)
        {
            code = code.Trim();
            for (var i = 0; i < code.Length; ++i)
            {
                // Dit or dah element?
                var isDit = code[i] == '.';
                var duration = (int)(isDit ? ElementLenght.Di : ElementLenght.Da);

                // View the morse code element.
                if (MorseCodeOptions.HasFlag(MorseCodeOptions.Verbally))
                {
                    var value = string.Empty;

                    if (i > 0)
                    {
                        value += '-';
                    }

                    if (i < code.Length - 1)
                    {
                        value += isDit ? "di" : "da";
                    }
                    else
                    {
                        value += isDit ? "dit" : "dah";
                    }

                    Console.Write(value);
                }

                // Play the morse code element, or just wait.
                if (MorseCodeOptions.HasFlag(MorseCodeOptions.Sound))
                {
                    Console.Beep(1000, duration);
                }
                else
                {
                    Thread.Sleep(duration);
                }

                // Intra-character spacing; skip spacing after the last element.
                if (i < code.Length - 1)
                {
                    Thread.Sleep((int)ElementLenght.IntraCharacter);
                }
            }
        }
    }
}
