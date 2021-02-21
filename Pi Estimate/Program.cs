using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_Estimate
{
    class Program
    {


        static int calc()
        {
            Random r = new Random((int)DateTime.Now.Ticks);
            int count = 0;
            int max = int.MaxValue - 1;
            for(int i = 0; i < 10000000; i++)
            {
                 uint a = (uint)((long)r.Next(int.MinValue, max) - int.MinValue + 1);
                 uint b = (uint)((long)r.Next(int.MinValue, max) - int.MinValue + 1);
                if (GCD(a, b) == 1)
                {
                    count++;
                }
            }
            //Console.Write("C:{0} ", count);
            return count;

        }
        static void Main(string[] args)
        {

            Console.WriteLine("Pi Estimate");


            Int64 max = Int64.MaxValue;
            Int64 count = 0;
            for(UInt64 i = 0; i < Int64.MaxValue; i+= 160000000)
            {
                if (i != 0)
                {
                    double estimate = Math.Sqrt(6 / ((double)count / (double)(1 + i)));
                    //Console.WriteLine();
                    Console.WriteLine("Max: {0} I: {3} Count: {1} Estimate: {2}", max, count, estimate, i);
                }
                count += calc();
                var tasks = Enumerable.Range(0, 15).Select(i => Task.Run(() => { return calc(); })).ToArray();
                Task.WaitAll(tasks);
                var res = tasks.Select(t => t.Result).ToList();

                foreach(int c in res)
                {
                    count += c;
                }

            }
            Console.WriteLine("Done");
        }

        private static uint GCD(uint a, uint b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }
    }
}
