namespace LinqToGmail.Query
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Imap.Commands;
    using Remotion.Data.Linq.Parsing;

    public class WhereClauseExpressionTreeVisitor : ThrowingExpressionTreeVisitor
    {
        private string name;
        public ICommand Command { get; private set; }

        protected override Exception CreateUnhandledItemException<T>(T unhandledItem, string visitMethod)
        {
            return new NotImplementedException(visitMethod + " method is not implemented");
        }

        protected override Expression VisitMethodCallExpression(MethodCallExpression expression)
        {
            if (expression.Method.Name == "Contains")
            {
                VisitExpression(expression.Object);

                var value = expression.Arguments.First().ToString().Replace("\"", string.Empty);
                Command = new Search(new Dictionary<string, string> {{name, value}});
            }
            return expression;
        }

        protected override Expression VisitMemberExpression(MemberExpression expression)
        {
            name = expression.Member.Name;
            return expression;
        }
    }
}