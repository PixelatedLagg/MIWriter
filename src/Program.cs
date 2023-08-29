using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace MIWriter
{
    public class Program
    {
        private static string[] Options = new string[] {
            "Help",
            "Quit",
            
        };

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
                        Console.WriteLine("aids!");
                        //goto somewhere else with file name to load
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
            JToken article = (from child in JObject.Parse(File.ReadAllText(name)).Children() where child["title"]?.ToString() == name select child).First();
            
        }
    }
}