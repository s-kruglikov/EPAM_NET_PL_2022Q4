// System.IO
// https://learn.microsoft.com/en-us/dotnet/api/system.io?view=net-7.0
// https://learn.microsoft.com/en-us/dotnet/api/system.io.stream?view=net-7.0
// https://www.bestprog.net/en/2021/03/10/c-hierarchy-of-streams-with-basic-storages-the-filestream-class/

var memoryStream = new MemoryStream();

var bytesArray = new byte[] { 22, 14, 8 };

memoryStream.Write(bytesArray);
memoryStream.WriteByte(2);

Console.WriteLine($"*** Current stream length {memoryStream.Length} ***");
Console.WriteLine($"*** Current stream position {memoryStream.Position} ***");

memoryStream.Seek(0, SeekOrigin.Begin);

var currentByte = memoryStream.ReadByte();
while(currentByte != -1)
{
    Console.WriteLine(currentByte);

    currentByte = memoryStream.ReadByte();
}

memoryStream.Close();