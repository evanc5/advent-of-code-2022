Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

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

    Console.WriteLine($"Part 1: {max}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 1: {sw.Elapsed}");
}

static void Part2()
{
    var input = File.ReadAllLines(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

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

    var sorted = list.OrderByDescending(v => v).Take(3).ToArray();
    var result = sorted[0] + sorted[1] + sorted[2];
    Console.WriteLine($"Part 2: {result}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 2: {sw.Elapsed}");
}