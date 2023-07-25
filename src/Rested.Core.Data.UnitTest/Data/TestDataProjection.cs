using Rested.Core.Data;

namespace Rested.Core.Data.UnitTest.Data
{
    public class TestDataProjection : Projection
    {
        #region Properties

        public Guid Id { get; set; }
        public string? Property1 { get; set; }
        public string? Property2 { get; set; }
        public string? Property3 { get; set; }

        #endregion Properties

        #region Ctor

        static TestDataProjection()
        {
            RegisterMapping((TestDataProjection p) => p.Id, (IDocument<TestData> d) => d.Id);
        }

        #endregion Ctor
    }
}
