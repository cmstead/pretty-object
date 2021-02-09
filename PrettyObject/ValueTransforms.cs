using System;

namespace PrettyObject
{
    public static class ValueTransforms
    {
        public static string ReadAndTransformOutputValue(
            string fieldName,
            object fieldValue,
            Func<string, object, string> transformer)
        {
            var transformedValue = transformer == null
                ? fieldValue
                : transformer(fieldName, fieldValue);

            return transformedValue.ToStringOrNull();
        }

        public static string ReadAndTransformOutputValue(
            string fieldName,
            object fieldValue,
            object transformer)
        {
            var finalValue = fieldValue;

            try
            {
                var typeOfObject = transformer.GetType();
                var transformationFieldProperty = typeOfObject.GetProperty(fieldName);

                if (transformationFieldProperty != null)
                {
                    var transformationFunc = transformationFieldProperty.GetValue(transformer);

                    var transformationFuncType = transformationFunc.GetType();

                    var transformerMethod = transformationFuncType.GetMethod("Invoke");
                    var transformerArguments = new[] { fieldValue };

                    finalValue = transformerMethod.Invoke(transformationFunc, transformerArguments);
                }
            }
            catch (Exception)
            {
                // property missing, that's okay                
            }
            

            return finalValue.ToStringOrNull();
        }
    }
}
