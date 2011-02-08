namespace LinqToImap.Linq
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Remotion.Data.Linq.Parsing;

    public class WhereExpressionVisitor : ThrowingExpressionTreeVisitor
    {
        public string Query { get; private set; }

        public WhereExpressionVisitor()
        {
            Query = string.Empty;
        }

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
                Query += " " + value;
            }
            return expression;
        }

        protected override Expression VisitUnaryExpression(UnaryExpression expression)
        {
            if (expression.NodeType == ExpressionType.Not)
            {
                Query += "Not ";
                return VisitMemberExpression(expression.Operand as MemberExpression);
            }
            throw new NotSupportedException();
        }

        protected override Expression VisitMemberExpression(MemberExpression expression)
        {
            var supportedKeywords = new[] {"Seen", "Deleted", "Draft", "Answered", "Flagged", "Recent", "Subject"};

            var name = expression.Member.Name;

            if (!supportedKeywords.Contains(name))
            {
                throw new NotSupportedException();
            }

            Query += name;
            return expression;
        }
    }
}