Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt").Skip(1);
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var fileSystem = BuildFileSystem(input);
    var result = fileSystem.FlatDirectories.Where(d => d.Size <= 100000).Sum(d => d.Size);

    var elapsedTime = System.Diagnostics.Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine($"Part 1: {result}");
    System.Diagnostics.Debug.WriteLine($"Part 1: {elapsedTime}");
}

static void Part2()
{
    var input = File.ReadAllLines(@".\input.txt").Skip(1);
    var startTime = System.Diagnostics.Stopwatch.GetTimestamp();

    var fileSystem = BuildFileSystem(input);
    var result = fileSystem.FlatDirectories.Where(d => d.Size >= fileSystem.SpaceToFree).Min(d => d.Size);

    var elapsedTime = System.Diagnostics.Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine($"Part 2: {result}");
    System.Diagnostics.Debug.WriteLine($"Part 2: {elapsedTime}");
}

static FileSystem BuildFileSystem(IEnumerable<string> input)
{
    var fileSystem = new FileSystem();
    foreach (var line in input)
    {
        var split = line.Split(' ');
        switch (split[0])
        {
            case "$":
                if (split[1] == "cd") fileSystem.cd(split[2]);
                break;
            case "dir":
                var newSubdirectory = new Directory(split[1], fileSystem.CurrentDirectory);
                fileSystem.CurrentSubdirectories.Add(newSubdirectory);
                fileSystem.FlatDirectories.Add(newSubdirectory);
                break;
            default:
                fileSystem.CurrentFiles.Add(split[1], int.Parse(split[0]));
                break;
        }
    }
    return fileSystem;
}

class FileSystem
{
    public List<Directory> FlatDirectories { get; }

    public Directory CurrentDirectory { get; private set; }
    public Dictionary<string, int> CurrentFiles => CurrentDirectory.Files;
    public List<Directory> CurrentSubdirectories => CurrentDirectory.Subdirectories;
    public IEnumerable<string> CurrentSubdirectoryNames => CurrentSubdirectories.Select(d => d.Path);
    public Directory? ParentDirectory => CurrentDirectory.Parent;

    public int TotalSpace => 70000000;
    public int RequiredSpace => 30000000;
    public int UsedSpace => FlatDirectories[0].Size;
    public int AvailableSpace => TotalSpace - UsedSpace;
    public int SpaceToFree => RequiredSpace - AvailableSpace;

    public FileSystem()
    {
        CurrentDirectory = new Directory("/");
        FlatDirectories = new List<Directory> { CurrentDirectory };
    }

    public void cd(string path)
    {
        if (path == "..")
        {
            CurrentDirectory = ParentDirectory ?? CurrentDirectory;
        }
        else if (CurrentSubdirectoryNames.Contains(path))
        {
            CurrentDirectory = CurrentSubdirectories.First(d => d.Path == path);
        }
        else
        {
            CurrentDirectory = new Directory(path, CurrentDirectory);
            FlatDirectories.Add(CurrentDirectory);
        }
    }
}

record class Directory(string Path, Directory? Parent = null)
{
    public Dictionary<string, int> Files { get; } = new Dictionary<string, int>();
    public List<Directory> Subdirectories { get; } = new List<Directory>();
    public int Size => Files.Values.Sum() + Subdirectories.Sum(d => d.Size);
}