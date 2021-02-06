using ApprovalTests;
using Xunit;
using PrettyObject;
using ApprovalTests.Reporters;
using System;

namespace PrettyObjectUnit
{
    [UseReporter(typeof(DiffReporter))]
    public class PrettyPrintTests
    {
        class TestObject
        {
            public TestObject()
            {
                StringField = "This is a test";
                IntField = 1234;
            }

            public string StringField { get; set; }
            public int IntField { get; set; }
            public object NullValue { get; set; }
        }

        [Fact]
        public void PrettyPrint_returns_newline_delimited_string_of_all_object_fields()
        {
            var testObject = new TestObject();

            var objectFieldOutput = PrettyPrinter.PrettyPrint(testObject);

            Approvals.Verify(objectFieldOutput);
        }

        private string ReverseString(string value)
        {
            var charArray = value.ToCharArray();
            Array.Reverse(charArray);

            return new string(charArray);
        }

        [Fact]
        public void PrettyPrint_transforms_values_when_transformer_is_provided_by_anonymous_object()
        {
            var testObject = new TestObject();

            var objectFieldOutput = PrettyPrinter.PrettyPrint(
                testObject,
                new {
                    StringField = new Func<string, string>((value) => ReverseString(value))
                });

            Approvals.Verify(objectFieldOutput);
        }


        [Fact]
        public void PrettyPrint_transforms_values_when_transformer_is_provided_by_object_returning_func()
        {
            var testObject = new TestObject();

            Func<string, object, object> transformStringValue = (name, value) =>
            {
                switch (name)
                {
                    case "StringField":
                        var stringValue = (string)value;
                        
                        var charArray = stringValue.ToCharArray();
                        Array.Reverse(charArray);

                        return new string(charArray);
                    default:
                        return value.ToNullSafeString();
                }
            };

            var objectFieldOutput = testObject.PrettyPrint(transformStringValue);

            Approvals.Verify(objectFieldOutput);
        }

        [Fact]
        public void PrettyPrint_transforms_values_when_transformer_is_provided_by_string_returning_func()
        {
            var testObject = new TestObject();

            Func<string, object, string> transformStringValue = (name, value) =>
            {
                switch (name)
                {
                    case "StringField":
                        var stringValue = (string)value;

                        var charArray = stringValue.ToCharArray();
                        Array.Reverse(charArray);

                        return new string(charArray);
                    default:
                        return value.ToNullSafeString();
                }
            };

            var objectFieldOutput = testObject.PrettyPrint(transformStringValue);

            Approvals.Verify(objectFieldOutput);
        }

    }
}
