using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinDrums
{
    public partial class Form1 : Form
    {
        Task _player;
        CancellationTokenSource _cts;
        private IDisposable _changeSubscription;

        public Form1()
        {
            Closing += (s, e) =>
            {
                using (_changeSubscription)
                {
                    if (_cts != null)
                        _cts.Cancel();
                }

            };
            InitializeComponent();

            _changeSubscription = Observable.FromEventPattern(
                h => txtPatterns.TextChanged += h,
                h => txtPatterns.TextChanged -= h)
                .Throttle(TimeSpan.FromSeconds(1))
                .Subscribe(_ => TextChanged());
        }

        private void TextChanged()
        {
            if (_cts != null)
            using(_cts)
            {             
                _cts.Cancel();
                _player.Wait();               
            }

            _cts = new CancellationTokenSource();
            _player = Task.Run(() => PlayThatFunkyMusic());
        }

        private void PlayThatFunkyMusic()
        {
            // get all lines that have the correct format
            var matches = _rgx.Matches(txtPatterns.Text)
                .Cast<Match>()
                .Select(m => new
                {
                    Note = int.Parse(m.Groups["note"].Value),
                    Pattern = m.Groups["pattern"].Value.Trim('\r','\n'),
                })
                .ToArray();

            if (matches.Length == 0)            
                return;

            var patterns = matches.Select(m =>
                MakePattern(
                        Generate(() => Tuple.Create(m.Note, 127)),
                        m.Pattern)).ToArray();

            var frames = Merge(patterns);

            using (var od = new OutputDevice(0))
            {
                var frameNumber = 0;
                foreach (var frame in frames.TakeWhile(_ => !_cts.IsCancellationRequested))
                {
                    foreach (var hit in frame)
                    {
                        if (hit == null)
                            continue;

                        od.PlayAsync((int)numChannel.Value, hit.Item1, hit.Item2);
                    }

                    Thread.Sleep((int)numFrameLen.Value);
                }
            }
        }

         IEnumerable<T> Generate<T>(Func<T> generator)
        {
            while (true)
                yield return generator();
        }

        private  IEnumerable<T> MakePattern<T>(IEnumerable<T> sequence, string pattern, int res = 1)
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

         IEnumerable<T[]> Merge<T>(params IEnumerable<T>[] sequences)
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

        Regex _rgx = new Regex(@"^(?<note>\d+):(?<pattern>.+)$", RegexOptions.Compiled|RegexOptions.Multiline);
     
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
