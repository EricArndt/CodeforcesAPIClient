using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace CodeforcesAPI
{
    public class UserStatus
    {
        public string Userhandle { get; private set; }
        public HashSet<Submission> OldSubmissions;
        public HashSet<Submission> UniqueCorrectSubmissions;
        public int PointTotal { get; private set; }

        private readonly Time time;
        public List<Submission> Submissions { get; private set; }
        public UserStatus(string handle, string apiResponse, Time startTime)
        {
            time = startTime;
            Userhandle = handle;
            dynamic ob = JObject.Parse(apiResponse);
            var results = ob.result;

            Submissions = new List<Submission>();
            foreach (var r in results)
            {
                Submissions.Add(new Submission(r));
            }

            OldSubmissions = new HashSet<Submission>();
            foreach (Submission submission in Submissions)
            {
                if (submission.Verdict == Verdict.Ok && !time.SubmittedAfterStart(Time.Epoch.AddSeconds(submission.CreationTimeSeconds)))
                {
                    OldSubmissions.Add(submission);
                }

            }

            UniqueCorrectSubmissions = new HashSet<Submission>();
            foreach (Submission submission in Submissions.Where(x => x.Verdict == Verdict.Ok && !OldSubmissions.Contains(x) && time.SubmittedAfterStart(Time.Epoch.AddSeconds(x.CreationTimeSeconds))))
            {
                UniqueCorrectSubmissions.Add(submission);
            }

            PointTotal = CalculatePointTotal(UniqueCorrectSubmissions);
        }

        private static int CalculatePointTotal(IEnumerable<Submission> submissions)
        {
            int points = 0;
            foreach (Submission submission in submissions)
            {
                switch (submission.Problem.Difficulty)
                {
                    case ProblemDifficulty.A:
                        points += 50;
                        break;
                    case ProblemDifficulty.B:
                        points += 200;
                        break;
                    case ProblemDifficulty.C:
                        points += 500;
                        break;
                    case ProblemDifficulty.D:
                        points += 2000;
                        break;
                    case ProblemDifficulty.E:
                        points += 5000;
                        break;
                }
            }

            return points;
        }
    }
}
