using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Threading;
using System.Xml;

namespace CodeforcesAPI
{
    class Runner
    {
        private static List<string> handles;
        private static Time time;
        const string URL = "http://codeforces.com/api/user.status?handle={0}&from=1";

        static void Main()
        {
            Initialize();

            var globalTimer = new System.Timers.Timer(60000 + handles.Count * 1000);
            globalTimer.Elapsed += (sender, args) => ProcessHandles();
            globalTimer.Start();

            Console.Read();
        }

        static void ProcessHandles()
        {
            var users = new List<UserStatus>();
            foreach (string handle in handles)
            {
                var request = WebRequest.Create(string.Format(URL, handle));
                var stream = request.GetResponse().GetResponseStream();

                string line;
                using (var reader = new StreamReader(stream))
                {
                    line = reader.ReadToEnd();
                }

                users.Add(new UserStatus(handle, line, time));
                Thread.Sleep(500);
            }

            var htmlDoc = new HtmlDoc();
            htmlDoc.AddUsersToTable(users);
           htmlDoc.WriteHtmlDoc();
        }

        static void Initialize()
        {
            handles = new List<string>();
            ReadConfig();
        }

        static void ReadConfig()
        {
            var doc = new XmlDocument();
            doc.Load("Config.xml");
            XmlNodeList xmlHandles = doc.SelectNodes("/base/handles/handle");

            foreach (XmlElement h in xmlHandles)
            {
                handles.Add(h.InnerText);
            }

            XmlNode year = doc.SelectSingleNode("/base/startTime/year");
            XmlNode month = doc.SelectSingleNode("/base/startTime/month");
            XmlNode day = doc.SelectSingleNode("/base/startTime/day");
            XmlNode hour = doc.SelectSingleNode("/base/startTime/hour");
            XmlNode minute = doc.SelectSingleNode("/base/startTime/minute");

            var startTime = new DateTime(int.Parse(year.InnerText), int.Parse(month.InnerText), int.Parse(day.InnerText), int.Parse(hour.InnerText), int.Parse(minute.InnerText), 0);
            time = new Time(startTime);
        }
    }
}
