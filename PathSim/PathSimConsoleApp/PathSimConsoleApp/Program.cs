using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathSimConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DataParser parser = new DataParser();

            parser.Parse();

            PathSimCalculator psc = new PathSimCalculator(parser.data);

            psc.CalculateAPCPA(7696);

            IList<Tuple<int, double>> topFiveResults = psc.GetTopFiveResults();

            foreach (Tuple<int, double> pair in topFiveResults)
                Console.WriteLine("Author Id: " + pair.Item1 + "\t  - " + pair.Item2);

            Console.ReadKey();
        }
    }
}
