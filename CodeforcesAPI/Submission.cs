namespace CodeforcesAPI
{
    public class Submission
    {
        public int Id { get; private set; }
        public int ContestId { get; private set; }
        public int CreationTimeSeconds { get; private set; }
        public int RelativeTimeSeconds { get; private set; }
        public Verdict Verdict { get; private set; }
        public Problem Problem { get; private set; }
        public int TimeConsumedMillis { get; private set; }
        public int MemoryConsumedBytes { get; private set; }

        public Submission(dynamic result)
        {
            Id = result.id;
            ContestId = result.contestId;
            CreationTimeSeconds = result.creationTimeSeconds;
            RelativeTimeSeconds = result.relativeTimeSeconds;
            Verdict = DetermineVerdict((string)result.verdict);
            Problem = new Problem(result.problem);
            TimeConsumedMillis = result.timeConsumedMillis;
            MemoryConsumedBytes = result.memoryConsumedBytes;
        }

        private static Verdict DetermineVerdict(string verdict)
        {
            switch (verdict)
            {
                case "OK":
                {
                    return Verdict.Ok;
                }
                default:
                    return Verdict.Absent;
            }
        }

        public override bool Equals(object obj)
        {
            Submission other = obj as Submission;

            if (other == null)
            {
                return false;
            }

            return Problem.Name == other.Problem.Name;
        }

        public override int GetHashCode()
        {
            return Problem.Name.GetHashCode();
        }
    }
}
