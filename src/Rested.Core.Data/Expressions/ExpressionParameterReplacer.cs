using System.Linq.Expressions;

namespace Rested.Core.Data.Expressions
{
    internal class ExpressionParameterReplacer : ExpressionVisitor
    {
        #region Members

        private readonly ParameterExpression _parameterExpression;

        #endregion Members

        #region Ctor

        internal ExpressionParameterReplacer(ParameterExpression parameterExpression)
        {
            _parameterExpression = parameterExpression;
        }

        #endregion Ctor

        #region Methods

        internal static Expression Replace(ParameterExpression parameterExpression, Expression expression) =>
            new ExpressionParameterReplacer(parameterExpression).Visit(expression);

        protected override Expression VisitParameter(ParameterExpression node) => base.VisitParameter(_parameterExpression);

        #endregion Methods
    }
}
