using System;
using System.Collections.Generic;
using DustInTheWind.ConsoleTools;

namespace ConsoleTools.Demo.PauseDemo.NetCore
{
    internal class KeyValueControl<TKey, TValue> : BlockControl
    {
        public List<KeyValuePair<TKey, TValue>> Items { get; set; }

        protected override void DoDisplayContent(ControlDisplay display)
        {
            foreach ((TKey key, TValue value) in Items)
            {
                DisplayLine(display, key, value);
            }
        }

        private static void DisplayLine(ControlDisplay display, object key, object value)
        {
            display.StartRow();
            display.Write($"{key}: ");

            if (value is bool boolValue)
            {
                ConsoleColor foregroundColor = boolValue
                    ? CustomConsole.SuccessColor
                    : CustomConsole.ErrorColor;

                display.Write(foregroundColor, null, value.ToString());
            }
            else
            {
                display.Write(value.ToString());
            }

            display.EndRow();
        }
    }
}