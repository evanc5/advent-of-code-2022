Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var ScoreLookup = new Dictionary<string, int>()
    {
        ["A X"] = 1 + 3,    //opponent rock self rock
        ["A Y"] = 2 + 6,    //opponent rock self paper
        ["A Z"] = 3 + 0,    //opponent rock self scissors
        ["B X"] = 1 + 0,    //opponent paper self rock
        ["B Y"] = 2 + 3,    //opponent paper self paper
        ["B Z"] = 3 + 6,    //opponent paper self scissors
        ["C X"] = 1 + 6,    //opponent scissors self rock
        ["C Y"] = 2 + 0,    //opponent scissors self paper
        ["C Z"] = 3 + 3     //opponent scissors self scissors
    };

    var total = 0;
    foreach(var line in input)
    {
        total += ScoreLookup[line];
    }
    Console.WriteLine($"Part 1: {total}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 1: {sw.Elapsed}");
}

static void Part2()
{
    var input = File.ReadAllLines(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var ScoreLookup = new Dictionary<string, int>()
    {
        ["A X"] = 0 + 3,    //opponent rock loss (self scissors)
        ["A Y"] = 3 + 1,    //opponent rock draw (self rock)
        ["A Z"] = 6 + 2,    //opponent rock win (self paper)
        ["B X"] = 0 + 1,    //opponent paper loss (self rock)
        ["B Y"] = 3 + 2,    //opponent paper draw (self paper)
        ["B Z"] = 6 + 3,    //opponent paper win (self scissors)
        ["C X"] = 0 + 2,    //opponent scissors loss (self paper)
        ["C Y"] = 3 + 3,    //opponent scissors draw (self scissors)
        ["C Z"] = 6 + 1     //opponent scissors win (self rock)
    };

    var total = 0;
    foreach(var line in input)
    {
        total += ScoreLookup[line];
    }
    Console.WriteLine($"Part 2: {total}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 2: {sw.Elapsed}");
}