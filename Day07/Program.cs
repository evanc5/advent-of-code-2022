Part1();
Part2();

static void Part1()
{
    var input = File.ReadAllLines(@".\input.txt").Skip(1);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var result = 0;
    var fileSystem = new FileSystem();
    foreach (var line in input)
    {
        var split = line.Split(' ');
        switch (split[0])
        {
            case "$":
                if (split[1] == "cd")
                {
                    fileSystem.cd(split[2]);
                }
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
    result = fileSystem.FlatDirectories.Where(d => d.Size <= 100000).Sum(d => d.Size);
    Console.WriteLine($"Part 1: {result}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 1: {sw.Elapsed}");
}

static void Part2()
{
    var input = File.ReadAllLines(@".\input.txt").Skip(1);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var result = 0;
    foreach (var line in input)
    {

    }
    Console.WriteLine($"Part 2: {result}");

    sw.Stop();
    System.Diagnostics.Debug.WriteLine($"Part 2: {sw.Elapsed}");
}

class FileSystem
{
    public Directory CurrentDirectory { get; private set; }
    public string CurrentPath => CurrentDirectory.Path;
    public Dictionary<string, int> CurrentFiles => CurrentDirectory.Files;
    public List<Directory> CurrentSubdirectories => CurrentDirectory.Subdirectories;
    public IEnumerable<string> CurrentSubdirectoryNames => CurrentSubdirectories.Select(d => d.Path);
    public Directory? ParentDirectory => CurrentDirectory.Parent;

    public List<Directory> FlatDirectories { get; }

    public FileSystem()
    {
        CurrentDirectory = new Directory("/");
        FlatDirectories = new List<Directory>();
    }

    public void cd(string path)
    {
        if (path == "..")
        {
            if (ParentDirectory == null) throw new NullReferenceException(nameof(ParentDirectory));
            CurrentDirectory = ParentDirectory;
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

    public void ls(IEnumerable<string> entries)
    {

    }
}

class Directory
{
    public string Path { get; }
    public Dictionary<string, int> Files { get; }
    public List<Directory> Subdirectories { get; }
    public Directory? Parent { get; }

    public int Size
    {
        get
        {
            var result = 0;
            foreach (var fileSize in Files.Values)
            {
                result += fileSize;
            }
            foreach (var subdirectory in Subdirectories)
            {
                result += subdirectory.Size;
            }
            return result;
        }
    }

    public Directory(string path)
    {
        Path = path;
        Files = new Dictionary<string, int>();
        Subdirectories = new List<Directory>();
    }

    public Directory(string path, Directory parent) : this(path)
    {
        Parent = parent;
    }
}