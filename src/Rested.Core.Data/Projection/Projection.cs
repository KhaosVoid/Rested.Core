﻿using System.Linq.Expressions;
using System.Reflection;
using Rested.Core.Data.Expressions;

namespace Rested.Core.Data.Projection;

public abstract class Projection
{
    #region Ctor

    public Projection()
    {

    }

    #endregion Ctor

    #region Methods

    protected static void RegisterMapping<TProjection, TDocument, TProjectionValue, TDocumentValue>(
        Expression<Func<TProjection, TProjectionValue>> projectionPropertySelector,
        Expression<Func<TDocument, TDocumentValue>> documentPropertySelector)
        where TDocumentValue : TProjectionValue
    {
        ProjectionMappings.Register(projectionPropertySelector, documentPropertySelector);
    }

    public static Expression<Func<TDocument, TProjection>> GetProjectionExpression<TProjection, TDocument>()
    {
        var parameterExpression = Expression.Parameter(typeof(TDocument));
        var projectionMappings = ProjectionMappings.GetProjectionMappings<TProjection>();
        var memberBindings = new List<MemberBinding>();

        foreach (var projectionMapping in projectionMappings)
        {
            var projectionPropertyLambdaExpression = (LambdaExpression)projectionMapping.ProjectionPropertySelector;
            var documentPropertyLambdaExpression = (LambdaExpression)projectionMapping.DocumentPropertySelector;
            var memberAssignment = Expression.Bind(
                member: (PropertyInfo)((MemberExpression)projectionPropertyLambdaExpression.Body).Member,
                expression: ExpressionParameterReplacer.Replace(parameterExpression, documentPropertyLambdaExpression.Body));

            memberBindings.Add(memberAssignment);
        }

        var expressionLambda = Expression.Lambda<Func<TDocument, TProjection>>(
            body: Expression.MemberInit(
                newExpression: Expression.New(typeof(TProjection)),
                bindings: memberBindings),
            parameters: new[] { parameterExpression });

        return expressionLambda;
    }

    #endregion Methods
}