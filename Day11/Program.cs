using System.Text.RegularExpressions;

Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var monkeys = new Monkey[8];
    for (int i = 0; i < monkeys.Length; i++)
    {
        var lines = input.Skip(i * 7).Take(6);
        monkeys[i] = new Monkey(lines);
    }
    foreach (var monkey in monkeys)
    {
        monkey.InitializeNeighbors(monkeys[monkey.Pass], monkeys[monkey.Fail]);
    }
    for (int i = 0; i < 20; i++)
    {
        foreach (var monkey in monkeys)
        {
            monkey.ProcessTurn();
        }
    }
    var result = monkeys.Select(m => m.InspectionCount).OrderDescending().Take(2).Product();

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

record class Monkey(int ID, IList<int> Inventory, string Operation, int OperationAmount, int Test, int Pass, int Fail)
{
    private Monkey? _passMonkey;
    private Monkey? _failMonkey;

    public int InspectionCount { get; private set; } = 0;

    public Monkey(IEnumerable<string> lines) : this(0, new List<int>(), "", 0, 0, 0, 0)
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

    public void ProcessTurn()
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
            itemValue /= 3;

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

public static class MathExtensions
{
    public static int Product(this IEnumerable<int> collection)
    {
        var product = 1;
        foreach (var num in collection)
        {
            product *= num;
        }
        return product;
    }
}