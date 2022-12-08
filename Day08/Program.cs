Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var result = 0;
    var forest = ForestExtensions.BuildForest(input.ToArray());
    for (int x = 0; x < input.Count(); x++)
    {
        for (int y = 0; y < input[x].Length; y++)
        {
            if (forest.IsVisible(x, y)) result++;
        }
    }
    Console.WriteLine($"Part 1: {result}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 1: {sw.Elapsed}");
}

static void Part2()
{
    var input = File.ReadAllLines(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var result = int.MinValue;
    var forest = ForestExtensions.BuildForest(input.ToArray());
    for (int x = 0; x < input.Count(); x++)
    {
        for (int y = 0; y < input[x].Length; y++)
        {
            result = Math.Max(result, forest.ScenicScore(x, y));
        }
    }
    Console.WriteLine($"Part 2: {result}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 2: {sw.Elapsed}");
}

public static class ForestExtensions
{
    public static int[,] BuildForest(string[] input)
    {
        var forest = new int[input[0].Count(), input.Count()];
        for (int i = 0; i < input.Count(); i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                forest[i, j] = int.Parse(input[i][j].ToString());
            }
        }
        return forest;
    }

    public static bool IsVisible(this int[,] forest, int x, int y)
    {
        var rightEdge = forest.GetLength(0);
        var bottomEdge = forest.GetLength(1);

        if (x >= rightEdge || x == 0 || y >= bottomEdge || y == 0) return true;

        var upVisible = true;
        for (int i = y - 1; i >= 0; i--)
        {
            if (forest[x, i] >= forest[x, y])
            {
                upVisible = false;
                break;
            }
        }
        var downVisible = true;
        for (int i = y + 1; i < bottomEdge; i++)
        {
            if (forest[x, i] >= forest[x, y])
            {
                downVisible = false;
                break;
            }
        }
        var leftVisible = true;
        for (int i = x - 1; i >= 0; i--)
        {
            if (forest[i, y] >= forest[x, y])
            {
                leftVisible = false;
                break;
            }
        }
        var rightVisible = true;
        for (int i = x + 1; i < rightEdge; i++)
        {
            if (forest[i, y] >= forest[x, y])
            {
                rightVisible = false;
                break;
            }
        }
        return upVisible || downVisible || leftVisible || rightVisible;
    }

    public static int ScenicScore(this int[,] forest, int x, int y)
    {
        var rightEdge = forest.GetLength(0);
        var bottomEdge = forest.GetLength(1);

        if (x >= rightEdge || x == 0 || y >= bottomEdge || y == 0) return 0;

        var upVisible = 0;
        for (int i = y - 1; i >= 0; i--)
        {
            upVisible++;
            if (forest[x, i] >= forest[x, y]) break;
        }
        var downVisible = 0;
        for (int i = y + 1; i < bottomEdge; i++)
        {
            downVisible++;
            if (forest[x, i] >= forest[x, y]) break;
        }
        var leftVisible = 0;
        for (int i = x - 1; i >= 0; i--)
        {
            leftVisible++;
            if (forest[i, y] >= forest[x, y]) break;
        }
        var rightVisible = 0;
        for (int i = x + 1; i < rightEdge; i++)
        {
            rightVisible++;
            if (forest[i, y] >= forest[x, y]) break;
        }
        return upVisible * downVisible * leftVisible * rightVisible;
    }
}