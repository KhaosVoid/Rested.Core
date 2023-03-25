using System.Linq.Expressions;

namespace Rested.Core.CQRS.Data
{
    public static class ProjectionMappings
    {
        #region Properties

        private static Dictionary<Type, List<ProjectionMapping>> _registeredMappings;

        #endregion Properties

        #region Ctor

        static ProjectionMappings()
        {
            _registeredMappings = new Dictionary<Type, List<ProjectionMapping>>();
        }

        #endregion Ctor

        #region Methods

        private static bool ContainsMapping<TProjection>(string projectionPropertyPath) =>
            ContainsMapping(typeof(TProjection), projectionPropertyPath);

        private static bool ContainsMapping(Type type, string projectionPropertyPath)
        {
            if (!_registeredMappings.ContainsKey(type))
                throw new ProjectionMappingNotRegisteredException(type);

            var projectionTypeMappings = _registeredMappings[type];
            var projectionMapping = projectionTypeMappings
                .FirstOrDefault(map => map.ProjectionPropertyPath == projectionPropertyPath);

            if (projectionMapping is null)
                return false;

            return true;
        }

        public static void Register<TProjection, TDocument, TProjectionValue, TDocumentValue>(
            Expression<Func<TProjection, TProjectionValue>> projectionPropertySelector,
            Expression<Func<TDocument, TDocumentValue>> documentPropertySelector)
        {
            if (!_registeredMappings.ContainsKey(typeof(TProjection)))
                _registeredMappings.Add(typeof(TProjection), new List<ProjectionMapping>());

            var projectionTypeMappings = _registeredMappings[typeof(TProjection)];

            var projectionMapping = new ProjectionMapping(typeof(TProjection), projectionPropertySelector, documentPropertySelector);

            if (ContainsMapping<TProjection>(projectionMapping.ProjectionPropertyPath))
                throw new DuplicateProjectionMappingException(
                    projectionType: typeof(TProjection),
                    projectionPropertyPath: projectionMapping.ProjectionPropertyPath,
                    documentPropertyPath: projectionMapping.DocumentPropertyPath);

            projectionTypeMappings.Add(projectionMapping);
        }

        public static bool TryGetMapping<TProjection>(string projectionPropertyPath, out ProjectionMapping projectionMapping, bool isCamelCase = false) =>
            TryGetMapping(typeof(TProjection), projectionPropertyPath, out projectionMapping, isCamelCase);

        public static bool TryGetMapping(Type projectionType, string projectionPropertyPath, out ProjectionMapping projectionMapping, bool isCamelCase = false)
        {
            if (!_registeredMappings.ContainsKey(projectionType))
                throw new ProjectionMappingNotRegisteredException(projectionType, projectionPropertyPath);

            var projectionTypeMappings = _registeredMappings[projectionType];
            projectionMapping = projectionTypeMappings
                .FirstOrDefault(map =>
                {
                    if (isCamelCase)
                        return map.ProjectionPropertyPath.ToCamelCase() == projectionPropertyPath;

                    return map.ProjectionPropertyPath == projectionPropertyPath;
                });

            if (projectionMapping is null)
                return false;

            return true;
        }

        public static IEnumerable<ProjectionMapping> GetProjectionMappings<TProjection>() =>
            GetProjectionMappingsForType(typeof(TProjection));

        public static IEnumerable<ProjectionMapping> GetProjectionMappingsForType(Type type)
        {
            if (!_registeredMappings.ContainsKey(type))
                throw new ProjectionMappingNotRegisteredException(type);

            return _registeredMappings[type];
        }

        #endregion Methods
    }
}
