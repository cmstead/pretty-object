namespace PrettyObject
{
    public static class OutputHelpers
    {
        public static string ToStringOrNull<T>(this T value)
        {
            return value == null ? "null" : value.ToString();
        }

        public static string ToNullSafeString<T>(this T value)
        {
            return value == null ? null : value.ToString();
        }
    }
}
