Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var board = input.Take(8);
    var moves = input.TakeLast(501);
    var cargo = new Cargo(board);

    foreach (var line in moves)
    {
        cargo.ProcessMove(line, 1);
    }

    Console.WriteLine($"Part 1: {cargo.GetTops()}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 1: {sw.Elapsed}");
}

static void Part2()
{
    var input = File.ReadAllLines(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var board = input.Take(8);
    var moves = input.TakeLast(501);
    var cargo = new Cargo(board);

    foreach (var line in moves)
    {
        cargo.ProcessMove(line, 2);
    }

    Console.WriteLine($"Part 2: {cargo.GetTops()}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 2: {sw.Elapsed}");
}

public class Cargo
{
    Stack<char>[] Stacks;

    public Cargo()
    {
        Stacks = new Stack<char>[9];
        Stacks[0] = new Stack<char>();
        Stacks[1] = new Stack<char>();
        Stacks[2] = new Stack<char>();
        Stacks[3] = new Stack<char>();
        Stacks[4] = new Stack<char>();
        Stacks[5] = new Stack<char>();
        Stacks[6] = new Stack<char>();
        Stacks[7] = new Stack<char>();
        Stacks[8] = new Stack<char>();
    }

    public Cargo(IEnumerable<string> board) : this()
    {
        int[] columns = { 1, 5, 9, 13, 17, 21, 25, 29, 33 };
        foreach (var row in board.Reverse())
        {
            for (int i = 0; i < columns.Length; i++)
            {
                var column = columns[i];
                var entry = row[column];
                if (char.IsLetter(entry))
                {
                    Stacks[i].Push(entry);
                }
            }
        }
    }

    public void ProcessMove(string move, int version)
    {
        var split = move.Split(' ');
        var count = int.Parse(split[1]);
        var from = int.Parse(split[3]) - 1;
        var to = int.Parse(split[5]) - 1;

        switch (version)
        {
            case 1:
                ProcessMove1(count, from, to);
                break;
            case 2:
                ProcessMove2(count, from, to);
                break;
        }
    }

    public void ProcessMove1(int count, int from, int to)
    {
        while (count > 0)
        {
            Stacks[to].Push(Stacks[from].Pop());
            count--;
        }
    }

    public void ProcessMove2(int count, int from, int to)
    {
        var top = Stacks[from].GrabTop(count);
        foreach (var item in top)
        {
            Stacks[to].Push(item);
        }
    }

    public string GetTops()
    {
        var result = string.Empty;
        foreach (var stack in Stacks)
        {
            result += stack.Peek();
        }
        return result;
    }
}

public static class StackExtensions
{
    public static IEnumerable<T> GrabTop<T>(this Stack<T> stack, int count)
    {
        var result = new T[count];
        for (int i = 0; i < count; i++)
        {
            result[i] = stack.Pop();
        }
        return result.Reverse();
    }
}