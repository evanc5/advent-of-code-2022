Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var ScoreLookup = new Dictionary<string, int>()
    {
        ["A X"] = 1 + 3,    //opponent rock v self rock (draw)
        ["A Y"] = 2 + 6,    //opponent rock v self paper (win)
        ["A Z"] = 3 + 0,    //opponent rock v self scissors (loss)
        ["B X"] = 1 + 0,    //opponent paper v self rock (loss)
        ["B Y"] = 2 + 3,    //opponent paper v self paper (draw)
        ["B Z"] = 3 + 6,    //opponent paper v self scissors (win)
        ["C X"] = 1 + 6,    //opponent scissors v self rock (win)
        ["C Y"] = 2 + 0,    //opponent scissors v self paper (loss)
        ["C Z"] = 3 + 3     //opponent scissors v self scissors (draw)
    };

    var total = 0;
    foreach (var line in input)
    {
        total += ScoreLookup[line];
    }

    var elapsedTime = System.Diagnostics.Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine($"Part 1: {total}");
    System.Diagnostics.Debug.WriteLine($"Part 1: {elapsedTime}");
}

static void Part2()
{
    var input = File.ReadAllLines(@".\input.txt");
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

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
    foreach (var line in input)
    {
        total += ScoreLookup[line];
    }

    var elapsedTime = System.Diagnostics.Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine($"Part 2: {total}");
    System.Diagnostics.Debug.WriteLine($"Part 2: {elapsedTime}");
}