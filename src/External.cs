using System.Runtime.InteropServices;

namespace MIWriter
{
    public static class External
    {
        /*
            Code from https://stackoverflow.com/questions/39476157/detect-ctrl-s-in-console
        */
        
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [Flags]
        public enum ConsoleInputModes : uint
        {
            ENABLE_PROCESSED_INPUT = 0x0001,
            ENABLE_LINE_INPUT = 0x0002,
            ENABLE_ECHO_INPUT = 0x0004,
            ENABLE_WINDOW_INPUT = 0x0008,
            ENABLE_MOUSE_INPUT = 0x0010,
            ENABLE_INSERT_MODE = 0x0020,
            ENABLE_QUICK_EDIT_MODE = 0x0040,
            ENABLE_EXTENDED_FLAGS = 0x0080,
            ENABLE_AUTO_POSITION = 0x0100
        }

        [Flags]
        public enum ConsoleOutputModes : uint
        {
            ENABLE_PROCESSED_OUTPUT = 0x0001,
            ENABLE_WRAP_AT_EOL_OUTPUT = 0x0002,
            ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004,
            DISABLE_NEWLINE_AUTO_RETURN = 0x0008,
            ENABLE_LVB_GRID_WORLDWIDE = 0x0010
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        public static int STD_INPUT_HANDLE = -10;
    }
}