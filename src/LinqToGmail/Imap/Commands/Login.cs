namespace LinqToGmail.Imap.Commands
{
    using Utils;

    public sealed class Login : Command
    {
        public Login(string username, string password)
        {
            Ensure.IsNotNullOrWhiteSpace(username, "username");
            Ensure.IsNotNullOrWhiteSpace(password, "password");

            Text = string.Format("LOGIN {0} {1}", username, password);
        }

        public override string Text { get; protected set; }
    }
}