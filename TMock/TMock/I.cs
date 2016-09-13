
namespace TMock
{
    public static class I
    {
        /// <summary>
        /// Returns any value for the mentioned type.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>T</returns>
        public static T Any<T>()
        {
            return default(T);
        }
    }
}
