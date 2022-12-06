Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllText(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var result = 0;
    for (int i = 3; i < input.Length; i++)
    {
        var lastFour = input.Substring(i - 3, 4);
        if (lastFour.Distinct().Count() == 4)
        {
            result = i + 1;
            break;
        }
    }
    Console.WriteLine($"Part 1: {result}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 1: {sw.Elapsed}");
}

static void Part2()
{
    var input = File.ReadAllText(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var result = 0;
    for (int i = 13; i < input.Length; i++)
    {
        var lastFourteen = input.Substring(i - 13, 14);
        if (lastFourteen.Distinct().Count() == 14)
        {
            result = i + 1;
            break;
        }
    }
    Console.WriteLine($"Part 2: {result}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 2: {sw.Elapsed}");
}