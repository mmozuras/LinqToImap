namespace LinqToImap.Imap.Commands
{
    public sealed class Logout : Command
    {
        public Logout()
        {
            Text = "LOGOUT";
        }

        public override string Text { get; protected set; }
    }
}