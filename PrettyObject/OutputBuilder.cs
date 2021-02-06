using System;
using System.Reflection;

namespace PrettyObject
{
    public static class OutputBuilder
    {
        public static PropertyInfo[] GetObjectProperties(object objectToPrint)
        {
            if (objectToPrint == null)
            {
                throw new Exception("Object to print cannot be null");
            }

            return objectToPrint.GetType().GetProperties();
        }

        public static string GetPrettyPrintedOutput<T>(
            this T objectToPrint,
            Func<string, object, string> readAndTransformFieldValue)
        {
            var outputString = "";

            var properties = GetObjectProperties(objectToPrint);

            foreach (var property in properties)
            {
                var fieldName = property.Name;
                var fieldValue = property.GetValue(objectToPrint);

                var finalValue = readAndTransformFieldValue(fieldName, fieldValue);

                outputString += $"{fieldName}: {finalValue}\n";
            }

            return outputString;
        }

    }
}
