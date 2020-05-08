using System;

namespace FitnessGCProjectMid
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome. Please select an option:");
            Console.WriteLine($"1. Check in to a club");
            Console.WriteLine($"2. Add member");
            Console.WriteLine($"3. Remove member");
            Console.WriteLine($"4. Display member information");
            int input = int.Parse(Console.ReadLine().Trim());

        }
    }
}
