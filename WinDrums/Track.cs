using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using CoreTechs.Common;

namespace WinDrums
{
    class Track : IEnumerable<MidiNote>
    {
        private readonly int _note;
        private readonly string _pattern;
        private readonly bool _playForever;

        public Track(int note, string pattern, bool playForever)
        {
            _note = note;
            _pattern = pattern;
            _playForever = playForever;
        }

        static int ParseVelocity(char c)
        {
            const int max = 127;

            int factor;
            if (int.TryParse(c.ToString(CultureInfo.InvariantCulture), out factor))
            {
                var low = (max / 10.0) * factor;
                var hi = (max / 10.0) * (factor + 1);
                return RNG.Next((int) Math.Round(low, 0), (int) Math.Round(hi, 0));
            }

            switch (c)
            {
                case '_':
                    return ParseVelocity('2');
                case '.':
                    return ParseVelocity('3');
                case ',':
                    return ParseVelocity('4');
                case '-':
                    return ParseVelocity('5');
                case '~':
                    return ParseVelocity('6');
                case '+':
                    return ParseVelocity('7');
                case '*':
                    return ParseVelocity('8');
                case '`':
                    return ParseVelocity('9');
                case '!':
                    return max;
                default:
                    return 100;
            }
        }

        public IEnumerator<MidiNote> GetEnumerator()
        {
            if (string.IsNullOrWhiteSpace(_pattern))
                yield break;

            do
            {
                foreach (var c in _pattern)
                {
                    if (char.IsWhiteSpace(c))
                    {
                        yield return null;
                        continue;
                    }

                    var velocity = ParseVelocity(c);
                    yield return new MidiNote(_note, velocity);
                }
            } while (_playForever);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}