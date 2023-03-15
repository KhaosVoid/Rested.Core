using System.Linq.Expressions;

namespace Rested.Core.Expressions
{
    internal class ExpressionParameterReplacer : ExpressionVisitor
    {
        #region Members

        private readonly ParameterExpression _parameter;

        #endregion Members

        #region Ctor

        internal ExpressionParameterReplacer(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

        #endregion Ctor

        #region Methods

        internal static Expression Replace(ParameterExpression parameterExpression, Expression expression) =>
            new ExpressionParameterReplacer(parameterExpression).Visit(expression);

        protected override Expression VisitParameter(ParameterExpression node) => base.VisitParameter(_parameter);

        #endregion Methods
    }
}
