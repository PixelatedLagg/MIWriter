using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace MIWriter
{
    public class Program
    {

        public static void Main()
        {
            ReadLine("aids!");
            return;
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
            Console.WriteLine("ALT+T for new text; ALT+H for new header; ALT+M for new math; ALT+E for new exponent; ALT+N for new section; ALT+S for section info; ALT+I for new image; ALT+A for article info");
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ReadKey();
            ConsoleKeyInfo key = Console.ReadKey();
            while (key.Key != ConsoleKey.Escape)
            {
                if (key.Modifiers == ConsoleModifiers.Alt)
                {
                    switch (key.Key)
                    {
                        case ConsoleKey.T: //new text
                            break;
                        case ConsoleKey.H: //new header
                            break;
                        case ConsoleKey.M: //new math
                            break;
                        case ConsoleKey.E: //new exponent
                            break;
                        case ConsoleKey.N: //new section
                            break;
                        case ConsoleKey.S: //section info
                            break;
                        case ConsoleKey.I: //new image
                            break;
                        case ConsoleKey.A: //article info
                            break;
                    }
                }
                key = Console.ReadKey();
            }
        }
        static string ReadLine(string Default)
        {
            Console.Write(Default);
            ConsoleKeyInfo info;
            List<char> chars = new();
            if (!string.IsNullOrEmpty(Default))
            {
                chars.AddRange(Default.ToCharArray());
            }
            while (true)
            {
                info = Console.ReadKey(true);
                switch (info.Key)
                {
                    case ConsoleKey.Backspace:
                        if (Console.CursorLeft <= chars.Count - 1 && Console.CursorLeft > 0)
                        {
                            chars.RemoveAt(Console.CursorLeft);
                            //Console.CursorLeft -= 1;
                            int past = Console.CursorLeft;
                            Console.Write($"{(chars.ToString() ?? "")[Console.CursorLeft..chars.Count]} ");
                            Console.CursorLeft = past;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (Console.CursorLeft > 0)
                        {
                            Console.CursorLeft--;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (Console.CursorLeft < chars.Count)
                        {
                            Console.CursorLeft++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.Write(Environment.NewLine);
                        return new string(chars.ToArray());
                    default:
                        Console.Write(info.KeyChar);
                        chars.Add(info.KeyChar);
                        break;
                }
            }
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