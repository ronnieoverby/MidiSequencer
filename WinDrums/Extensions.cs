using System;
using System.IO;
using System.Threading.Tasks;
using CoreTechs.Common;
using Sanford.Multimedia.Midi;

namespace WinDrums
{
    static class Extensions
    {
        async public static Task PlayAsync(this OutputDeviceBase od, int channel, int data1, int data2 = 127, int noteDuration = 250)
        {
            od.Send(new ChannelMessage(ChannelCommand.NoteOn, channel, data1, data2));
            await Task.Delay(noteDuration);
            od.Send(new ChannelMessage(ChannelCommand.NoteOff, channel, data1));
        }

        public static string NormalizeLineEndings(this string s)
        {
            using (var reader = new StringReader(s))
                return reader.ReadLines().Join(Environment.NewLine).Trim();
        }
    }
}