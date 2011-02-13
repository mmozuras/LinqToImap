namespace LinqToImap.Imap.Commands
{
    public sealed class Logout : Command
    {
        public Logout()
        {
            Text = "LOGOUT";
        }

        protected override string Text { get; set; }
    }
}