using Microsoft.AspNetCore.Mvc;

namespace Rested.Core.Http
{
    [ModelBinder(BinderType = typeof(IfMatchByteArrayModelBinder))]
    public class IfMatchByteArray
    {
        #region Properties

        public byte[] Tag { get; }

        #endregion Properties

        #region Ctor

        public IfMatchByteArray(byte[] tag)
        {
            Tag = tag;
        }

        #endregion Ctor
    }
}
