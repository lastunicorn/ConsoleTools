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