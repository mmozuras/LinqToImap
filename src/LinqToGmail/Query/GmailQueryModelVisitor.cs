namespace LinqToGmail.Query
{
    using Imap;
    using Imap.Commands;
    using System;
    using System.Collections.Generic;
    using Remotion.Data.Linq;
    using Remotion.Data.Linq.Clauses;
    using Remotion.Data.Linq.Clauses.ResultOperators;

    internal class GmailQueryModelVisitor : QueryModelVisitorBase
    {
        public IEnumerable<MailboxMessage> Results { get; private set; }
        private readonly Mailbox mailbox;
        private readonly ImapSslClient imapSslClient;

        public GmailQueryModelVisitor(Mailbox mailbox)
        {
            this.mailbox = mailbox;

            //TODO: Figure this out
            imapSslClient = ImapSslClient.Current;
        }

        public override void VisitResultOperator(ResultOperatorBase resultOperator, QueryModel queryModel, int index)
        {
            if (resultOperator is TakeResultOperator)
            {
                var take = resultOperator as TakeResultOperator;
                var count = int.Parse(take.Count.ToString());

                var command = new Fetch(mailbox.MessagesCount - count, mailbox.MessagesCount);
                Results = imapSslClient.Execute<IEnumerable<MailboxMessage>>(command);
            }
            else if (resultOperator is AverageResultOperator)
                throw new NotSupportedException("LinqToExcel does not provide support for the Average() method");
            else if (resultOperator is CountResultOperator)
                throw new NotSupportedException("LinqToExcel does not provide support for the Count() method");
            else if (resultOperator is LongCountResultOperator)
                throw new NotSupportedException("LinqToExcel does not provide support for the LongCount() method");
            else if (resultOperator is FirstResultOperator)
                throw new NotSupportedException("LinqToExcel does not provide support for the First() method");
            else if (resultOperator is MaxResultOperator)
                throw new NotSupportedException("LinqToExcel does not provide support for the Max() method");
            else if (resultOperator is MinResultOperator)
                throw new NotSupportedException("LinqToExcel does not provide support for the Min() method");
            else if (resultOperator is SumResultOperator)
                throw new NotSupportedException("LinqToExcel does not provide support for the Sum() method");
            else if (resultOperator is ContainsResultOperator)
                throw new NotSupportedException("LinqToExcel does not provide support for the Contains() method");
            else if (resultOperator is DefaultIfEmptyResultOperator)
                throw new NotSupportedException("LinqToExcel does not provide support for the DefaultIfEmpty() method");
            else if (resultOperator is DistinctResultOperator)
                throw new NotSupportedException("LinqToExcel does not provide support for the Distinct() method");
            else if (resultOperator is ExceptResultOperator)
                throw new NotSupportedException("LinqToExcel does not provide support for the Except() method");
            else if (resultOperator is GroupResultOperator)
                throw new NotSupportedException("LinqToExcel does not provide support for the Group() method");
            else if (resultOperator is IntersectResultOperator)
                throw new NotSupportedException("LinqToExcel does not provide support for the Intersect() method");
            else if (resultOperator is OfTypeResultOperator)
                throw new NotSupportedException("LinqToExcel does not provide support for the OfType() method");
            else if (resultOperator is SingleResultOperator)
                throw new NotSupportedException("LinqToExcel does not provide support for the Single() method. Use the First() method instead");
            else if (resultOperator is UnionResultOperator)
                throw new NotSupportedException("LinqToExcel does not provide support for the Union() method");

            base.VisitResultOperator(resultOperator, queryModel, index);
        }
    }
}