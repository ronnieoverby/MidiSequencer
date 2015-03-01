namespace WinDrums
{
    internal class MidiNote
    {
        public int Note { get; private set; }
        public int Velocity { get; private set; }

        public MidiNote(int note, int velocity)
        {
            Note = note;
            Velocity = velocity;
        }
    }
}