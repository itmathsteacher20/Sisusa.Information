

# README - Sisusa.Information.Time
This namespace contains utility classes for working with Time and Dates.

Included classes :
-   `Year` : for working with years 
- `DateRange` : for working 


## **Year Class Documentation**

## **Overview**
The `Year` class is part of the `Sisusa.Information.Time` namespace. It provides a strongly-typed representation of a year, encapsulating validation, range checks, and utility methods for working with years.

This class is immutable and supports operations such as comparisons, arithmetic, and conversions. It is especially useful in scenarios where year-related calculations or validations are required.

---

## **Features**
- Encapsulation of year-specific logic.
- Validation of year values.
- Support for two-digit year conversions.
- Implicit conversion to integer.
- Customizable time provider for testing.
- Arithmetic operations (+, -) and comparisons (<, >, ==, !=).
- Static properties for predefined values (`MinValue`, `ThisYear`).

---

## **Constructors**
### `Year(int value, int minYear = 1900, int maxYear = 2999)`
Creates a new `Year` instance with the given value and optional range bounds.

- **Parameters:**
  - `value`: The year to represent.
  - `minYear` (optional): The minimum allowed year (default is 1900).
  - `maxYear` (optional): The maximum allowed year (default is 2999).

- **Exceptions:**
  - Throws `InvalidYearException` if the `value` is out of the allowed range.

---

## **Static Properties**
### `ThisYear`
Returns a `Year` instance representing the current year. It uses the configured time provider.

### `MinValue`
Returns the smallest predefined valid year (`1900`).

---

## **Methods**
### `UseTimeProvider(ITimeProvider provider)`
Sets a custom time provider for testing or alternative time manipulation.

### `FromDateTime(DateTime dateTime)`
Creates a `Year` instance from a `DateTime` object.

- **Example:**
  ```csharp
  DateTime date = new DateTime(2023, 5, 10);
  Year year = Year.FromDateTime(date);
  Console.WriteLine(year); // Output: 2023
  ```

### `FromInt(int yearValue)`
Creates a `Year` instance from an integer value.

- **Example:**
  ```csharp
  Year year = Year.FromInt(2024);
  Console.WriteLine(year); // Output: 2024
  ```

---

## **Operators**
### Equality and Comparison
- `==`: Checks if two `Year` instances are equal.
- `!=`: Checks if two `Year` instances are not equal.
- `<`, `>`: Compares two `Year` instances.

- **Example:**
  ```csharp
  Year year1 = new Year(2020);
  Year year2 = new Year(2025);
  Console.WriteLine(year1 < year2); // Output: True
  ```

### Arithmetic
- `+`: Adds years to a `Year` instance.
- `-`: Subtracts years or calculates the difference between two `Year` instances.

- **Example:**
  ```csharp
  Year year = new Year(2020);
  Year nextYear = year + 1;
  Console.WriteLine(nextYear); // Output: 2021

  int difference = new Year(2025) - year;
  Console.WriteLine(difference); // Output: 5
  ```

### Implicit Conversion
- Automatically converts a `Year` instance to an integer.

- **Example:**
  ```csharp
  Year year = new Year(2024);
  int yearValue = year;
  Console.WriteLine(yearValue); // Output: 2024
  ```

---

## **Extension Methods**
### `IsWithinRange(this Year instance, Year minYear, Year maxYear)`
Checks if the year is within a specified range.

- **Example:**
  ```csharp
  Year year = new Year(2023);
  bool isValid = year.IsWithinRange(new Year(2020), new Year(2030));
  Console.WriteLine(isValid); // Output: True
  ```

### `IsLeapYear(this Year instance)`
Checks if the year is a leap year.

- **Example:**
  ```csharp
  Year year = new Year(2024);
  bool isLeap = year.IsLeapYear();
  Console.WriteLine(isLeap); // Output: True
  ```

---

## **Usage Examples**
### Scenario 1: Validating User Input
```csharp
try
{
    Year userInputYear = new Year(1985);
    Console.WriteLine($"User entered a valid year: {userInputYear}");
}
catch (InvalidYearException)
{
    Console.WriteLine("The entered year is invalid.");
}
```

### Scenario 2: Calculating Age
```csharp
Year birthYear = new Year(1990);
Year currentYear = Year.ThisYear;
int age = currentYear - birthYear;

Console.WriteLine($"Age: {age}"); // Output depends on the current year
```

### Scenario 3: Handling Two-Digit Year Input
```csharp
try
{
    Year shortYear = new Year(99); // Automatically converts to 1999
    Console.WriteLine(shortYear); // Output: 1999
}
catch (InvalidYearException)
{
    Console.WriteLine("Invalid year entered.");
}
```

### Scenario 4: Validating a Range
```csharp
Year startYear = new Year(2000);
Year endYear = new Year(2020);

Year yearToCheck = new Year(2010);
if (yearToCheck.IsWithinRange(startYear, endYear))
{
    Console.WriteLine($"{yearToCheck} is within range.");
}
else
{
    Console.WriteLine($"{yearToCheck} is not within range.");
}
```

---

## **Error Handling**
### `InvalidYearException`
An exception thrown when a year is out of range or invalid.

- **Example:**
  ```csharp
  try
  {
      Year invalidYear = new Year(5000); // Out of range
  }
  catch (InvalidYearException)
  {
      Console.WriteLine("Caught an invalid year exception.");
  }
  ```

---

## **Testing with Custom Time Provider**
The `UseTimeProvider` method allows setting a custom time provider for testing.

- **Example:**
  ```csharp
  Year.UseTimeProvider(new MockTimeProvider(new DateTime(2025, 1, 1)));
  Year thisYear = Year.ThisYear;
  Console.WriteLine(thisYear); // Output: 2025
  ```

---

This documentation covers the primary features, methods, and real-world examples of the `Year` class. For further assistance, consult the library's additional resources or contact support.



## **Documentation for `DateRange` Class**

The `DateRange` class in the `Sisusa.Information.Time` namespace provides an intuitive and flexible way to represent a range of dates, allowing you to perform operations like checking if a date falls within the range, calculating the duration, and comparing ranges.

## **Class Overview**

### **Namespace**
`Sisusa.Information.Time`

### **Summary**
- Represents a range of dates with a start and end date.
- Provides methods to validate, compare, and construct date ranges.

---

## **Public Properties**

### **`Start`**
- **Type**: `DateTime`
- **Description**: The start date of the range.

### **`End`**
- **Type**: `DateTime`
- **Description**: The end date of the range.

### **`Duration`**
- **Type**: `TimeSpan`
- **Description**: The duration between the `Start` and `End` dates.

---

## **Public Methods**

### **`Includes(DateTime value)`**
- **Description**: Determines whether the specified date is within the range.
- **Parameters**:
  - `value` (DateTime): The date to check.
- **Returns**: `bool` - `true` if the date is within the range; otherwise, `false`.

### **`ToString()`**
- **Description**: Returns a string representation of the date range.
- **Returns**: A formatted string in the form: `Start - End`.

### **`Equals(object? obj)`**
- **Description**: Checks equality between the current instance and another object.
- **Parameters**:
  - `obj` (object?): The object to compare.
- **Returns**: `bool` - `true` if the objects are equal; otherwise, `false`.

### **`Equals(DateRange? other)`**
- **Description**: Determines whether the specified `DateRange` is equal to the current instance.
- **Parameters**:
  - `other` (`DateRange?`): The `DateRange` to compare.
- **Returns**: `bool` - `true` if equal; otherwise, `false`.

### **`operator ==` / `operator !=`**
- **Description**: Compares two `DateRange` instances for equality or inequality.

### **`GetHashCode()`**
- **Description**: Returns a hash code for the `DateRange` instance.

---

## **Static Methods**

### **`From(DateTime startDate)`**
- **Description**: Creates a new `DateRangeBuilder` with the specified start date.
- **Parameters**:
  - `startDate` (`DateTime`): The start date of the range.
- **Returns**: A `DateRangeBuilder` instance.

---

## **Nested Classes**

### **`DateRangeBuilder`**
A builder class for constructing `DateRange` instances.

#### **Constructor**
- `DateRangeBuilder(DateTime startDate)`
  - Initializes the builder with a start date.

#### **Methods**
- **`To(DateTime endDate)`**
  - Sets the end date of the range and creates a new `DateRange` instance.
  - **Parameters**:
    - `endDate` (`DateTime`): The end date of the range.
  - **Returns**: A `DateRange` instance.

---

## **Real-World Usage Examples**

### **1. Creating a Date Range**
```csharp
var dateRange = DateRange.From(new DateTime(2024, 1, 1))
                         .To(new DateTime(2024, 12, 31));
Console.WriteLine(dateRange); // Output: 1/1/2024 12:00:00 AM - 12/31/2024 12:00:00 AM
```

---

### **2. Checking if a Date Falls Within the Range**
```csharp
var dateRange = DateRange.From(new DateTime(2024, 1, 1))
                         .To(new DateTime(2024, 12, 31));
var isIncluded = dateRange.Includes(new DateTime(2024, 6, 15)); // true
Console.WriteLine(isIncluded); // Output: True
```

---

### **3. Calculating the Duration of a Date Range**
```csharp
var dateRange = DateRange.From(new DateTime(2024, 1, 1))
                         .To(new DateTime(2024, 12, 31));
Console.WriteLine(dateRange.Duration.Days); // Output: 365
```

---

### **4. Comparing Two Date Ranges**
```csharp
var range1 = DateRange.From(new DateTime(2024, 1, 1))
                      .To(new DateTime(2024, 6, 30));

var range2 = DateRange.From(new DateTime(2024, 1, 1))
                      .To(new DateTime(2024, 6, 30));

Console.WriteLine(range1 == range2); // Output: True
```

---

### **5. Handling Invalid Ranges**
The constructor ensures that the start date is always earlier than or equal to the end date:
```csharp
var invalidRange = DateRange.From(new DateTime(2024, 12, 31))
                            .To(new DateTime(2024, 1, 1));
Console.WriteLine(invalidRange); // Output: 1/1/2024 12:00:00 AM - 12/31/2024 12:00:00 AM
```

---

### **6. Using `DateRangeBuilder` for Dynamic Construction**
```csharp
var builder = DateRange.From(new DateTime(2024, 1, 1));
// Do some processing
var dateRange = builder.To(new DateTime(2024, 6, 15));
Console.WriteLine(dateRange); // Output: 1/1/2024 12:00:00 AM - 6/15/2024 12:00:00 AM
```

---
## User Documentation: `ITimeProvider` and `SystemTimeProvider`

---

### Overview

The `Sisusa.Information.Time` namespace provides a flexible and testable interface (`ITimeProvider`) for obtaining the current time in various formats. It also includes a default implementation, `SystemTimeProvider`, that uses the system's clock to fetch the current time.

This abstraction is particularly useful in scenarios where:
1. **Dependency Injection**: You need to inject time-related functionality into classes.
2. **Testing**: You want to control or mock the current time for unit tests.
3. **Consistency**: You aim to centralize time-related operations and ensure uniformity across your application.

---

### Why Use `ITimeProvider` Instead of `DateTime.Now`?

1. **Testability**: Using `DateTime.Now` directly in code creates tight coupling with the system clock. This makes it difficult to test time-dependent logic (e.g., scheduling or expiration checks). `ITimeProvider` enables you to mock time in unit tests for consistent results.

2. **Abstraction**: Abstracting time-related operations via `ITimeProvider` provides a single point of customization. This is valuable when working in systems with diverse time zone requirements or where you might need to inject different time sources (e.g., an NTP server or a predefined test clock).

3. **Consistency**: By centralizing time-related logic, you can ensure all parts of the application adhere to the same time source, preventing discrepancies.

---

### Interface: `ITimeProvider`

The `ITimeProvider` interface provides three methods:

- `GetCurrentTime()`: Returns the local system time (`DateTime.Now`).
- `GetCurrentUtcTime()`: Returns the Coordinated Universal Time (`DateTime.UtcNow`).
- `GetCurrentTimestamp()`: Returns the Unix timestamp in seconds (`long`).

---

### Default Implementation: `SystemTimeProvider`

`SystemTimeProvider` is the default implementation of `ITimeProvider` that uses the system clock to fetch the current time.

---

### Usage Examples

#### **Real-World Scenario 1: Logging Events with Consistent Timestamps**

```csharp
public class EventLogger
{
    private readonly ITimeProvider _timeProvider;

    public EventLogger(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public void LogEvent(string message)
    {
        var timestamp = _timeProvider.GetCurrentUtcTime();
        Console.WriteLine($"[{timestamp:O}] {message}");
    }
}

// Usage
var timeProvider = new SystemTimeProvider();
var logger = new EventLogger(timeProvider);

logger.LogEvent("Application started.");
logger.LogEvent("User logged in.");
```

---

#### **Real-World Scenario 2: Testing Scheduled Tasks**

```csharp
public class Scheduler
{
    private readonly ITimeProvider _timeProvider;

    public Scheduler(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public bool IsTaskDue(DateTime scheduledTime)
    {
        var currentTime = _timeProvider.GetCurrentUtcTime();
        return currentTime >= scheduledTime;
    }
}

// Mock Implementation for Testing
public class MockTimeProvider : ITimeProvider
{
    private readonly DateTime _mockTime;

    public MockTimeProvider(DateTime mockTime)
    {
        _mockTime = mockTime;
    }

    public DateTime GetCurrentTime() => _mockTime;
    public DateTime GetCurrentUtcTime() => _mockTime;
    public long GetCurrentTimestamp() => new DateTimeOffset(_mockTime).ToUnixTimeSeconds();
}

// Usage in Test
var mockTimeProvider = new MockTimeProvider(new DateTime(2024, 12, 25, 12, 0, 0));
var scheduler = new Scheduler(mockTimeProvider);

Console.WriteLine(scheduler.IsTaskDue(new DateTime(2024, 12, 25, 11, 0, 0))); // True
Console.WriteLine(scheduler.IsTaskDue(new DateTime(2024, 12, 25, 13, 0, 0))); // False
```

---

#### **Real-World Scenario 3: Coordinating Across Time Zones**

```csharp
public class TimeZoneConverter
{
    private readonly ITimeProvider _timeProvider;

    public TimeZoneConverter(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public DateTime ConvertToTimeZone(TimeZoneInfo timeZone)
    {
        var utcTime = _timeProvider.GetCurrentUtcTime();
        return TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZone);
    }
}

// Usage
var timeProvider = new SystemTimeProvider();
var timeZoneConverter = new TimeZoneConverter(timeProvider);

var estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
var localTime = timeZoneConverter.ConvertToTimeZone(estTimeZone);

Console.WriteLine($"Eastern Time: {localTime}");
```

---

### Summary

By using `ITimeProvider`, you decouple your application logic from the system clock. This allows for enhanced testability, flexibility, and consistency across your application, making it an invaluable tool for time-sensitive applications. The default `SystemTimeProvider` implementation ensures seamless integration with the system clock while supporting future extensions or custom time sources.
## **Key Notes**
1. The class uses `init` properties, ensuring immutability after initialization.
2. The constructor automatically adjusts `Start` and `End` if the input dates are out of order.
3. Comparison operators (`==` and `!=`) are overridden for easy equality checks.

The `DateRange` class is a powerful tool for managing time periods in various applications, including scheduling, reporting, and validation.