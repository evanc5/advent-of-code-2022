Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllText(@".\input.txt");
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var result = FindMarker(input, 4);

    var elapsedTime = System.Diagnostics.Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine($"Part 1: {result}");
    System.Diagnostics.Debug.WriteLine($"Part 1: {elapsedTime}");
}

static void Part2()
{
    var input = File.ReadAllText(@".\input.txt");
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var result = FindMarker(input, 14);

    var elapsedTime = System.Diagnostics.Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine($"Part 2: {result}");
    System.Diagnostics.Debug.WriteLine($"Part 2: {elapsedTime}");
}

static int FindMarker(string input, int count)
{
    for (int i = count - 1; i < input.Length; i++)
    {
        var lastN = input.Substring(i - (count - 1), count);
        if (lastN.Distinct().Count() == count)
        {
            return i + 1;
        }
    }
    return -1;
}