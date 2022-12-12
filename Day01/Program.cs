Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var max = 0;
    var current = 0;

    foreach (var line in input)
    {
        if (!string.IsNullOrWhiteSpace(line))
        {
            current += int.Parse(line);
        }
        else
        {
            max = Math.Max(current, max);
            current = 0;
        }
    }
    max = Math.Max(current, max);

    var elapsedTime = System.Diagnostics.Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine($"Part 1: {max}");
    System.Diagnostics.Debug.WriteLine($"Part 1: {elapsedTime}");
}

static void Part2()
{
    var input = File.ReadAllLines(@".\input.txt");
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var list = new List<int>();
    var current = 0;

    foreach (var line in input)
    {
        if (!string.IsNullOrWhiteSpace(line))
        {
            current += int.Parse(line);
        }
        else
        {
            list.Add(current);
            current = 0;
        }
    }
    list.Add(current);

    var result = list.OrderByDescending(v => v).Take(3).Sum();

    var elapsedTime = System.Diagnostics.Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine($"Part 2: {result}");
    System.Diagnostics.Debug.WriteLine($"Part 2: {elapsedTime}");
}