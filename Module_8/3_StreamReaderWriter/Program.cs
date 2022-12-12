
// Write some strings in the file
var filePath = Path.Combine("data", "test.txt"); // Always use Path.Compbine to be platform agnostic!

using (var fileWriteStream = new FileStream(filePath, FileMode.Create))
{
    using (var streamWriter = new StreamWriter(fileWriteStream))
    {
        streamWriter.WriteLine("Hello");
        streamWriter.WriteLine("world"); // Use asyncs!

        streamWriter.Flush();
    }
}

// Read strings from the file

using var fileReadStream = new FileStream(filePath, FileMode.Open);

using var streamReader = new StreamReader(fileReadStream); // Can specify encoding

while (!streamReader.EndOfStream)
{
    Console.WriteLine(streamReader.ReadLine());
}

//Console.WriteLine(streamReader.ReadToEnd());
