namespace Rested.Core.Data
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BsonCollectionAttribute : Attribute
    {
        #region Properties

        public string CollectionName { get; }

        #endregion Properties

        #region Ctor

        public BsonCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }

        #endregion Ctor
    }
}
