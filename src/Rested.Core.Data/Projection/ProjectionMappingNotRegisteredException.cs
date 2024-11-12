namespace Rested.Core.Data.Projection
{
    public sealed class ProjectionMappingNotRegisteredException : Exception
    {
        #region Members

        private const string EXCEPTION_MESSAGE_PROPERTY_PATH = "The projection type '{0}' does not have a mapping registered for the property '{1}'.";
        private const string EXCEPTION_MESSAGE_PROJECTION_TYPE = "There are no mappings registered for the type '{0}'.";

        #endregion Members

        #region Ctor

        public ProjectionMappingNotRegisteredException(Type projectionType, string projectionPropertyPath = null) :
            base(message: projectionPropertyPath is null ?
                string.Format(EXCEPTION_MESSAGE_PROJECTION_TYPE, projectionType) :
                string.Format(EXCEPTION_MESSAGE_PROPERTY_PATH, projectionType, projectionPropertyPath))
        {

        }

        #endregion Ctor
    }
}
