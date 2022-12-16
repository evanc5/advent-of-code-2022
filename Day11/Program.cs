using System.Text.RegularExpressions;

Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var result = 0;
    var monkeys = new Monkey[7];
    for (int i = 0; i < monkeys.Length; i++)
    {
        var lines = input.Skip(i * 7).Take(6);
        monkeys[i] = new Monkey(lines);
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
    foreach (var line in input)
    {

    }

    var elapsedTime = System.Diagnostics.Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine($"Part 2: {result}");
    System.Diagnostics.Debug.WriteLine($"Part 2: {elapsedTime}");
}

record class Monkey(int ID, IList<int> Inventory, string Operation, int OperationAmount, string TestOperation, int TestAmount, int Pass, int Fail)
{
    public Monkey(IEnumerable<string> lines) : this(0, new List<int>(), "", 0, "", 0, 0, 0)
    {
        var currentLine = 0;
        foreach (var line in lines)
        {
            string pattern = @"[0-9]+";
            switch (currentLine)
            {
                case 0:
                    ID = int.Parse(Regex.Matches(line, pattern).First().ValueSpan);
                    break;
                case 1:
                    Inventory = Regex.Matches(line, pattern).Select(m => int.Parse(m.ValueSpan)).ToList();
                    break;
                case 2:
                    pattern = @"old ([-+*(/]) ([0-9]+|(?:old))";
                    var match = Regex.Match(line, pattern);
                    Operation = match.Groups[1].Value;
                    switch (match.Groups[2].Value)
                    {
                        case "old":
                            Operation = "**";
                            OperationAmount = 2;
                            break;
                        default:
                            OperationAmount = int.Parse(match.Groups[2].Value);
                            break;
                    }
                    break;
                case 3:
                    var split = line.Trim().Split(' ');
                    TestOperation = split[1];
                    TestAmount = int.Parse(split[3]);
                    break;
                case 4:
                    Pass = int.Parse(Regex.Matches(line, pattern).First().ValueSpan);
                    break;
                case 5:
                    Fail = int.Parse(Regex.Matches(line, pattern).First().ValueSpan);
                    break;
            }

            currentLine++;
        }
    }
}

enum OperationEnum
{
    Add,
    Subtract,
    Multiply,
    Divide
}