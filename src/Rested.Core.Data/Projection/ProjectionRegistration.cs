using System.Reflection;
using System.Runtime.CompilerServices;

namespace Rested.Core.Data.Projection
{
    public class ProjectionRegistration
    {
        #region Properties

        public static ProjectionRegistration Instance { get; private set; }

        #endregion Properties

        #region Ctor

        private ProjectionRegistration()
        {
            InvokeStaticConstructorOnProjections(
                assemblies: AppDomain.CurrentDomain.GetAssemblies());
        }

        private ProjectionRegistration(Assembly assembly)
        {
            InvokeStaticConstructorOnProjections(
                assemblies: new Assembly[] { assembly });
        }

        private ProjectionRegistration(Assembly[] assemblies)
        {
            InvokeStaticConstructorOnProjections(assemblies);
        }

        #endregion Ctor

        #region Methods

        public static ProjectionRegistration Initialize()
        {
            Instance ??= new ProjectionRegistration();

            return Instance;
        }

        public static ProjectionRegistration Initialize(Assembly assembly)
        {
            Instance ??= new ProjectionRegistration(assembly);

            return Instance;
        }

        public static ProjectionRegistration Initialize(Assembly[] assemblies)
        {
            Instance ??= new ProjectionRegistration(assemblies);

            return Instance;
        }

        private void InvokeStaticConstructorOnProjections(Assembly[] assemblies)
        {
            var projectionTypes = GetDerivedProjectionTypes(assemblies);

            projectionTypes.ForEach(t => RuntimeHelpers.RunClassConstructor(t.TypeHandle));
        }

        private List<Type> GetDerivedProjectionTypes(Assembly[] assemblies)
        {
            return assemblies
                .SelectMany(a => a.GetExportedTypes())
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
