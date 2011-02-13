namespace LinqToImap.Imap.Commands
{
    using System;

    public abstract class Command<T> : Command
    {
    }

    public abstract class Command : IEquatable<Command>
    {
        protected abstract string Text { get; set; }

        public bool Equals(Command other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ToString() == other.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is Command)) return false;
            return Equals((Command) obj);
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }

        public static bool operator ==(Command left, Command right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Command left, Command right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return Text;
        }
    }
}