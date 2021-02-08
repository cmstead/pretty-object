# Pretty Object #

Pretty Object is an object pretty-printer for handling Golden Master test output. Pretty Object, out of the box, will print all accessible fields in an object as a new-line delimited output. You can also provide transformations for any field values which either should be ignored, or might need to be modified, such as DateTime comparisons.

## Usage ##

### Just Printing Object Properties ###

Example with Approvals:

```csharp
// Don't forget to add a using statement:
// using PrettyObject;

var testObject = new TestObject();

var objectFieldOutput = PrettyPrinter.PrettyPrint(testObject);

Approvals.Verify(objectFieldOutput);
```

It can also be used as a mixin:

```csharp
// Don't forget to add a using statement:
// using PrettyObject;

var testObject = new TestObject();

Approvals.Verify(testObject.PrettyPrint(testObject));
```

### Modifying Properties Before Printing ###

Using an anonymous object with field name and transformation funcs:

```csharp
var testObject = new TestObject();

var verificationOutput = testObject.PrettyPrint(new
{
    StringField = new Func<string, string>((value)
        => ReverseString(value)),
    IntField = new Func<int, string>((value)
        => (value * 2).ToString())
});

Approvals.Verify(verificationOutput);
```

Using a single func (JavaScript JSON.stringify style transformations):

```csharp
var testObject = new TestObject();

Func<string, object, string> transformStringValue = (name, value) =>
{
    switch (name)
    {
        case "StringField":
            return ReverseString(value);
        default:
            return value.ToNullSafeString();
    }
};

var objectFieldOutput = testObject.PrettyPrint(transformStringValue);

Approvals.Verify(objectFieldOutput);
```

## Public API ##

- PrettyPrint:
    - `public string PrettyPrint(this T objectToPrint)`
    - `public string PrettyPrint(this T objectToPrint, Func<string, object, object> transformer)`
    - `public string PrettyPrint(this T objectToPrint, Func<string, object, string> transformer)`
    - `public string PrettyPrint(this T objectToPrint, object transformer)`
        - All properties for the transformer object must be `Func<T, string>`
- ToNullSafeString:
    - `public string ToNullSafeString<T>(this T value)`
- ToStringOrNull:
    - `public string ToStringOrNull<T>(this T value)`

## Thanks and Contributors ##

Big thanks to [@jason-kerney](https://github.com/jason-kerney) for his contributions!