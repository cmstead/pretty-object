using System;

namespace PrettyObject
{
    public static class PrettyPrinter
    {
        public static string PrettyPrint<T>(this T objectToPrint)
        {
            Func<string, object, string> transformer = 
                (_, value) => value.ToNullSafeString();

            return PrettyPrint(objectToPrint, transformer);
        }

        public static string PrettyPrint<T>(
            this T objectToPrint,
            Func<string, object, object> transformer)
        {
            Func<string, object, string> newTransformer = (fieldName, value)
                => transformer(fieldName, value).ToNullSafeString();

            return PrettyPrint(objectToPrint, newTransformer);
        }

        public static string PrettyPrint<T>(
            this T objectToPrint,
            Func<string, object, string> transformer)
        {
            Func<string, object, string> readAndTransform = (fieldName, fieldValue)
                => ValueTransforms.ReadAndTransformOutputValue(fieldName, fieldValue, transformer);

            return OutputBuilder.GetPrettyPrintedOutput(objectToPrint, readAndTransform);
        }

        public static string PrettyPrint<T>(
            this T objectToPrint,
            object transformer)
        {
            Func<string, object, string> readAndTransform = (fieldName, fieldValue)
                => ValueTransforms.ReadAndTransformOutputValue(fieldName, fieldValue, transformer);

            return OutputBuilder.GetPrettyPrintedOutput(objectToPrint, readAndTransform);
        }

    }
}
