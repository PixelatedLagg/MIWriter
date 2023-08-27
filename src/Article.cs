using System.Text;

namespace MIWriter
{
    public class Article
    {
        public string Title = "";
        public string Url = "";
        public List<Section> Sections = new();

        public string Render()
        {
            StringBuilder builder = new($"    \"{Url}\" : {{\n        \"title\" : \"{Title}\",\n        \"sections\" : [\n");
            foreach (Section section in Sections)
            {
                builder.Append($"            {{\n                \"text\" : \"{section.Text}\",\n                \"image\" : \"<div class=\\\"image\\\"><img src=\\\"{section.ImageUrl}\\\" alt=\\\"{section.ImageAlt}\\\"><p>{section.Text}</p></div>\"\n            }},\n");
            }
            builder.Length--; //remove last ','
            return builder.Append("        ]\n    }").ToString();
        }
    }
}