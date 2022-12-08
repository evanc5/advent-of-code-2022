Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var forest = new Forest(input);
    var result = forest.VisibleTreeCount();
    Console.WriteLine($"Part 1: {result}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 1: {sw.Elapsed}");
}

static void Part2()
{
    var input = File.ReadAllLines(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var forest = new Forest(input);
    var result = forest.GetBestScenicScore();
    Console.WriteLine($"Part 2: {result}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 2: {sw.Elapsed}");
}

public class Forest
{
    public int[,] Grid { get; }

    public int RightEdge => Grid.GetLength(0);
    public int BottomEdge => Grid.GetLength(1);

    public Forest(string[] input)
    {
        Grid = new int[input[0].Count(), input.Count()];
        for (int i = 0; i < input.Count(); i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                Grid[i, j] = int.Parse(input[i][j].ToString());
            }
        }
    }

    public bool IsVisible(int x, int y)
    {
        if (x >= RightEdge || x == 0 || y >= BottomEdge || y == 0) return true;
        return UpVisible(x, y) || DownVisible(x, y) || LeftVisible(x, y) || RightVisible(x, y);
    }

    private bool RightVisible(int x, int y)
    {
        for (int i = x + 1; i < RightEdge; i++)
        {
            if (Grid[i, y] >= Grid[x, y])
            {
                return false;
            }
        }
        return true;
    }

    private bool LeftVisible(int x, int y)
    {
        for (int i = x - 1; i >= 0; i--)
        {
            if (Grid[i, y] >= Grid[x, y])
            {
                return false;
            }
        }
        return true;
    }

    private bool DownVisible(int x, int y)
    {
        for (int i = y + 1; i < BottomEdge; i++)
        {
            if (Grid[x, i] >= Grid[x, y])
            {
                return false;
            }
        }
        return true;
    }

    private bool UpVisible(int x, int y)
    {
        for (int i = y - 1; i >= 0; i--)
        {
            if (Grid[x, i] >= Grid[x, y])
            {
                return false;
            }
        }
        return true;
    }

    public int ScenicScore(int x, int y)
    {
        if (x >= RightEdge || x == 0 || y >= BottomEdge || y == 0) return 0;

        var upVisible = 0;
        for (int i = y - 1; i >= 0; i--)
        {
            upVisible++;
            if (Grid[x, i] >= Grid[x, y]) break;
        }
        var downVisible = 0;
        for (int i = y + 1; i < BottomEdge; i++)
        {
            downVisible++;
            if (Grid[x, i] >= Grid[x, y]) break;
        }
        var leftVisible = 0;
        for (int i = x - 1; i >= 0; i--)
        {
            leftVisible++;
            if (Grid[i, y] >= Grid[x, y]) break;
        }
        var rightVisible = 0;
        for (int i = x + 1; i < RightEdge; i++)
        {
            rightVisible++;
            if (Grid[i, y] >= Grid[x, y]) break;
        }
        return upVisible * downVisible * leftVisible * rightVisible;
    }

    public int VisibleTreeCount()
    {
        var result = 0;
        for (int x = 0; x < Grid.GetLength(0); x++)
        {
            for (int y = 0; y < Grid.GetLength(1); y++)
            {
                if (IsVisible(x, y)) result++;
            }
        }
        return result;
    }

    public int GetBestScenicScore()
    {
        var result = int.MinValue;
        for (int x = 0; x < Grid.GetLength(0); x++)
        {
            for (int y = 0; y < Grid.GetLength(1); y++)
            {
                result = Math.Max(result, ScenicScore(x, y));
            }
        }
        return result;
    }
}