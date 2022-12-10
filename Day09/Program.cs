Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var rope = new Rope();
    foreach (var line in input)
    {
        rope.ProcessMove(line);
    }
    var result = rope.Visited.Count;
    Console.WriteLine($"Part 1: {result}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 1: {sw.Elapsed}");
}

static void Part2()
{
    var input = File.ReadAllLines(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var result = 0;
    foreach (var line in input)
    {

    }
    Console.WriteLine($"Part 2: {result}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 2: {sw.Elapsed}");
}

public class Rope
{
    public Point<int> Head { get; private set; } = new Point<int>(0, 0);
    public Point<int> Tail { get; private set; } = new Point<int>(0, 0);
    public HashSet<Point<int>> Visited { get; } = new HashSet<Point<int>>();

    public Rope()
    {
        Visited.Add(Head);
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
            Visited.Add(Head);
            Visited.Add(Tail);
        }
    }

    private void UpdateTail()
    {
        var dx = Head.x - Tail.x;
        var dy = Head.y - Tail.y;

        if (Math.Abs(dx) < 2 && Math.Abs(dy) < 2) return;

        if (dy == 0 && dx >= 2)
        {
            Tail = new Point<int>(Tail.x + dx - 1, Tail.y);
        }
        else if (dy == 0 && dx <= 2)
        {
            Tail = new Point<int>(Tail.x + dx + 1, Tail.y);
        }
        else if (dx == 0 && dy >= 2)
        {
            Tail = new Point<int>(Tail.x, Tail.y + dy - 1);
        }
        else if (dx == 0 && dy <= 2)
        {
            Tail = new Point<int>(Tail.x, Tail.y + dy + 1);
        }
    }
}

public record struct Point<T>(T x, T y);