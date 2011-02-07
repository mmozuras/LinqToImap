namespace LinqToImap.Imap.Parsing
{
    using System.Net.Mail;

    internal static class QuotedPrintableDecoder
    {
        internal static string Decode(string quotedPrintableString)
        {
            return Attachment.CreateAttachmentFromString(string.Empty, quotedPrintableString).Name;
        }
    }
}