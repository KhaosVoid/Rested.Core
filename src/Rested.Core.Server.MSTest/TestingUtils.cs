using Rested.Core.Data;

namespace Rested.Core.Server.MSTest
{
    public static class TestingUtils
    {
        public static string GenerateNameFromData<T>(int number = 1) where T : IData
        {
            if (number < 1)
                number = 1;

            return $"{typeof(T).Name}-{number:000}";
        }

        public static string GenerateDescriptionFromData<T>(int number = 1) where T : IData
        {
            if (number < 1)
                number = 1;

            return $"Test {typeof(T).Name} {number:000}";
        }
    }
}
