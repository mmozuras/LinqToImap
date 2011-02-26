# LinqToImap

*On February 17th (I noticed it on February 26th), Alexander Wieser revealed [Crystalbyte Equinox](http://www.codeproject.com/KB/library/equinox.aspx), which, among other features, has Linq to Imap functionality. Because of that, I might or might not continue work on LingToImap.*

LinqToImap is not finished, but you can already write queries like this: 

`(from message in gmail.Inbox`  
`where message.Subject.Contains("subject") && message.Flags.Seen`  
`select message).Take(20)`  

