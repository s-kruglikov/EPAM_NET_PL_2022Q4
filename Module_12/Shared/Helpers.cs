using System;
namespace Shared
{
	public static class Helpers
	{
        public static void OutputStreamToConsole(Stream resultStream)
        {
            //Show results
            resultStream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(resultStream))
            {
                Console.WriteLine(reader.ReadToEnd());
            }
        }
    }
}

