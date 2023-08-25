using System.Text;

namespace MIWriter
{
    public class Article
    {
        public string Title;
        public string Url;
        public List<Section> Sections;

        public string Render()
        {
            StringBuilder builder = new($"    \"{Url}\" : {{\n        \"title\" : \"{Title}\",        \"sections\" : [\n");
            foreach (Section section in Sections)
            {
                builder.Append("            {")
                foreach (IElement element in Text)
                {
                    builder.Append(element.Render());
                }
                builder.Append($"\"\n                \"image\" : \"<div class=\\\"image\\\"><img src=\\\"{ImageUrl}\\\" alt=\\\"{ImageAlt}\\\">");

                return builder.Append("\"\n                \"image\" : \"<div class=\\\"image\\\"><img src=\\\"url\\\" alt=\\\"alt\\\"><p>caption</p></div>\"").ToString();
            }
        }
    }
}