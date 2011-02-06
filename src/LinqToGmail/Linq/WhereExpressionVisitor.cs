namespace LinqToGmail.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Remotion.Data.Linq.Parsing;

    public class WhereExpressionVisitor : ThrowingExpressionTreeVisitor
    {
        private string name;
        public IDictionary<string, string> SearchParameters { get; private set; }

        protected override Exception CreateUnhandledItemException<T>(T unhandledItem, string visitMethod)
        {
            return new ParserException(visitMethod + " method is not implemented");
        }

        protected override Expression VisitMethodCallExpression(MethodCallExpression expression)
        {
            if (expression.Method.Name == "Contains")
            {
                VisitExpression(expression.Object);

                var value = expression.Arguments.First().ToString().Replace("\"", string.Empty);
                SearchParameters = new Dictionary<string, string> {{name, value}};
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