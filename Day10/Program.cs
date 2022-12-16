var input = File.ReadAllLines(@".\input.txt");
var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

var computer = new Computer();
Console.WriteLine("Part 2:");
foreach (var line in input)
{
    computer.Tick(line);
}
var part1 = computer.SignalStrengthHistory[20] +
                computer.SignalStrengthHistory[60] +
                computer.SignalStrengthHistory[100] +
                computer.SignalStrengthHistory[140] +
                computer.SignalStrengthHistory[180] +
                computer.SignalStrengthHistory[220];

var elapsedTime = System.Diagnostics.Stopwatch.GetElapsedTime(startTime);
Console.WriteLine($"Part 1: {part1}");
System.Diagnostics.Debug.WriteLine($"Part 1 + 2: {elapsedTime}");

public class Computer
{
    private int _currentCycle = 1;
    private int _xRegister = 1;
    private int _scanX;
    private Dictionary<int, int> _futureAdds = new Dictionary<int, int>();
    private int SignalStrength => _currentCycle * _xRegister;

    public Dictionary<int, int> SignalStrengthHistory { get; } = new Dictionary<int, int>();

    public void Tick(string line)
    {
        var cycles = ProcessInstruction(line);
        for (int i = 0; i < cycles; i++) Scan();
        if (_futureAdds.TryGetValue(_currentCycle, out var v)) _xRegister += v;
        SignalStrengthHistory[_currentCycle] = SignalStrength;
    }

    private void Scan()
    {
        if (Math.Abs(_xRegister - _scanX) <= 1)
        {
            Console.Write('#');
        }
        else
        {
            Console.Write(' ');
        }
        _scanX++;
        if (_scanX > 39)
        {
            Console.WriteLine();
            _scanX = 0;
        }
    }

    private int ProcessInstruction(string instruction)
    {
        var split = instruction.Split(' ');
        switch (split[0])
        {
            case "addx":
                _currentCycle += 2;
                _futureAdds[_currentCycle] = int.Parse(split[1]);
                return 2;
            case "noop":
                _currentCycle++;
                return 1;
        }
        return 0;
    }
}