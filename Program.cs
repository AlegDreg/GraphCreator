using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Program
    {
        static void Main(string[] args)
        {
            int dl = 100;
            int[] vs = new int[dl];
            Random random = new Random();

            for (int i = 0; i < dl; i++)
            {
                vs[i] = i * 10 + random.Next(-1000, 1000);
            }

            Graph graph = new Graph(num: vs, color: System.Drawing.Color.Blue, System.Drawing.Color.Red, System.Drawing.Color.Green);

            if (graph.Create())
                Console.WriteLine(graph.GetGraphPath());
            else
                Console.WriteLine("Error - " + graph.Excep.Message);

            Console.ReadKey();
        }
    }
}
