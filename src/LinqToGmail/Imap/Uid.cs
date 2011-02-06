namespace LinqToGmail.Imap
{
    public struct Uid
    {
        public int Value { get; private set; }

        public Uid(int value) : this()
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}