using System.Xml.Linq;
using CoreTechs.Common;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly Regex _rgx = new Regex(@"(?<note>\d+)\|(?<pattern>.+?)\|", RegexOptions.Compiled | RegexOptions.Multiline);
        private decimal _humPct;

        public Form1()
        {
            InitializeComponent();
            SetupDrumsMenu();
            SetInitialPattern();

            Closing += (s, e) => StopTheMusic();

            Load += OnLoad;
            SetHumanizationPct();
        }

        private void SetupDrumsMenu()
        {
            var drums = from el in XElement.Parse(Resources.drums).Elements("drum")
                let note = el.Attribute("note").Value.ConvertTo<int>()
                let name = el.Attribute("name").Value
                select new {note, name};

            var drumsMenu = (ToolStripMenuItem)contextMenuStrip1.Items.Add("Drums");

            foreach (var drum in drums)
            {
                var drumItem = (ToolStripMenuItem)drumsMenu.DropDownItems.Add(string.Format("{1}: {0}", drum.name,drum.note));
                drumItem.Click += (sender, args) =>
                {
                    txtPatterns.AppendText(Environment.NewLine);
                    txtPatterns.AppendText(string.Format("{1}|*| {0}", drum.name, drum.note));
                };
            }
        }

        private void SetInitialPattern()
        {
            var ptn = new[]
            {
                Resources.ShoveIt,
                Resources.Fonkay,
            }
            .Select(XElement.Parse)
            .Select(x => new
            {
                text = x.Value.NormalizeLineEndings(),
                frameLen = x.Attribute("frameLength").Value.Parse(decimal.Parse)
            })
            .RandomElement();

            txtPatterns.Text = ptn.text;
            numFrameLen.Value = ptn.frameLen;
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
            StopTheMusic();

            Thread.Sleep(500);

            _cts = new CancellationTokenSource();
            var deviceId = GetOutputDevices().Select((d, i) => i).Single(i => i == cboDevices.SelectedIndex);
            _player = Task.Run(() => PlayThatFunkyMusic(deviceId));
        }

        private void StopTheMusic()
        {
            if (_cts == null) return;
            using (_cts)
            {
                _cts.Cancel();

                using (_player)
                    _player.Wait();
            }

            _cts = null;
        }

        

        
        private void PlayThatFunkyMusic(int deviceId)
        {
            var txt = txtPatterns.Text.ReadLines().Where(x => !x.Trim().StartsWith("#")).Join(Environment.NewLine);

            // txt contains the textbox text without commented lines (lines starting with '#')

            var tracks = _rgx.Matches(txt)
                .Cast<Match>()
                .Select(m => new
                {
                    Note = int.Parse(m.Groups["note"].Value),
                    Pattern = m.Groups["pattern"].Value.Trim('\r', '\n'),
                })
                .Select(x => new Track(x.Note, x.Pattern,true))
                .ToArray();

            if (tracks.Length == 0)
                return;

            var frames = Merge(tracks);

            using (var od = new OutputDevice(deviceId))
            {
                var sw = Stopwatch.StartNew();
                foreach (var frame in frames.TakeWhile(_ => !_cts.IsCancellationRequested))
                {
                    sw.Restart();
                    var hitTasks = frame
                        .Where(hit => hit != null)
                        .Select(hit =>
                            GetHumanDelay()
                                .ContinueWith(at =>
                                    od.PlayAsync((int) numChannel.Value, hit.Note, hit.Velocity)
                                ))
                        .ToArray();

                    Task.WaitAll(hitTasks);
                    var millisecondsTimeout = (int)numFrameLen.Value - (int)sw.Elapsed.TotalMilliseconds;
                    if (millisecondsTimeout > 0)
                        Thread.Sleep(millisecondsTimeout);
                }
            }
        }

        Task GetHumanDelay()
        {
            if (_humPct == 0)
                return Task.FromResult(0);

            var maxDelay = (int) Math.Round((numFrameLen.Value/1/*conrols how much the drummer can screw up*/)*_humPct, 0);
            var delay = RNG.Next(maxDelay);
            return Task.Delay(delay);
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
            if (_cts != null)
                RestartTheMusic();
        }

        private void trkHumanize_Scroll(object sender, EventArgs e)
        {
            SetHumanizationPct();
        }

        private void SetHumanizationPct()
        {
            _humPct = trkHumanize.Value/100m;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            RestartTheMusic();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopTheMusic();
        }
    }
}
