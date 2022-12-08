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

    public int VisibleTreeCount()
    {
        var result = 0;
        for (int x = 0; x < RightEdge; x++)
        {
            for (int y = 0; y < BottomEdge; y++)
            {
                if (IsVisible(x, y)) result++;
            }
        }
        return result;
    }

    public int GetBestScenicScore()
    {
        var result = 0;
        for (int x = 1; x < RightEdge - 1; x++)
        {
            for (int y = 1; y < BottomEdge - 1; y++)
            {
                result = Math.Max(result, ScenicScore(x, y));
            }
        }
        return result;
    }

    private bool IsVisible(int x, int y)
    {
        if (OnEdge(x, y)) return true;
        return UpVisible(x, y) || DownVisible(x, y) || LeftVisible(x, y) || RightVisible(x, y);
    }

    private int ScenicScore(int x, int y)
    {
        if (OnEdge(x, y)) return 0;
        return UpScore(x, y) * DownScore(x, y) * LeftScore(x, y) * RightScore(x, y);
    }

    private bool OnEdge(int x, int y)
    {
        return x >= RightEdge || x == 0 || y >= BottomEdge || y == 0;
    }

    private bool UpVisible(int x, int y)
    {
        for (int i = y - 1; i >= 0; i--)
        {
            if (Grid[x, i] >= Grid[x, y]) return false;
        }
        return true;
    }
    private bool DownVisible(int x, int y)
    {
        for (int i = y + 1; i < BottomEdge; i++)
        {
            if (Grid[x, i] >= Grid[x, y]) return false;
        }
        return true;
    }
    private bool LeftVisible(int x, int y)
    {
        for (int i = x - 1; i >= 0; i--)
        {
            if (Grid[i, y] >= Grid[x, y]) return false;
        }
        return true;
    }
    private bool RightVisible(int x, int y)
    {
        for (int i = x + 1; i < RightEdge; i++)
        {
            if (Grid[i, y] >= Grid[x, y]) return false;
        }
        return true;
    }

    private int UpScore(int x, int y)
    {
        var score = 0;
        for (int i = y - 1; i >= 0; i--)
        {
            score++;
            if (Grid[x, i] >= Grid[x, y]) return score;
        }
        return score;
    }
    private int DownScore(int x, int y)
    {
        var score = 0;
        for (int i = y + 1; i < BottomEdge; i++)
        {
            score++;
            if (Grid[x, i] >= Grid[x, y]) return score;
        }
        return score;
    }
    private int LeftScore(int x, int y)
    {
        var score = 0;
        for (int i = x - 1; i >= 0; i--)
        {
            score++;
            if (Grid[i, y] >= Grid[x, y]) return score;
        }
        return score;
    }
    private int RightScore(int x, int y)
    {
        var score = 0;
        for (int i = x + 1; i < RightEdge; i++)
        {
            score++;
            if (Grid[i, y] >= Grid[x, y]) return score;
        }
        return score;
    }
}