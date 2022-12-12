Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var rope = new Rope();
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

        var xDirection = Math.Sign(dx);
        var yDirection = Math.Sign(dy);

        if (Math.Abs(dx) < 2 && Math.Abs(dy) < 2) return;

        //determine where Tail needs to move
    }
}

public record struct Point<T>(T x, T y);