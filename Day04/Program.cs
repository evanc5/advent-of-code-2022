Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var result = 0;
    foreach (var line in input)
    {
        var split = line.Split(',');
        if (FullyContains(split[0], split[1])) result++;
    }
    Console.WriteLine($"Part 1: {result}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 1: {sw.Elapsed}");
}

static void Part2()
{
    var input = File.ReadAllLines(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var result = 0;
    foreach (var line in input)
    {
        var split = line.Split(',');
        if (Overlaps(split[0], split[1])) result++;
    }
    Console.WriteLine($"Part 2: {result}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 2: {sw.Elapsed}");
}

static bool FullyContains(string elf1, string elf2)
{
    var range1 = elf1.Split('-').Select(i => int.Parse(i)).ToArray();
    var range2 = elf2.Split('-').Select(i => int.Parse(i)).ToArray();

    if (range1[0] <= range2[0] && range1[1] >= range2[1]) return true;
    if (range2[0] <= range1[0] && range2[1] >= range1[1]) return true;

    return false;
}

static bool Overlaps(string elf1, string elf2)
{
    var range1 = elf1.Split('-').Select(i => int.Parse(i)).ToArray();
    var range2 = elf2.Split('-').Select(i => int.Parse(i)).ToArray();

    if (range1[0] <= range2[0] && range1[1] >= range2[1]) return true;
    if (range2[0] <= range1[0] && range2[1] >= range1[1]) return true;

    if (range1[0] <= range2[0] && range1[1] >= range2[0]) return true;
    if (range2[0] <= range1[0] && range2[1] >= range1[0]) return true;

    if (range1[1] >= range2[1] && range1[1] <= range2[0]) return true;
    if (range2[1] >= range1[1] && range2[1] <= range1[0]) return true;

    return false;
}