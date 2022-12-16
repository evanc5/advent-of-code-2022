using System.Text.RegularExpressions;

Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var result = 0;
    var pipes = new Dictionary<string, Pipe>();
    foreach (var line in input)
    {
        var pipe = new Pipe(line);
        pipes[pipe.ID] = pipe;
    }
    foreach (var pipe in pipes.Values)
    {
        foreach (var tunnelID in pipe.TunnelIDs)
        {
            pipe.Tunnels.Add(pipes[tunnelID]);
        }
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

record class Pipe(string ID, int Flow, List<string> TunnelIDs, List<Pipe> Tunnels)
{
    public Pipe(string line) : this("", 0, new List<string>(), new List<Pipe>())
    {
        string pattern = @"Valve ([A-Z]{2}) has flow rate=([0-9]+); tunnels? leads? to valves? (.+)";
        var groups = Regex.Match(line, pattern).Groups;

        ID = groups[1].Value;
        Flow = int.Parse(groups[2].Value);
        TunnelIDs = groups[3].Value.Split(", ").ToList();
    }
}