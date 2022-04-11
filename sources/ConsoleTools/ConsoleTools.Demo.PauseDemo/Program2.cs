// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.Advanced;
using DustInTheWind.ConsoleTools.Controls;

namespace ConsoleTools.Demo.PauseDemo.NetCore
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ConsoleOutputMode consoleOutputMode = new ConsoleOutputMode();
            ConsoleInputMode consoleInputMode = new ConsoleInputMode();

            DisplayObject("ConsoleOutputMode", consoleOutputMode);

            consoleOutputMode.IsEnableVirtualTerminalProcessing = false;
            consoleOutputMode.IsDisabledNewLineAutoReturn = true;

            string line = new string('=', 120);
            Console.WriteLine(line);
            Console.Write(line);

            DisplayObject("ConsoleOutputMode", consoleOutputMode);

            Pause.QuickDisplay();
        }

        private static void DisplayObject(string title, object obj)
        {
            CustomConsole.WriteLineEmphasized(title);

            IEnumerable<KeyValuePair<string, object>> values = EnumerateMembers(obj);

            KeyValueControl<string, object> control = new KeyValueControl<string, object>
            {
                Padding = "0 0 1 0",
                BackgroundColor = ConsoleColor.Blue,
                Items = values.ToList()
            };
            control.Display();

            //foreach ((string key, object value) in values)
            //    Display(key, value);
        }

        private static void Display(string label, object value)
        {
            CustomConsole.Write("{0}: ", label);

            if (value is bool boolValue)
                if (boolValue)
                    CustomConsole.WriteLineSuccess(value);
                else
                    CustomConsole.WriteLineError(value);
            else
                CustomConsole.WriteLine(value);
        }

        private static IEnumerable<KeyValuePair<string, object>> EnumerateMembers(object obj)
        {
            Type type = obj.GetType();

            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);

            foreach (FieldInfo fieldInfo in fields)
            {
                string key = fieldInfo.Name;
                object value = fieldInfo.GetValue(obj);

                yield return new KeyValuePair<string, object>(key, value);
            }

            IEnumerable<PropertyInfo> properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.CanRead);

            foreach (PropertyInfo propertyInfo in properties)
            {
                string key = propertyInfo.Name;
                object value = propertyInfo.GetValue(obj);

                yield return new KeyValuePair<string, object>(key, value);
            }
        }
    }
}