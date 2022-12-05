Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt");
    var sw = System.Diagnostics.Stopwatch.StartNew();

    // var board = input.Take(8);
    var moves = input.TakeLast(501);
    var cargo = new Cargo();

    // foreach (var line in board)
    // {
    //     //build board object
    //     //idk how to do this actually so let's just do this manually for now I guess
    // }
    foreach (var line in moves)
    {
        cargo.ProcessMove(line);
    }

    Console.WriteLine($"Part 1: {cargo.GetTops()}");

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

public class Cargo
{
    Stack<char>[] Stacks;

    public Cargo()
    {
        Stacks = new Stack<char>[9];
        InitializeStacks();
    }

    private void InitializeStacks()
    {
        Stacks[0] = new Stack<char>();
        Stacks[1] = new Stack<char>();
        Stacks[2] = new Stack<char>();
        Stacks[3] = new Stack<char>();
        Stacks[4] = new Stack<char>();
        Stacks[5] = new Stack<char>();
        Stacks[6] = new Stack<char>();
        Stacks[7] = new Stack<char>();
        Stacks[8] = new Stack<char>();

        Stacks[0].Push('B');
        Stacks[0].Push('S');
        Stacks[0].Push('V');
        Stacks[0].Push('Z');
        Stacks[0].Push('G');
        Stacks[0].Push('P');
        Stacks[0].Push('W');

        Stacks[1].Push('J');
        Stacks[1].Push('V');
        Stacks[1].Push('B');
        Stacks[1].Push('C');
        Stacks[1].Push('Z');
        Stacks[1].Push('F');

        Stacks[2].Push('V');
        Stacks[2].Push('L');
        Stacks[2].Push('M');
        Stacks[2].Push('H');
        Stacks[2].Push('N');
        Stacks[2].Push('Z');
        Stacks[2].Push('D');
        Stacks[2].Push('C');

        Stacks[3].Push('L');
        Stacks[3].Push('D');
        Stacks[3].Push('M');
        Stacks[3].Push('Z');
        Stacks[3].Push('P');
        Stacks[3].Push('F');
        Stacks[3].Push('J');
        Stacks[3].Push('B');

        Stacks[4].Push('V');
        Stacks[4].Push('F');
        Stacks[4].Push('C');
        Stacks[4].Push('G');
        Stacks[4].Push('J');
        Stacks[4].Push('B');
        Stacks[4].Push('Q');
        Stacks[4].Push('H');

        Stacks[5].Push('G');
        Stacks[5].Push('F');
        Stacks[5].Push('Q');
        Stacks[5].Push('T');
        Stacks[5].Push('S');
        Stacks[5].Push('L');
        Stacks[5].Push('B');

        Stacks[6].Push('L');
        Stacks[6].Push('G');
        Stacks[6].Push('C');
        Stacks[6].Push('Z');
        Stacks[6].Push('V');

        Stacks[7].Push('N');
        Stacks[7].Push('L');
        Stacks[7].Push('G');

        Stacks[8].Push('J');
        Stacks[8].Push('F');
        Stacks[8].Push('H');
        Stacks[8].Push('C');
    }

    public void ProcessMove(string move)
    {
        var split = move.Split(' ');
        var count = int.Parse(split[1]);
        var from = int.Parse(split[3]) - 1;
        var to = int.Parse(split[5]) - 1;

        ProcessMove(count, from, to);
    }

    public void ProcessMove(int count, int from, int to)
    {
        while (count > 0)
        {
            Stacks[to].Push(Stacks[from].Pop());
            count--;
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