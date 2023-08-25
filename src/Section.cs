using System.Text;

namespace MIWriter
{
    public class Section
    {
        public string Text;
        public string ImageUrl;
        public string ImageAlt;
        public string ImageCaption;

        public Section()
        {
            Text = "";
            ImageUrl = "";
            ImageAlt = "";
            ImageCaption = "";
        }
    }
}