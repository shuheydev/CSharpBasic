using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBasic
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var numbers = new[] { 5, 3, 9, 6, 7, 5, 8, 1, 0, 5, 10, 4 };

            var result = Count(numbers, n => n % 2 == 0);
            Console.WriteLine(result);

            result = Count(numbers, n => n == 5);
            Console.WriteLine(result);

            var list = numbers.ToList();
            list.ConvertAll(n => n * 2).ForEach(n => Console.WriteLine(n));

            Array.Reverse(numbers);
            numbers.ToList().ConvertAll(n => n * 2).ForEach(n => Console.WriteLine(n));

            var str = "this";
            Console.WriteLine(str.MyReverse());
            Console.WriteLine(string.Join("", str.Reverse()));

            var number2 = new List<int> { 12, 87, 94, 14, 53, 20, 40, 35, 76, 91, 31, 17, 48 };

            //1.
            Console.WriteLine(number2.Exists(x => x % 8 == 0 || x % 9 == 0));

            //2.
            number2.ForEach(x => Console.WriteLine(x / 2.0));

            //3.
            number2.Where(x => x > 50).ToList().ForEach(x => Console.WriteLine(x));

            //4.
            var numbers3 = number2.Select(x => x * 2).ToList();
            numbers3.ForEach(x => Console.WriteLine(x));

            var names = new List<string> { "Tokyo", "New Delhi", "Bangkok", "London", "Paris", "Berlin", "Canberra", "Hong Kong" };

            var inputCity = Console.ReadLine();
            Console.WriteLine(names.IndexOf(inputCity));//なかったら-1がかえる

            Console.WriteLine(names.Where(x => x.Contains("o")).Count());

            var names2 = names.Where(x => x.Contains("o")).ToList();
            names2.ForEach(x => Console.WriteLine(x));

            names.Where(x => x.StartsWith("B"))
                 .Select(x => x.Length)
                 .ToList()
                 .ForEach(x => Console.WriteLine(x));

            YearMonth[] yearMonths = new YearMonth[]
            {
                new YearMonth{Year=2001,Month=5},
                new YearMonth{Year=2003,Month=12},
                new YearMonth{Year=2019,Month=3},
                new YearMonth{Year=2020,Month=8},
                new YearMonth{Year=1979,Month=6},
            };

            yearMonths.ToList().ForEach(x => Console.WriteLine(x.ToString()));

            var century21 = First21Century(yearMonths);
            Console.WriteLine(century21 != null ? century21.Year.ToString() : "21世紀のデータはありません");

            var addOneMonths = yearMonths.Select(x => x.AddOneMonth());
            addOneMonths.ToList().ForEach(x => Console.WriteLine(x.ToString()));

            int a = 12, b = 5;
            Console.WriteLine($"第{a,2}条{b,2}項");

            if (int.TryParse(Console.ReadLine(), out int inputInt) == true)
            {
                var commmaValue = $"{inputInt:#,0}";
                Console.WriteLine(commmaValue);
            }

            string str2 = "Jackdaws love my big sphinx of quarts";

            Console.WriteLine(str2.Count(x => x == ' '));

            str2 = str2.Replace(" big ", " small ");
            Console.WriteLine(str2);

            Console.WriteLine(str2.Split(" ").Length);

            str2.Split(" ").Where(x => x.Length <= 4).ToList().ForEach(x => Console.WriteLine(x));


            var splitted = str2.Split(" ");
            StringBuilder sb = new StringBuilder();

            splitted.ToList().ForEach(x => sb.Append($"{x} "));

            Console.WriteLine(sb.ToString().TrimEnd());

            var input2 = "Novelist=谷崎潤一郎;BestWork=春琴抄;Born=1886";

            string novelistMark = "Novelist=";
            int novelistStart = input2.IndexOf(novelistMark) + novelistMark.Length;
            int novelistEnd = input2.IndexOf(";", novelistStart);
            string novelist = input2.Substring(novelistStart, novelistEnd - novelistStart);
            Console.WriteLine($"作家 : {novelist}");

            string bestworkMark = "BestWork=";
            int bestworkStart = input2.IndexOf(bestworkMark) + bestworkMark.Length;
            int bestworkEnd = input2.IndexOf(";", bestworkStart);
            string bestwork = input2.Substring(bestworkStart, bestworkEnd - bestworkStart);
            Console.WriteLine($"代表作: {bestwork}");

            for (int i = 0; i < 10; i++)
                yearMonths.AsParallel().ForAll(x => Console.WriteLine(x.ToString()));

            var sw = Stopwatch.StartNew();

            
            var task1 =  Task.Run(()=> {
               return GetPrimeAt5000();
            });

            var task2 =  Task.Run(()=> {
                return GetPrimeAt6000();
            });

            var prime1 = await task1;
            var prime2 = await task2;

            Console.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();

            var primes= await Task.WhenAll(new Task<int>[] { task1, task2 });
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
        static int GetPrimeAt6000()
        {
            return GetPrimes().Skip(5999).First();
        }

        static int GetPrimeAt5000()
        {
            return GetPrimes().Skip(4999).First();
        }

        static IEnumerable<int> GetPrimes()
        {
            for (int i = 2; i < int.MaxValue; i++)
            {
                bool isPrime = true;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    yield return i;
                }
            }
        }

        static YearMonth First21Century(YearMonth[] yearMonths)
        {
            foreach (var ym in yearMonths)
            {
                if (ym.Is21Century)
                    return ym;
            }

            return null;
        }

        static int Count(int[] numbers, Func<int, bool> judge)
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


    //stringの拡張メソッドを作成する
    static class StringExtensions
    {
        public static string MyReverse(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            var chars = str.ToCharArray();

            Array.Reverse(chars);

            return new string(chars);
        }
    }

    class YearMonth
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public bool Is21Century { get => this.Year >= 2001 && this.Year <= 2100; }

        public YearMonth AddOneMonth()
        {
            return new YearMonth
            {
                Month = this.Month == 12 ? 1 : this.Month + 1,
                Year = this.Month == 12 ? this.Year + 1 : this.Year,
            };
        }

        public override string ToString()
        {
            return $"{this.Year}年{this.Month}月";
        }
    }
}
