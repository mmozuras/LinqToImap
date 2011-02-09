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

        protected override Expression VisitBinaryExpression(BinaryExpression expression)
        {
            VisitExpression(expression.Left);

            switch (expression.NodeType)
            {
                case ExpressionType.AndAlso:
                    Query += " ";
                    break;
                default:
                    throw new NotSupportedException(string.Format("{0} statement is not supported", expression.NodeType));
            }

            VisitExpression(expression.Right);

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
            Query += expression.Member.Name;
            return expression;
        }
    }
}