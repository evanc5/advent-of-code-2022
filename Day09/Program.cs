Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var result = ProcessInput(input, 2);

    var elapsedTime = System.Diagnostics.Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine($"Part 1: {result}");
    System.Diagnostics.Debug.WriteLine($"Part 1: {elapsedTime}");
}

static void Part2()
{
    var input = File.ReadAllLines(@".\input.txt");
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var result = ProcessInput(input, 10);

    var elapsedTime = System.Diagnostics.Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine($"Part 2: {result}");
    System.Diagnostics.Debug.WriteLine($"Part 2: {elapsedTime}");
}

static int ProcessInput(IEnumerable<string> input, int count)
{
    var ropeSim = new RopeSimulator(count);
    foreach (var line in input)
    {
        ropeSim.ProcessMove(line);
    }
    return ropeSim.Visited.Count;
}

public class RopeSimulator
{
    public Point<int>[] Rope { get; private set; }

    public Point<int> Tail => Rope[Rope.Length - 1];

    public HashSet<Point<int>> Visited { get; } = new HashSet<Point<int>>();

    public RopeSimulator(int count)
    {
        Rope = new Point<int>[count];
        Visited.Add(new Point<int>(0, 0));
    }

    public void ProcessMove(string line)
    {
        var split = line.Split(' ');
        var direction = split[0];
        var amount = int.Parse(split[1]);
        for (int i = amount; i > 0; i--)
        {
            switch (direction)
            {
                case "U":
                    Rope[0] = new Point<int>(Rope[0].x, Rope[0].y + 1);
                    break;
                case "D":
                    Rope[0] = new Point<int>(Rope[0].x, Rope[0].y - 1);
                    break;
                case "L":
                    Rope[0] = new Point<int>(Rope[0].x - 1, Rope[0].y);
                    break;
                case "R":
                    Rope[0] = new Point<int>(Rope[0].x + 1, Rope[0].y);
                    break;
            }
            UpdateSegment(1);
            Visited.Add(Tail);
        }
    }

    private void UpdateSegment(int i)
    {
        if (i >= Rope.Length) return;

        var hx = Rope[i - 1].x;
        var hy = Rope[i - 1].y;

        var tx = Rope[i].x;
        var ty = Rope[i].y;

        var dx = hx - tx;
        var dy = hy - ty;

        var xDirection = Math.Sign(dx);
        var yDirection = Math.Sign(dy);

        var xAbs = Math.Abs(dx);
        var yAbs = Math.Abs(dy);

        if (xAbs > 1 || yAbs > 1)
        {
            tx += xDirection;
            ty += yDirection;
        }

        Rope[i] = new Point<int>(tx, ty);

        UpdateSegment(++i);
    }
}

public record struct Point<T>(T x, T y);