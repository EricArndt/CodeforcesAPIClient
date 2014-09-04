using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeforcesAPI
{
    public class HtmlTag
    {
        public string Name { get; private set; }
        public List<HtmlTag> Children { get; private set; }

        public List<string> Attributes { get; private set; } 

        public string Content { get; private set; }

        public HtmlTag(string name, string content = "", List<string> attributes = null)
        {
            Name = name;
            Content = content;
            Children = new List<HtmlTag>();
            Attributes = attributes ?? new List<string>();
        }

        public void AddChild(HtmlTag child)
        {
            Children.Add(child);
        }

        public void AddAttribute(string attribute)
        {
            Attributes.Add(attribute);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("<").Append(Name).Append(" ").Append(AttributesToString()).Append(">");
            sb.Append(Content);

            foreach (HtmlTag child in Children)
            {
                sb.Append(child);
            }

            sb.Append("</").Append(Name).Append(">");

            return sb.ToString();
        }

        private string AttributesToString()
        {
            if (!Attributes.Any())
            {
                return "";
            }

            var sb = new StringBuilder();

            for (int i = 0; i < Attributes.Count - 1; ++i)
            {
                sb.Append(Attributes[i]).Append("; ");
            }

            sb.Append(Attributes.Last());

            return sb.ToString();
        }
    }
}
