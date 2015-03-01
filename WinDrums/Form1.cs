using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
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
        private readonly IDisposable _changeSubscription;
        private readonly Regex _rgx = new Regex(@"(?<note>\d+)\|(?<pattern>.+?)\|", RegexOptions.Compiled | RegexOptions.Multiline);

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

            Load += OnLoad;
            InitializeComponent();

            _changeSubscription = Observable.FromEventPattern(
                h => txtPatterns.TextChanged += h,
                h => txtPatterns.TextChanged -= h)
                .Throttle(TimeSpan.FromSeconds(.75))
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(_ => RestartTheMusic());
        }

        private void OnLoad(object sender, EventArgs eventArgs)
        {
            cboDevices.Items.AddRange(GetOutputDevices().Select(x => x.name).ToArray());
            cboDevices.SelectedIndex = 0;
        }

        static IEnumerable<MidiOutCaps> GetOutputDevices()
        {
            return Enumerable.Range(0, OutputDeviceBase.DeviceCount).Select(OutputDeviceBase.GetDeviceCapabilities);
        }

        private void RestartTheMusic()
        {
            if (_cts != null)
                using (_cts)
                {
                    _cts.Cancel();

                    using (_player)
                        _player.Wait();
                }

            Thread.Sleep(500);

            _cts = new CancellationTokenSource();
            var deviceId = GetOutputDevices().Select((d, i) => i).Single(i => i == cboDevices.SelectedIndex);
            _player = Task.Run(() => PlayThatFunkyMusic(deviceId));
        }

        private void PlayThatFunkyMusic(int deviceId)
        {
            // get all lines that have the correct format
            var tracks = _rgx.Matches(txtPatterns.Text)
                .Cast<Match>()
                .Select(m => new
                {
                    Note = int.Parse(m.Groups["note"].Value),
                    Pattern = m.Groups["pattern"].Value.Trim('\r', '\n'),
                })
                .Select(x => new Track(x.Note, x.Pattern))
                .ToArray();

            if (tracks.Length == 0)
                return;

            var frames = Merge(tracks);

            using (var od = new OutputDevice(deviceId))
            {
                foreach (var frame in frames.TakeWhile(_ => !_cts.IsCancellationRequested))
                {
                    foreach (var hit in frame)
                    {
                        if (hit == null)
                            continue;

                        od.PlayAsync((int) numChannel.Value, hit.Note, hit.Velocity);
                    }

                    Thread.Sleep((int)numFrameLen.Value);
                }
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

        private void cboDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            RestartTheMusic();
        }

    }
}
