Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var computer = new Computer();
    foreach (var line in input)
    {
        computer.Tick(line);
    }
    var result = computer.SignalStrengthHistory[20] +
                 computer.SignalStrengthHistory[60] +
                 computer.SignalStrengthHistory[100] +
                 computer.SignalStrengthHistory[140] +
                 computer.SignalStrengthHistory[180] +
                 computer.SignalStrengthHistory[220];

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

public class Computer
{
    public int CurrentCycle { get; private set; } = 1;
    public int XRegister { get; private set; } = 1;
    public int SignalStrength => CurrentCycle * XRegister;
    public Dictionary<int, int> FutureAdds { get; } = new Dictionary<int, int>();
    public Dictionary<int, int> SignalStrengthHistory { get; } = new Dictionary<int, int>();

    public void Tick(string line = "")
    {
        ProcessInstruction(line);
        if (FutureAdds.TryGetValue(CurrentCycle, out var v)) XRegister += v;
        SignalStrengthHistory[CurrentCycle] = SignalStrength;
    }

    public void ProcessInstruction(string instruction)
    {
        var split = instruction.Split(' ');
        switch (split[0])
        {
            case "addx":
                CurrentCycle += 2;
                FutureAdds[CurrentCycle] = int.Parse(split[1]);
                break;
            case "noop":
                CurrentCycle++;
                break;
        }
    }
}