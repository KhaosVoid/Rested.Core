namespace Rested.Core.CQRS.Data
{
    public sealed class DuplicateProjectionMappingException : Exception
    {
        #region Members

        private const string EXCEPTION_MESSAGE = "The projection type '{0}' already has a mapping registered for: '{1}' => '{2}'";

        #endregion Members

        #region Ctor

        public DuplicateProjectionMappingException(Type projectionType, string projectionPropertyPath, string documentPropertyPath) :
            base(message: string.Format(EXCEPTION_MESSAGE, projectionType, projectionPropertyPath, documentPropertyPath))
        {

        }

        #endregion Ctor
    }
}
