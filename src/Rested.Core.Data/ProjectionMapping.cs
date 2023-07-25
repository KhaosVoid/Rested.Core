using System.Linq.Expressions;

namespace Rested.Core.Data
{
    public sealed class ProjectionMapping
    {
        #region Properties

        public Type ProjectionType { get; }
        public Expression ProjectionPropertySelector { get; }
        public string ProjectionPropertyPath { get; }
        public Expression DocumentPropertySelector { get; }
        public string DocumentPropertyPath { get; }

        #endregion Properties

        #region Ctor

        internal ProjectionMapping(Type projectionType, Expression projectionPropertySelector, Expression documentPropertySelector)
        {
            ProjectionType = projectionType;
            ProjectionPropertySelector = projectionPropertySelector;
            ProjectionPropertyPath = ExpressionToPropertyPath(projectionPropertySelector);
            DocumentPropertySelector = documentPropertySelector;
            DocumentPropertyPath = ExpressionToPropertyPath(documentPropertySelector);
        }

        #endregion Ctor

        #region Methods

        public static string ExpressionToPropertyPath(Expression expression)
        {
            return string.Join(".", expression.ToString().Split('.').Skip(1));
        }

        #endregion Methods
    }
}
