using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathSimConsoleApp
{
    public class DataParser
    {
        private string dataPath = @"D:\PersonalProjects\DataMiningProjects\data";
        public Data data = new Data();

        public void Parse()
        {
            HashSet<int> authors = new HashSet<int>();
            HashSet<int> conferences = new HashSet<int>();

            // Parse PaperAuthor
            string paPath = Path.Combine(dataPath, "PA.txt");
            string[] paLines = File.ReadAllLines(paPath);

            foreach (string line in paLines)
            {
                string[] tokens = line.Split();

                int idPaper = int.Parse(tokens[0]);
                int idAuthor = int.Parse(tokens[1]);

                if (data.paperAuthorConferenceDict.ContainsKey(idPaper))
                {
                    Paper paper = data.paperAuthorConferenceDict[idPaper];
                    paper.AuthorList.Add(idAuthor);
                }
                else
                {
                    Paper paper = new Paper(idPaper);
                    paper.AuthorList.Add(idAuthor);
                    data.paperAuthorConferenceDict.Add(idPaper, paper);
                }

                if (!authors.Contains(idAuthor))
                    authors.Add(idAuthor);
            }

            // Parse PaperConference
            string pcPath = Path.Combine(dataPath, "PC.txt");
            string[] pcLines = File.ReadAllLines(pcPath);

            foreach (string line in pcLines)
            {
                string[] tokens = line.Split();

                int idPaper = int.Parse(tokens[0]);
                int idConference = int.Parse(tokens[1]);

                if (data.paperAuthorConferenceDict.ContainsKey(idPaper))
                {
                    Paper paper = data.paperAuthorConferenceDict[idPaper];
                    paper.ConferenceList.Add(idConference);

                    if (!conferences.Contains(idConference))
                        conferences.Add(idConference);
                }
                else
                    Debug.Assert(false);
            }

            // Create the Author Conference Matrix
            foreach (int authorId in authors)
            {
                Dictionary<int, int> conferenceCountDict = new Dictionary<int, int>();
                data.authorConferenceDict.Add(authorId, conferenceCountDict);

                foreach (int conferenceId in conferences)
                {
                    conferenceCountDict.Add(conferenceId, 0);
                }
            }

            // Add Papers to the matrix
            foreach (KeyValuePair<int, Paper> kvp in data.paperAuthorConferenceDict)
            {
                Paper paper = kvp.Value;
                foreach (int authorId in paper.AuthorList)
                {
                    Dictionary<int, int> conferenceCountDict = data.authorConferenceDict[authorId];
                    foreach (int conferenceId in paper.ConferenceList)
                    {
                        int count = conferenceCountDict[conferenceId] + 1;
                        conferenceCountDict[conferenceId] = count;
                    }
                }
            }

        }

        //public void Parse1()
        //{
        //    // Parse Papers and add to List
        //    string paperPath = Path.Combine(dataPath, "paper_label.txt");
        //    string[] paperLines = File.ReadAllLines(paperPath);
        //    foreach (string line in paperLines)
        //    {
        //        string[] tokens = line.Split();

        //        int id = int.Parse(tokens[0]);

        //        Paper paper = new Paper(id);

        //        data.paperAuthorConferenceDict.Add(id, paper);
        //    }

        //    // Parse Authors
        //    List<int> authors = new List<int>();
        //    string authorPath = Path.Combine(dataPath, "author_label.txt");
        //    string[] authorLines = File.ReadAllLines(authorPath);
        //    foreach (string line in authorLines)
        //    {
        //        string[] tokens = line.Split();

        //        int id = int.Parse(tokens[0]);

        //        authors.Add(id);
        //    }

        //    // Parse Conference
        //    List<int> conferences = new List<int>();
        //    string conferencePath = Path.Combine(dataPath, "conf_label.txt");
        //    string[] conferenceLines = File.ReadAllLines(conferencePath);
        //    foreach (string line in conferenceLines)
        //    {
        //        string[] tokens = line.Split();

        //        int id = int.Parse(tokens[0]);

        //        conferences.Add(id);
        //    }


        //    // Create the Author Conference Matrix
        //    foreach (int authorId in authors)
        //    {
        //        foreach (int conferenceId in conferences)
        //        {
        //            Tuple<int, int> key = new Tuple<int, int>(authorId, conferenceId);

        //            data.authorConferenceDict.Add(key, 0);
        //        }
        //    }


        //    // Parse PaperAuthor
        //    string paPath = Path.Combine(dataPath, "PA.txt");
        //    string[] paLines = File.ReadAllLines(paPath);

        //    foreach (string line in paLines)
        //    {
        //        string[] tokens = line.Split();

        //        int idPaper = int.Parse(tokens[0]);
        //        int idAuthor = int.Parse(tokens[1]);

        //        Paper paper = data.paperAuthorConferenceDict[idPaper];
        //        paper.AuthorList.Add(idAuthor);
        //    }


        //    // Parse PaperConference
        //    string pcPath = Path.Combine(dataPath, "PC.txt");
        //    string[] pcLines = File.ReadAllLines(pcPath);

        //    foreach (string line in pcLines)
        //    {
        //        string[] tokens = line.Split();

        //        int idPaper = int.Parse(tokens[0]);
        //        int idConference = int.Parse(tokens[1]);

        //        Paper paper = data.paperAuthorConferenceDict[idPaper];
        //        paper.ConferenceList.Add(idConference);
        //    }

        //    // Add Papers to the matrix
        //    foreach (KeyValuePair<int, Paper> kvp in data.paperAuthorConferenceDict)
        //    {
        //        Paper paper = kvp.Value;
        //        foreach (int authorId in paper.AuthorList)
        //        {
        //            foreach (int conferenceId in paper.ConferenceList)
        //            {
        //                Tuple<int, int> key = new Tuple<int, int>(authorId, conferenceId);
        //                int count = data.authorConferenceDict[key] + 1;
        //                data.authorConferenceDict[key] = count;
        //            }
        //        }
        //    }

        //} 

    }
}
