Sequence class, that given min, max and step can calculate, without enumerating, such basic descriptives as sum, length, Variance, Standard Deviation etc. 
Getting the actual range is also possible, by calling GetFullSequence() like this:
```c#
            var tested = new Sequence(0, 10, 1);
            var sequence = tested.GetFullSequence(); // this is lazy evaluated, so call ToList() to eager load.
```
Also, extension methods to generate sequences in GetSequence class.

Example usages:

```c#
            var tested = new Sequence(0, 10, 1);
            var sum = tested.Sum; // 55
            var variance = tested.Variance; // 10
```
