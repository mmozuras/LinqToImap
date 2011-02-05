namespace LinqToGmail.Imap.Commands
{
    using System;

    /// <summary>
    /// Closes the connection.
    /// </summary>
    public sealed class Logout : BaseCommand
    {
        public Logout()
        {
            Text = "LOGOUT";
        }

        public override string Text { get; protected set; }
    }
}