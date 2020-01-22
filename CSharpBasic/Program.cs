using System;

namespace CSharpBasic
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = new[] { 5, 3, 9, 6, 7, 5, 8, 1, 0, 5, 10, 4 };

            var result = Count(numbers, n => n % 2 == 0);

            Console.WriteLine(result);
        }

        static int Count(int[] numbers, Func<int,bool> judge)
        {
            int count = 0;
            foreach (var n in numbers)
            {
                if (judge(n) == true)
                { count++; }
            }

            return count;
        }
    }
}
