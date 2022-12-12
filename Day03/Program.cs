Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var result = 0;
    foreach (var line in input)
    {
        var compartment1 = line.Take(line.Length / 2);
        var compartment2 = line.TakeLast(line.Length / 2);
        var intersection = compartment1.Intersect(compartment2).First();
        result += GetPriority(intersection);
    }

    var elapsedTime = System.Diagnostics.Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine($"Part 1: {result}");
    System.Diagnostics.Debug.WriteLine($"Part 1: {elapsedTime}");
}

static void Part2()
{
    var input = File.ReadAllLines(@".\input.txt");
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var result = 0;
    var currentGroup = new string[3];
    var currentCount = 0;
    foreach (var line in input)
    {
        currentGroup[currentCount++] = line;
        if (currentCount >= 3)
        {
            currentCount = 0;
            var intersection = currentGroup[0].Intersect(currentGroup[1]).Intersect(currentGroup[2]).First();
            result += GetPriority(intersection);
        }
    }

    var elapsedTime = System.Diagnostics.Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine($"Part 2: {result}");
    System.Diagnostics.Debug.WriteLine($"Part 2: {elapsedTime}");
}

static int GetPriority(char item)
{
    if (char.IsUpper(item))
    {
        return (int)item - 65 + 27;
    }
    else
    {
        return (int)item - 97 + 1;
    }
}