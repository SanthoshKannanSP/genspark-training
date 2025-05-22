# Day 12 - May 20th, 2025
## Session Overview
- Arrays
- Generics
- Boxing and Unboxing
- IComparable
- List TrimExcess()

## Arrays
[Reference](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/arrays)
- Store multiple elements of same data type
- To store elements of any type, use `object` as the type of array
```
type[] arrayName;
```
- Array is a reference type
- Uninitialized elements are set to their default value for that type
- Can be single-dimensional, multi-dimensional or jagged

### Single-dimensional Array
- Sequence of elements
- Access elements by their ordinal position in the array (index)
```
int[] array = new int[5];

int[] array = { 1, 2, 3, 4, 5 };

Console.WriteLine(array[2]);
```
### Multi-dimensional Array
- Array with more than one dimension
- Access elements by their index in each dimension, seperated by commas
```
int[,] array = new int[4,2];

int[,] array = { { 1, 2 }, { 3, 4 }, { 5, 6 } };

Console.WriteLine(array[1,0]);
```
### Jagged Array
- Array of arrays
- Each array element can be of different size
- The number of dimensions is set during the array variable declaration. The length of each dimension is set when the array instance is created
- Elements are reference types and are initialized as null
```
int[][] array = new int[3][];
array[0] = [1,3,4,6];
array[1] = [9,2,1];
array[2] = [9,1,2,4,6,7];

int[][] array = 
[
	[1,3,4,6],
	[9,2,1],
	[9,1,2,4,6,7]
];

Console.WriteLine(array[1][2]);
```

## Generics
[Reference](https://learn.microsoft.com/en-us/dotnet/standard/generics/)
- allows defining classes, methods, interfaces and delegates with a placeholder for the type of data stored/used
```
public class TaggedVariable<T>
{
    public T data;
    public string tag = "This is the default tag";
    public TaggedVariable(T value)
    {
        data = value;
    }
}
```
- When creating an instance of a generic class, you can specify the actual types to substitute
```
var name = new TaggedVariable<string>("Ram");
name.tag = "This variable stores the name of a person";
```
- Built-in Generics - `List<T>`, `Dictionary<TKey,TValue>`, `Queue<T>`, `Stack<T>`

## Boxing and Unboxing
[Reference](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/boxing-and-unboxing)
- **Boxing:** process of converting a value type into an object (reference type)
- allocates a new object instance on the heap and copies the value into the new object
```
int number = 123;
object obj = i; // Boxing
```
- **Unboxing:** process of converting an object back to it's original value type
- Requires an explicit cast, and if the object isn't actually the casted type, `InvalidCastException` will be thrown
- Unboxing operation will first check if the object instance is a boxed value of the given cast type and then copy the value from the instance into a value-type variable.
```
int num1 = 123;
object obj = i; // Boxing
int num2 = (int)obj; // Unboxing
```
- Boxing and unboxing is commonly used in non-generic collections like `ArrayList`, which stores `objects`
- The performance cost is high as copying data and heap allocation can slow things down if used frequently
- Type safety is a concern as unboxing must be down carefully to avoid runtime errors
- The introduction of generics significantly reduced the need for boxing and unboxing

## IComparable Interface
[Reference](https://learn.microsoft.com/en-us/dotnet/api/system.icomparable-1?view=net-9.0)
- The interface that a class implements to define how objects of the class should be compared for sorting
- A class implementing `IComparable` interface must implement the `CompareTo` method
- The `CompareTo` method should return `-1` if **this** object is less than the other, `0` if they are equal and `1` if **this** object is greater than the other
```
public class Employee : IComparable<Employee>
{
    public string Name { get; set; }
    public int Age { get; set; }
    
    public int CompareTo(Person? other)
    {
        if (other == null)
            return 1;
        return this.Age.CompareTo(other.Age);
    }
}
```
- The `IComparable` interface should be implemented to perform sorting in collections (`List<T>.Sort()`)

## List TrimExcess
[Reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.trimexcess?view=net-9.0)
- The `Capacity` of a list is the number of elements that a list can store before resizing is required
- The `Count` is the actual number of elements in the list and will be always lesser than or equal to `Capacity`
- This is done inorder to reduce the number of memory allocations done when adding elements to the list. Instead of increasing its size by just one each time an element is added, it allocates extra spaces in chunks.
- The `TrimExcess` method can be used to deallocate the unused memory overhead of a list by setting its `Capacity` to match its `Count`
- If the current `Count` is less than 90% of its `Capacity`, `TrimExcess()` will resize the internal array to fit. Otherwise, it does nothing.
- Only use after large reduction in list size or if no more elements will be added to the list (The performance cost of trimming is relatively expensive as a new smaller list is allocated and the values are copied to it. Further addition of elements to the list will cause the `Capacity` to grow again, so trimming and adding again results in double the work)