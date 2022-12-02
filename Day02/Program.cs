Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var total = 0;
    foreach (var line in input)
    {
        var split = line.Split(' ');
        total += RoundScoreV1(split[0], split[1]);
    }
    Console.WriteLine($"Part 1: {total}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 1: {sw.Elapsed}");
}

static void Part2()
{
    var input = File.ReadAllLines(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var total = 0;
    foreach (var line in input)
    {
        var split = line.Split(' ');
        total += RoundScoreV2(split[0], split[1]);
    }
    Console.WriteLine($"Part 2: {total}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 2: {sw.Elapsed}");
}

static int RoundScoreV1(string opponent, string response)
{
    var result = RoundResult(opponent, response);

    const string selfRock = "X";
    const string selfPaper = "Y";
    const string selfScissors = "Z";

    switch (response)
    {
        case selfRock:
            return result + 1;
        case selfPaper:
            return result + 2;
        case selfScissors:
            return result + 3;
        default:
            throw new ArgumentOutOfRangeException(response);
    }
}

static int RoundScoreV2(string opponent, string result)
{
    const string opponentRock = "A";
    const string opponentPaper = "B";
    const string opponentScissors = "C";

    const string loss = "X";
    const string draw = "Y";
    const string win = "Z";

    switch (opponent)
    {
        case opponentRock:
            switch (result)
            {
                case win:
                    return 6 + 2;
                case draw:
                    return 3 + 1;
                case loss:
                    return 0 + 3;
                default:
                    throw new ArgumentOutOfRangeException(result);
            }
        case opponentPaper:
            switch (result)
            {
                case win:
                    return 6 + 3;
                case draw:
                    return 3 + 2;
                case loss:
                    return 0 + 1;
                default:
                    throw new ArgumentOutOfRangeException(result);
            }
        case opponentScissors:
            switch (result)
            {
                case win:
                    return 6 + 1;
                case draw:
                    return 3 + 3;
                case loss:
                    return 0 + 2;
                default:
                    throw new ArgumentOutOfRangeException(result);
            }
        default:
            throw new ArgumentOutOfRangeException(opponent);
    }
}

static int RoundResult(string opponent, string response)
{
    const string opponentRock = "A";
    const string opponentPaper = "B";
    const string opponentScissors = "C";

    const string selfRock = "X";
    const string selfPaper = "Y";
    const string selfScissors = "Z";

    switch (opponent)
    {
        case opponentRock:
            switch (response)
            {
                case selfRock:
                    return 3;
                case selfPaper:
                    return 6;
                case selfScissors:
                    return 0;
                default:
                    throw new ArgumentOutOfRangeException(response);
            }
        case opponentPaper:
            switch (response)
            {
                case selfRock:
                    return 0;
                case selfPaper:
                    return 3;
                case selfScissors:
                    return 6;
                default:
                    throw new ArgumentOutOfRangeException(response);
            }
        case opponentScissors:
            switch (response)
            {
                case selfRock:
                    return 6;
                case selfPaper:
                    return 0;
                case selfScissors:
                    return 3;
                default:
                    throw new ArgumentOutOfRangeException(response);
            }
        default:
            throw new ArgumentOutOfRangeException(opponent);
    }
}