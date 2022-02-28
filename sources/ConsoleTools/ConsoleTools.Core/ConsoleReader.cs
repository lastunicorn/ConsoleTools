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
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;
using System.Threading;

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// Thanks to JSQuareD
    /// https://stackoverflow.com/questions/57615/how-to-add-a-timeout-to-console-readline/57655#57655
    /// </summary>
    public class ConsoleReader
    {
        private static readonly Thread inputThread;
        private static readonly AutoResetEvent getInput;
        private static readonly AutoResetEvent gotInput;
        private static string input;

        static ConsoleReader()
        {
            getInput = new AutoResetEvent(false);
            gotInput = new AutoResetEvent(false);

            inputThread = new Thread(reader)
            {
                IsBackground = true
            };
            inputThread.Start();
        }

        private static void reader()
        {
            while (true)
            {
                getInput.WaitOne();
                input = Console.ReadLine();
                gotInput.Set();
            }
        }

        public static string ReadLine(int timeOutMilliseconds = Timeout.Infinite)
        {
            getInput.Set();

            bool success = gotInput.WaitOne(timeOutMilliseconds);

            if (!success)
                throw new TimeoutException("User did not provide input within the time limit.");

            return input;
        }

        public static bool TryReadLine(out string line, int timeOutMilliseconds = Timeout.Infinite)
        {
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMilliseconds);

            line = success ? input : null;

            return success;
        }
    }
}