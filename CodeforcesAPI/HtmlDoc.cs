using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeforcesAPI
{
    public class HtmlDoc
    {
        public HtmlTag FullDocument { get; private set; }
        public HtmlTag Head { get; private set; }

        public HtmlTag Body { get; private set; }

        public HtmlTag Table { get; private set; }

        private static readonly List<string> borderAttributes = new List<string> { "style='text-align:center; border:1px solid black'" }; 

        public HtmlDoc()
        {
            Head = new HtmlTag("head");
            Head.AddChild(new HtmlTag("title", "Scoreboard"));

            var tableHeader = new HtmlTag("tr", "", borderAttributes);
            tableHeader.AddChild(new HtmlTag("th", "Rank", borderAttributes));
            tableHeader.AddChild(new HtmlTag("th", "Handle", borderAttributes));
            tableHeader.AddChild(new HtmlTag("th", "A", borderAttributes));
            tableHeader.AddChild(new HtmlTag("th", "B", borderAttributes));
            tableHeader.AddChild(new HtmlTag("th", "C", borderAttributes));
            tableHeader.AddChild(new HtmlTag("th", "D", borderAttributes));
            tableHeader.AddChild(new HtmlTag("th", "E", borderAttributes));
            tableHeader.AddChild(new HtmlTag("th", "Points", borderAttributes));

            Table = new HtmlTag("table");
            Table.AddAttribute("style='width:100%; border:1px solid black; border-collapse:collapse'");
            Table.AddChild(tableHeader);

            Body = new HtmlTag("body");
            Body.AddChild(Table);

            FullDocument = new HtmlTag("html");
            FullDocument.AddChild(Head);
            FullDocument.AddChild(Body);
        }

        public void AddUsersToTable(IEnumerable<UserStatus> users)
        {
            int rank = 1;
            foreach (UserStatus user in users.OrderByDescending(x => x.PointTotal))
            {
                AddUserToTable(user, rank);
                ++rank;
            }
        }

        private void AddUserToTable(UserStatus user, int rank)
        {
            
            var userRow = new HtmlTag("tr", "", borderAttributes);
            userRow.AddChild(new HtmlTag("td", rank.ToString(CultureInfo.InvariantCulture), borderAttributes));
            userRow.AddChild(new HtmlTag("td", user.Userhandle, borderAttributes));
            userRow.AddChild(new HtmlTag("td", user.UniqueCorrectSubmissions.Count(x => x.Problem.Difficulty == ProblemDifficulty.A).ToString(CultureInfo.InvariantCulture), borderAttributes));
            userRow.AddChild(new HtmlTag("td", user.UniqueCorrectSubmissions.Count(x => x.Problem.Difficulty == ProblemDifficulty.B).ToString(CultureInfo.InvariantCulture), borderAttributes));
            userRow.AddChild(new HtmlTag("td", user.UniqueCorrectSubmissions.Count(x => x.Problem.Difficulty == ProblemDifficulty.C).ToString(CultureInfo.InvariantCulture), borderAttributes));
            userRow.AddChild(new HtmlTag("td", user.UniqueCorrectSubmissions.Count(x => x.Problem.Difficulty == ProblemDifficulty.D).ToString(CultureInfo.InvariantCulture), borderAttributes));
            userRow.AddChild(new HtmlTag("td", user.UniqueCorrectSubmissions.Count(x => x.Problem.Difficulty == ProblemDifficulty.E).ToString(CultureInfo.InvariantCulture), borderAttributes));
            userRow.AddChild(new HtmlTag("td", user.PointTotal.ToString(CultureInfo.InvariantCulture), borderAttributes));

            Table.AddChild(userRow);
        }

        public void WriteHtmlDoc()
        {
            File.WriteAllText("Scoreboard.html", "<!DOCTYPE html>");
            File.AppendAllText("Scoreboard.html", FullDocument.ToString());
        }
    }
}
