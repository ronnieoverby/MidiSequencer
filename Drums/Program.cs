using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Drums
{
    class Program
    {
        static void Main(string[] args)
        {
            var rng = new Random();

            var kick = Generate(() => Tuple.Create(35, rng.Next(110,127)));
            var snare = Generate(() => Tuple.Create(38, rng.Next(110,127)));
            var closedHat = Generate(() => Tuple.Create(42, rng.Next(110,127)));
            var openHat = Generate(() => Tuple.Create(46, rng.Next(110,127)));
            var crash = Generate(() => Tuple.Create(49, rng.Next(110,127)));
            var cow = Generate(() => Tuple.Create(56, rng.Next(110,127)));

            var kicks = MakePattern(kick, "----", 1);
            var snares = MakePattern(snare, "   -", 2);
            var closedHats = MakePattern(closedHat, "-", 2);
            var openHats = MakePattern(openHat, "", 4);
            var crashes = MakePattern(crash, "", 32);
            var cows = MakePattern(cow, "");

            var frames = Merge(kicks, snares, cows, crashes, closedHats, openHats);

            using (var od = new OutputDevice(0))
            {
                var frameNumber = 0;
                foreach (var frame in frames)
                {
                    frameNumber++;
                    Console.WriteLine("FRAME #{0}", frameNumber);
                    foreach (var hit in frame)
                    {
                        if (hit == null)
                            continue;

                        Console.WriteLine("Note: {0,-4}", hit.Item1);
                        od.PlayAsync(9, hit.Item1, hit.Item2);
                    }

                    Thread.Sleep(50);
                    Console.WriteLine(new string('_', 80));
                }              
            }
        }

        private static IEnumerable<T> MakePattern<T>(IEnumerable<T> sequence, string pattern, int res = 1)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                yield break; 

            var iterator = sequence.GetEnumerator();
            while (true)
            {
                foreach (var c in pattern)
                {
                    if (!iterator.MoveNext())
                        yield break;

                    if (char.IsWhiteSpace(c))
                        yield return default(T);
                    else
                        yield return iterator.Current;
                }

                for (int i = 0; i < res - 1; i++)
                    yield return default(T);                
            }
        }

        static IEnumerable<T[]> Merge<T>(params IEnumerable<T>[] sequences)
        {
            var iterators = sequences.Select(x => x.GetEnumerator()).ToArray();

            while (true)
            {
                var alive = iterators.Where(it => it.MoveNext()).ToArray();

                if (!alive.Any())
                    yield break;

                yield return alive.Select(x => x.Current).ToArray();
            }
        }

        static IEnumerable<T> Generate<T>(Func<T> generator)
        {
            while (true)
                yield return generator();
        }
    }

    static class Extensions
    {
        async public static Task PlayAsync(this OutputDeviceBase od, int channel, int data1, int data2 = 127, int noteDuration = 100)
        {
            od.Send(new ChannelMessage(ChannelCommand.NoteOn, channel, data1, data2));
            await Task.Delay(noteDuration);
            od.Send(new ChannelMessage(ChannelCommand.NoteOff, channel, data1));
        }
    }
}
