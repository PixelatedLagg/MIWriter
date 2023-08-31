using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace MIWriter
{
    public class Program
    {

        public static void Main()
        {
            if (!File.Exists("tempfile.txt"))
            {
                File.Create("tempfile.txt");
            }
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine("+ New Article");
            Console.BackgroundColor = ConsoleColor.Black;
            List<string> names = new();
            foreach (string line in File.ReadLines("tempfile.txt"))
            {
                if (line == "!LIST NAME END")
                {
                    break;
                }
                names.Add(line);
                Console.WriteLine(line);
            }



            Console.SetCursorPosition(0, Console.CursorTop - names.Count - 1);
            int position = -1;
            int lastPosition;
            while (true)
            {
                lastPosition = position;
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.Q)
                {
                    //help
                }
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (position > -1)
                        {
                            position--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (position < names.Count - 1)
                        {
                            position++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (position > -1)
                        {
                            OpenFile(names[position]);
                            Console.Clear();
                            Main();
                        }
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.BackgroundColor = ConsoleColor.Black;
                if (lastPosition == -1)
                {
                    Console.Write("+ New Article");
                }
                else
                {
                    Console.Write(names[lastPosition]);
                }
                Console.SetCursorPosition(0, Console.CursorTop + position - lastPosition);
                Console.BackgroundColor = ConsoleColor.Gray;
                if (position == -1)
                {
                    Console.Write("+ New Article");
                }
                else
                {
                    Console.Write(names[position]);
                }
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
        private static void OpenFile(string name)
        {
            JObject articleJson = JObject.Parse($"{{{File.ReadAllText($"articles/{name}.txt")}}}");
            List<Section> sections = new();
            foreach (JToken section in articleJson[name]?["sections"] ?? throw new Exception("null article!"))
            {
                ParseImage(out Section current, (section["image"] ?? "").ToString());
                Console.WriteLine($"url: {current.ImageUrl} alt: {current.ImageAlt} caption: {current.ImageCaption}");
                current.Text = (section["text"] ?? "").ToString();
                sections.Add(current);
            }
            Article article = new()
            {
                Title = (articleJson[name]?["title"] ?? "").ToString(),
                Url = name,
                Sections = sections
            };
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ALT+T for text; ALT+H for header; ALT+M for math; ALT+E for exponent; ALT+N for new section; ALT+S for section info; ALT+I for image; ALT+A for article info");
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ReadKey();
        }

        private static void ParseImage(out Section current, string image)
        {
            current = new();
            for (int i = 0; i < image.Length; i++)
            {
                if (image[i] == 'r')
                {
                    i += 4;
                    int start = i;
                    while (image[i] != ' ')
                    {
                        i++;
                    }
                    current.ImageUrl = image[start..(i - 1)];
                    i += 6;
                    start = i;
                    while (image[i] != '"')
                    {
                        i++;
                    }
                    current.ImageAlt = image[start..i];
                    i += 5;
                    start = i;
                    while (i + 10 != image.Length)
                    {
                        i++;
                    }
                    current.ImageCaption = image[start..i];
                    return;
                }
            }
        }
    }
}