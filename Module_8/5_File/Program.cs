// https://learn.microsoft.com/en-us/dotnet/api/system.io.file?view=net-7.0

var filePath = "test.txt";
var copyFilePath = "copy_test.txt";

using var fileWriteStream = File.Create(filePath);

var streamWriter = new StreamWriter(fileWriteStream);

streamWriter.Write("Hello!");
streamWriter.Flush();
streamWriter.Close();

// We can do smth with file
File.Copy(filePath, copyFilePath);
//File.Delete(filePath);

using var streamReader = File.OpenText(copyFilePath);

Console.WriteLine(streamReader.ReadToEnd());