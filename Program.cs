using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    public class Zenfolio
    {
        /// <summary>
        /// Use a hashmap to keep char count. This implementation can handle both special chars and case sensitivity.
        /// </summary>
        /// <param name="s">string s doesn't have blank char</param>
        private static void ProcessInputString(string s)
        {
            SortedDictionary<char, int> charCountMap = new SortedDictionary<char, int>();

            foreach (char c in s)
            {
                CountChar(charCountMap, c);
            }

            Console.WriteLine("output:");
            foreach (KeyValuePair<char, int> kv in charCountMap)
            {
                Console.WriteLine("{0}: {1}", kv.Key, kv.Value);
            }
        }

        private static void ProcessInputNumbers(decimal[] numbers)
        {
            decimal mean = numbers.Average();
            decimal median = FindMedian(numbers);
            decimal mode;

            bool uniqueMode = FindMode(numbers, out mode);
            decimal range = numbers.Max() - numbers.Min();

            Console.WriteLine("output:");
            Console.WriteLine("mean = {0:0.##}\nmedian = {1:0.##}\nmode = {2:0.##}\nrange = {3:0.##}", mean, median, uniqueMode ? mode.ToString() : "none", range);
        }

        public static decimal FindMedian(decimal[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
            {
                throw new ArgumentException();
            }

            List<decimal> orderedNumbers = numbers.OrderBy(x => x).ToList();
            int middleNumber = orderedNumbers.Count / 2;

            if (orderedNumbers.Count % 2 == 0)
            {
                return Decimal.Divide(orderedNumbers[middleNumber - 1] + orderedNumbers[middleNumber], 2);
            }

            return orderedNumbers[middleNumber];
        }

        /// <summary>
        /// Find the mode of the list of numbers. Returns "none" to indicate non-unique modes.
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="mode">Mode if method returns true</param>
        /// <returns></returns>
        public static bool FindMode(decimal[] numbers, out decimal mode)
        {
            if (numbers == null || numbers.Length == 0)
            {
                throw new ArgumentException();
            }

            var orderedGroups = numbers.GroupBy(x => x)
                .OrderByDescending(x => x.Count());

            if (numbers.Length > 1 && orderedGroups.ElementAt(0).Count() == orderedGroups.ElementAt(1).Count())
            {
                mode = 0;       // irrelevant value
                return false;
            }

            mode = orderedGroups.Select(x => x.Key).FirstOrDefault();
            return true;
        }

        public static void CountChar(SortedDictionary<char, int> charCountMap, char c)
        {
            if (charCountMap == null)
            {
                throw new ArgumentException();
            }

            if (!charCountMap.ContainsKey(c))
            {
                charCountMap.Add(c, 1);
            }
            else
            {
                charCountMap[c]++;
            }
        }

        public static void Main()
        {
            while (true)
            {
                Console.Write("input: ");
                string[] tokens = Console.ReadLine().Split(' ');

                if (tokens[0].Equals("quit", StringComparison.InvariantCultureIgnoreCase))
                {
                    break;
                }

                try
                {
                    // Use decimal data type and Convert.ToDecimal to handle decimal numbers too.
                    decimal[] numbers = new decimal[tokens.Length];
                    numbers = Array.ConvertAll(tokens, x => Convert.ToDecimal(x));

                    // Input is a bunch of numbers
                    ProcessInputNumbers(numbers);
                }
                catch
                {
                    // Input is a literal string
                    ProcessInputString(string.Join("", tokens));
                }
            }
        }
    }
}