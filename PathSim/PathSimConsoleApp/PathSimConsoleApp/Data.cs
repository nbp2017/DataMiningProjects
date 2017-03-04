using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathSimConsoleApp
{
    public class Data
    {
        public Dictionary<int, Paper> paperAuthorConferenceDict = new Dictionary<int, Paper>();

        public Dictionary<int, Dictionary<int, int>> authorConferenceDict = new Dictionary<int, Dictionary<int, int>>();

    }

    public class Paper
    {
        public Paper(int id)
        {
            this.Id = id;
            this.AuthorList = new HashSet<int>();
            this.ConferenceList = new HashSet<int>();
        }

        public int Id { get; private set; }

        public HashSet<int> AuthorList { get; private set; }

        public HashSet<int> ConferenceList { get; private set; }
    }
}
