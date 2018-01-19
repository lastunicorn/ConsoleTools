using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace ConsoleApplication1
{
    [StructLayout(LayoutKind.Sequential)]
    struct COORD
    {
        public short X;
        public short Y;
    }

    internal static class WinAPI
    {
        public const Int16 STD_INPUT_HANDLE = -10;
        public const Int16 STD_OUTPUT_HANDLE = -11;
        public const Int16 STD_ERROR_HANDLE = -12;

        public const int INVALID_HANDLE_VALUE = -1;

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern int SetConsoleCursorPosition(IntPtr hConsoleOutput, COORD dwCursorPosition);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        //[DllImport("kernel32.dll", SetLastError = true)]
        //public static extern int ReadConsoleOutputCharacter(IntPtr hConsoleOutput, LPTSTR lpCharacter, int nLength, COORD dwReadCoord, LPDWORD lpNumberOfCharsRead);

        [DllImport("kernel32.dll")]
        public static extern bool ReadConsoleOutputCharacter(IntPtr hConsoleOutput, [Out] StringBuilder lpCharacter, uint nLength, COORD dwReadCoord, out uint lpNumberOfCharsRead);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <exception cref="Win32Exception"></exception>
        public static void SetConsoleCursorPosition(short x, short y)
        {
            IntPtr h = WinAPI.GetStdHandle(WinAPI.STD_OUTPUT_HANDLE);
            COORD coord = new COORD();
            coord.X = x;
            coord.Y = y;
            if (WinAPI.SetConsoleCursorPosition(h, coord) == 0)
            {
                throw new Win32Exception();
            }
        }

        public static string ReadALineOfConsoleOutput(short lineIndex)
        {
            IntPtr stdout = WinAPI.GetStdHandle(WinAPI.STD_OUTPUT_HANDLE);

            if (stdout.ToInt32() == INVALID_HANDLE_VALUE)
                throw new Win32Exception();

            // this assumes the console screen buffer is 80 columns wide.
            // You can call GetConsoleScreenBufferInfo() to get its actual dimensions.
            uint nLength = 80;
            StringBuilder lpCharacter = new StringBuilder((int)nLength);

            // read from the first character of the first line (0, 0).
            COORD dwReadCoord;
            dwReadCoord.X = 0;
            dwReadCoord.Y = lineIndex;

            uint lpNumberOfCharsRead = 0;

            if (!ReadConsoleOutputCharacter(stdout, lpCharacter, nLength, dwReadCoord, out lpNumberOfCharsRead))
                throw new Win32Exception();

            return lpCharacter.ToString();
        }
    }
}
