using System;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = string.Empty;

            while (input != "exit")
            {
                Console.Write("Enter your string: ");
                input = Console.ReadLine();
                try
                {
                    Console.WriteLine($"First character is \'{input[0]}\'");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Input string was empty!");
                }
                catch
                {
                    Console.WriteLine("Unknown error.");
                }
            }
        }
    }
}