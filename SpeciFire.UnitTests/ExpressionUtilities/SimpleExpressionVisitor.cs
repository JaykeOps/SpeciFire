using System.Collections.Generic;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests.ExpressionUtilities
{
    internal class SimpleExpressionVisitor : ExpressionVisitor
    {
        public Dictionary<Expression, Expression> Map = new Dictionary<Expression, Expression>();

        protected override Expression VisitParameter(ParameterExpression node)
            => Map.TryGetValue(node, out Expression result) ? result : node;
    }
}