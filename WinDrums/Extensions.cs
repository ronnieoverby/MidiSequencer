using System.Threading.Tasks;
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
    }
}