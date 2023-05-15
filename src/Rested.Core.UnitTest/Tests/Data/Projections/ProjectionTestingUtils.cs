using Rested.Core.CQRS.Data;

namespace Rested.Core.UnitTest.Tests.Data.Projections
{
    public static class ProjectionTestingUtils
    {
        #region Members

        private static ProjectionRegistration _projectionRegistration;

        #endregion Members

        #region Methods

        public static void InitilizeProjectionRegistration()
        {
            if (_projectionRegistration is null)
                _projectionRegistration = new ProjectionRegistration();
        }

        #endregion Methods
    }
}
