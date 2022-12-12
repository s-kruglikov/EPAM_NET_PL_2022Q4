// https://learn.microsoft.com/en-us/dotnet/api/system.io.directory?view=net-7.0
// https://learn.microsoft.com/en-us/dotnet/api/system.io.fileinfo?view=net-7.0

var currentDirectoryName = "current";
var archiveDirectory = "archive";

if (Directory.Exists(currentDirectoryName))
{
    Console.WriteLine("Directory exists!");

    // create archive directory
    Directory.CreateDirectory(archiveDirectory);

    // Go over current directory content
    var directoryItems = Directory.GetFiles(currentDirectoryName, "*.txt", SearchOption.AllDirectories);

    // See file info
    foreach (var item in directoryItems)
    {
        var fileInfo = new FileInfo(item);

        Console.WriteLine($"File name: {fileInfo.Name}, size in bytes: {fileInfo.Length}");

        // copy file to the archive directory
        fileInfo.CopyTo(Path.Combine(archiveDirectory, $"archive_{fileInfo.Name}"));

        // delete original file
        //fileInfo.Delete();
    }


    // copy all files to the archive
    // Directory.Move(currentDirectoryName, archiveDirectory);
}



// You can recursively go over directory content and nested directories.
static IEnumerable<string> GetDirectoryItemsRecursively(string path)
{
    var files = new List<string>();

    files.AddRange(Directory.GetFiles(path));

    foreach (var item in Directory.GetDirectories(path)) // searchPattern and SearchOption
    {
        files.AddRange(GetDirectoryItemsRecursively(item));
    }

    return files;
}