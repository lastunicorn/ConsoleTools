// ConsoleTools
// Copyright (C) 2017 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace DustInTheWind.ConsoleTools
{
    public partial class CustomConsole
    {
        public static void Write(string value)
        {
            Console.Write(value);
        }

        public static void Write(string format, params object[] arg)
        {
            Console.Write(format, arg);
        }
        public static void Write(object o)
        {
            Console.Write(o);
        }

        public static void WriteLine()
        {
            Console.WriteLine();
        }

        public static void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public static void WriteLine(string format, params object[] arg)
        {
            Console.WriteLine(format, arg);
        }

        public static void WriteLine(object o)
        {
            Console.WriteLine(o);
        }
    }
}