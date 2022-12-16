using System.Text.RegularExpressions;

Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var monkeys = InitializeMonkeys(input, 8);
    for (int i = 0; i < 20; i++)
    {
        foreach (var monkey in monkeys)
        {
            monkey.ProcessTurn();
        }
    }
    var result = monkeys.Select(m => m.InspectionCount).OrderDescending().Take(2).Aggregate((x, y) => x * y);

    var elapsedTime = System.Diagnostics.Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine($"Part 1: {result}");
    System.Diagnostics.Debug.WriteLine($"Part 1: {elapsedTime}");
}

static void Part2()
{
    var input = File.ReadAllLines(@".\input.txt");
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var monkeys = InitializeMonkeys(input, 8);
    for (int i = 0; i < 10000; i++)
    {
        foreach (var monkey in monkeys)
        {
            monkey.ProcessTurn(false);
        }
    }
    var result = monkeys.Select(m => m.InspectionCount).OrderDescending().Take(2).Aggregate((x, y) => x * y);

    var elapsedTime = System.Diagnostics.Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine($"Part 2: {result}");
    System.Diagnostics.Debug.WriteLine($"Part 2: {elapsedTime}");
}

static Monkey[] InitializeMonkeys(string[] input, int count)
{
    var monkeys = new Monkey[count];
    for (int i = 0; i < monkeys.Length; i++)
    {
        var lines = input.Skip(i * 7).Take(6);
        monkeys[i] = new Monkey(lines);
    }
    foreach (var monkey in monkeys)
    {
        monkey.InitializeNeighbors(monkeys[monkey.Pass], monkeys[monkey.Fail]);
        monkey.CommonDivisor = monkeys.Select(m => m.Test).Aggregate((x, y) => x * y);
    }
    return monkeys;
}

record class Monkey(int ID, IList<long> Inventory, string Operation, int OperationAmount, int Test, int Pass, int Fail)
{
    private Monkey? _passMonkey;
    private Monkey? _failMonkey;
    public long CommonDivisor { get; set; } = 1;

    public long InspectionCount { get; private set; } = 0;

    public Monkey(IEnumerable<string> lines) : this(0, new List<long>(), "", 0, 0, 0, 0)
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
                    Inventory = Regex.Matches(line, pattern).Select(m => long.Parse(m.ValueSpan)).ToList();
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
                    Test = int.Parse(Regex.Matches(line, pattern).First().ValueSpan);
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

    public void InitializeNeighbors(Monkey passMonkey, Monkey failMonkey)
    {
        _passMonkey = passMonkey;
        _failMonkey = failMonkey;
    }

    public void ProcessTurn(bool worryDivides = true)
    {
        InspectionCount += Inventory.Count;
        foreach (var item in Inventory)
        {
            var itemValue = item;
            switch (Operation)
            {
                case "+":
                    itemValue += OperationAmount;
                    break;
                case "*":
                    itemValue *= OperationAmount;
                    break;
                case "**":
                    itemValue *= itemValue;
                    break;
            }
            if (worryDivides) itemValue /= 3;
            itemValue %= CommonDivisor;
            if (itemValue % Test == 0)
            {
                _passMonkey?.Inventory.Add(itemValue);
            }
            else
            {
                _failMonkey?.Inventory.Add(itemValue);
            }
        }
        Inventory.Clear();
    }
}