using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathSimConsoleApp
{
    public class PathSimCalculator
    {
        Data data;
        SortedDictionary<double, int> results = new SortedDictionary<double, int>(new DuplicateComparer());

        public PathSimCalculator(Data data)
        {
            this.data = data;
        }

        public IList<Tuple<int, double>> GetTopFiveResults()
        {
            List<Tuple<int, double>> topFive = new List<Tuple<int, double>>();

            int count = 0;
            foreach (KeyValuePair<double, int> kvp in results)
            {
                Tuple<int, double> pair = new Tuple<int, double>(kvp.Value, kvp.Key);

                topFive.Add(pair);

                if (count > 5)
                    break;

                count++;
            }

            return topFive;
        }

        public void CalculateAPCPA(int authorId)
        {
            SortedDictionary<double, int> simScores = results;

            foreach (KeyValuePair<int, Dictionary<int, int>> kvp in data.authorConferenceDict)
            {
                double simScore = CalculateSimilarity(authorId, kvp.Key);

                simScores.Add(simScore, kvp.Key);
            }
        }

        private double CalculateSimilarity(int authorIdOne, int authorIdTwo)
        {
            Dictionary<int, Dictionary<int, int>> matrix = data.authorConferenceDict;

            Dictionary<int, int> conferenceDictOne = matrix[authorIdOne];
            Dictionary<int, int> conferenceDictTwo = matrix[authorIdTwo];

            double numerator = 0;
            double denominator = 0;

            foreach(KeyValuePair<int, int> kvp in conferenceDictOne)
            {
                int conferenceId = kvp.Key;
                int countOne = kvp.Value;
                int countTwo = conferenceDictTwo[conferenceId];

                numerator += countOne * countTwo;

                denominator += countOne * countOne + countTwo * countTwo;
            }

            return (2.0 * numerator) / denominator;
        }
    }

    class DuplicateComparer : IComparer<double>
    { 
        public int Compare(double x, double y)
        {
            if (x > y)
            {
                return -1;
            }
            else if (x < y)
            {
                return 1;
            }
            else
            {
                return 1;
            }

        }

    }

}
