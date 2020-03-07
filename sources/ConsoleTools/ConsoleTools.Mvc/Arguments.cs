// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Mvc
{
    public class Arguments
    {
        public string Command { get; }

        public List<Argument> Values { get; } = new List<Argument>();
        public int Count => Values.Count;

        public string this[int index] => Values[index]?.Value ?? Values[index]?.Name;

        public Arguments(IReadOnlyList<string> args)
        {
            if (args == null || args.Count == 0)
                return;

            Command = args[0];

            string previousName = null;

            for (int i = 1; i < args.Count; i++)
            {
                string arg = args[i];

                if (arg == null)
                    continue;

                bool isNewArgument = arg.StartsWith("-");

                if (isNewArgument)
                {
                    if (previousName != null)
                        Values.Add(new Argument(previousName, null));

                    previousName = arg.TrimStart('-');
                }
                else
                {
                    if (previousName != null)
                    {
                        Values.Add(new Argument(previousName, arg));

                        previousName = null;
                    }
                    else
                    {
                        Values.Add(new Argument(arg, null));
                    }
                }
            }
        }
    }
}