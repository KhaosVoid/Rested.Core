using System.Reflection;
using System.Runtime.CompilerServices;

namespace Rested.Core.Data
{
    public class ProjectionRegistration
    {
        #region Ctor

        public ProjectionRegistration()
        {
            InitializeStaticConstructorsOnProjections(
                assemblies: AppDomain.CurrentDomain.GetAssemblies());
        }

        public ProjectionRegistration(Assembly assembly)
        {
            InitializeStaticConstructorsOnProjections(
                assemblies: new Assembly[] { assembly });
        }

        public ProjectionRegistration(Assembly[] assemblies)
        {
            InitializeStaticConstructorsOnProjections(assemblies);
        }

        #endregion Ctor

        #region Methods

        private void InitializeStaticConstructorsOnProjections(Assembly[] assemblies)
        {
            var projectionTypes = GetDerivedProjections(assemblies);

            projectionTypes.ForEach(t => RuntimeHelpers.RunClassConstructor(t.TypeHandle));
        }

        private List<Type> GetDerivedProjections(Assembly[] assemblies)
        {
            return assemblies
                .SelectMany(da => da.GetExportedTypes())
                .Where(t => IsBaseTypeProjection(t) && !t.IsAbstract)
                .ToList();
        }

        private bool IsBaseTypeProjection(Type type)
        {
            if (type.BaseType is null)
                return false;

            if (type.BaseType == typeof(Projection))
                return true;

            else
                return IsBaseTypeProjection(type.BaseType);
        }

        #endregion Methods
    }
}
