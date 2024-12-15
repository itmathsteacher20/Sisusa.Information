# User Documentation for `Sisusa.Information.Identification` Namespace

This documentation provides a comprehensive guide to the `Sisusa.Information.Identification` namespace, which includes classes and enumerations for managing personal identification details. It covers the `Gender` enumeration, the `NameOfPerson` class, and supporting components.

## Overview
The `Sisusa.Information.Identification` namespace is designed to provide structured and validated representations of personal identification data, such as names and gender. This is useful in applications that require consistent handling of personal data, such as HR systems, educational platforms, and healthcare management systems.

---

## Components

### 1. `Gender` Enumeration
The `Gender` enumeration represents the gender of an individual.

#### Definition
```csharp
public enum Gender : ushort
{
    Female = 1,
    Male = 2,
    Unknown = 3
}
```

#### Values
- **Female** (1): Represents the female gender.
- **Male** (2): Represents the male gender.
- **Unknown** (3): Represents an unknown or unspecified gender.

#### Use Cases
- Storing gender information in a database.
- Filtering records by gender in reports.
- Providing default values when gender is unknown.

#### Example
```csharp
Gender userGender = Gender.Female;
if (userGender == Gender.Unknown)
{
    Console.WriteLine("Gender is not specified.");
}
```

---

### 2. `NameOfPerson` Class
The `NameOfPerson` class represents a person’s name, including first, middle, and last names. It provides various utilities for name management, such as retrieving full names, initials, and shortened names.

#### Properties
- **Firstname**: Gets the first name of the person.
- **MiddleNames**: Gets the middle names of the person.
- **Lastname**: Gets the last name of the person.
- **HasOtherNames**: Indicates whether the person has middle names.

#### Methods
- **GetFullname(NameOrder nameOrder)**: Returns the full name in the specified order.
- **GetInitials(NameOrder nameOrder)**: Returns the initials of the name.
- **GetShortenedName(NameOrder nameOrder)**: Returns a shortened version of the name.
- **ToString()**: Returns the full name as a string.
- **Equals(Object obj)**: Determines whether the current instance is equal to another object.
- **GetHashCode()**: Returns the hash code for the current instance.

#### Enums
- **NameOrder**: Defines the order of a person’s name:
    - `SurnameNames`: Surname followed by other names.
    - `NamesSurname`: Other names followed by surname.

#### Builder
The `NameOfPersonBuilder` class facilitates constructing a `NameOfPerson` instance using a fluent API.

#### Validation
The `NameOfPersonValidator` ensures the validity of name details, such as non-empty first and last names.

#### Use Cases
- Formatting names for official documentation.
- Validating name inputs in forms.
- Generating initials for identification purposes.

#### Example
##### Creating a Name Instance
```csharp
var name = NameOfPerson.New()
    .WithFirstName("Jane")
    .HavingOtherNames("Elizabeth")
    .AndLastName("Doe")
    .TryBuild();

Console.WriteLine(name.GetFullname(NameOrder.NamesSurname));
// Output: "Jane Elizabeth Doe"
```

##### Retrieving Initials
```csharp
string initials = name.GetInitials(NameOrder.SurnameNames);
Console.WriteLine(initials);
// Output: "D.J.E."
```

##### Validating a Name
```csharp
var validationErrors = NameOfPersonValidator.Validate(name);
if (validationErrors.Count > 0)
{
    Console.WriteLine(string.Join("\n", validationErrors));
}
```

---

### 3. `InvalidNameOfPersonException` Class
This exception is thrown when an invalid `NameOfPerson` instance is created or used.

#### Definition
```csharp
public sealed class InvalidNameOfPersonException : Exception
```

#### Use Cases
- Ensuring valid names are entered in registration forms.
- Preventing incomplete data in databases.

#### Example
```csharp
try
{
    var invalidName = NameOfPerson.New()
        .WithFirstName("")
        .AndLastName("Doe")
        .TryBuild();
}
catch (InvalidNameOfPersonException ex)
{
    Console.WriteLine(ex.Message);
    // Output: "Name of person must at least have a valid firstname and surname."
}
```

---

## Best Practices
1. **Validation**: Always validate `NameOfPerson` instances using `NameOfPersonValidator` to ensure data integrity.
2. **Consistent Naming**: Use the `NameOrder` enumeration to standardize name formats across the application.
3. **Error Handling**: Catch `InvalidNameOfPersonException` to gracefully handle invalid name inputs.
4. **Enums for Gender**: Default to `Gender.Unknown` for unspecified gender data to avoid null values.

---

## Real-World Applications
1. **Human Resource Systems**: Use `NameOfPerson` for managing employee records with full names, initials, and gender.
2. **Educational Platforms**: Store and format student and teacher names for reports and certificates.
3. **Healthcare Systems**: Record patient names and genders for medical records.

---

This documentation covers all primary aspects of the `Sisusa.Information.Identification` namespace, offering examples and practical applications to help developers effectively integrate these components into their systems.

