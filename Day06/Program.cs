Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllText(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var result = FindMarker(input, 4);
    Console.WriteLine($"Part 1: {result}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 1: {sw.Elapsed}");
}

static void Part2()
{
    var input = File.ReadAllText(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var result = FindMarker(input, 14);
    Console.WriteLine($"Part 2: {result}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 2: {sw.Elapsed}");
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