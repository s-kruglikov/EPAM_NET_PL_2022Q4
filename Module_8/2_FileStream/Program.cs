// https://www.softwaretestinghelp.com/c-sharp/csharp-filestream-streamwriter-streamreader/
// https://learn.microsoft.com/en-us/dotnet/api/system.io.filestream.flush?view=net-7.0
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-statement

// Write some bytes in file

var filePath = Path.Combine("data", "test.txt"); // Always use Path.Combine to be platform agnostic!

var fileStream = new FileStream(filePath, FileMode.Create);

var bytes = new byte[] { 72, 101, 108, 108, 111 }; // Hello https://www.charset.org/utf-8

fileStream.Write(bytes);

fileStream.Flush();
fileStream.Close();

// Read some bytes from file

using (var readFileStream = new FileStream(filePath, FileMode.Open)) // Using is equal to readFileStream.Close()
{
    var currentByte = readFileStream.ReadByte();
    while (currentByte != -1)
    {
        Console.WriteLine(currentByte);

        currentByte = readFileStream.ReadByte();
    }
}