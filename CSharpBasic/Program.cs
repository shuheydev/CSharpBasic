using System;

namespace CSharpBasic
{
    class Program
    {
        public delegate bool Judgement(int value);

        static bool IsEven(int n)
        {
            return n % 2==0;
        }

        static void Main(string[] args)
        {
            var numbers = new[] { 5, 3, 9, 6, 7, 5, 8, 1, 0, 5, 10, 4 };

            var result = Count(numbers,IsEven);

            Console.WriteLine(result);
        }

        static int Count(int[] numbers,Judgement judge)
        {
            int count = 0;
            foreach(var n in numbers)
            {
                if(judge(n)==true)
                { count++; }
            }

            return count;
        }
    }
}
