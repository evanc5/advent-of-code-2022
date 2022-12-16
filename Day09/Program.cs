Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var rope = new RopeSimulator(2);
    foreach (var line in input)
    {
        rope.ProcessMove(line);
    }
    var result = rope.Visited.Count;

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

public class RopeSimulator
{
    public Point<int>[] Rope { get; private set; }

    public Point<int> Head
    {
        get { return Rope[0]; }
        private set { Rope[0] = value; }
    }
    public Point<int> Tail
    {
        get { return Rope[Rope.Length - 1]; }
        private set { Rope[Rope.Length - 1] = value; }
    }

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
                    Head = new Point<int>(Head.x, Head.y + 1);
                    break;
                case "D":
                    Head = new Point<int>(Head.x, Head.y - 1);
                    break;
                case "L":
                    Head = new Point<int>(Head.x - 1, Head.y);
                    break;
                case "R":
                    Head = new Point<int>(Head.x + 1, Head.y);
                    break;
            }
            UpdateTail();
            Visited.Add(Tail);
        }
    }

    private void UpdateTail()
    {
        var tx = Tail.x;
        var ty = Tail.y;

        var dx = Head.x - tx;
        var dy = Head.y - ty;

        var xDirection = Math.Sign(dx);
        var yDirection = Math.Sign(dy);

        var xAbs = Math.Abs(dx);
        var yAbs = Math.Abs(dy);

        if (xAbs < 2 && yAbs < 2) return;

        if (xAbs >= 2 || yAbs >= 2)
        {
            tx += xDirection;
            ty += yDirection;
        }

        Tail = new Point<int>(tx, ty);
    }
}

public record struct Point<T>(T x, T y);