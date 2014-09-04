using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeforcesAPI
{
    public class Problem
    {
        public string Name { get; private set; }
        public ProblemDifficulty Difficulty { get; private set; }

        public Problem(dynamic problem)
        {
            Name = problem.name;
            Difficulty = DetermineProblemDifficulty((string)problem.index);
        }

        private static ProblemDifficulty DetermineProblemDifficulty(string difficulty)
        {
            if (difficulty.Contains("A"))
            {
                return ProblemDifficulty.A;
            }

            if (difficulty.Contains("B"))
            {
                return ProblemDifficulty.B;
            }

            if (difficulty.Contains("C"))
            {
                return ProblemDifficulty.C;
            }

            if (difficulty.Contains("D"))
            {
                return ProblemDifficulty.D;
            }

            if (difficulty.Contains("E"))
            {
                return ProblemDifficulty.E;
            }

            return ProblemDifficulty.Unknown;
        }
    }
}
