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
using System.Threading.Tasks;

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// Contains a set of methods that help to write text to the Console.
    /// </summary>
    public static class Cursor
    {
        /// <summary>
        /// Executes the specified action while hiding the cursor.
        /// </summary>
        /// <param name="action">The action to be executed.</param>
        public static void RunWithoutCursor(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            bool initialCursorVisible = Console.CursorVisible;
            Console.CursorVisible = false;

            try
            {
                action();
            }
            finally
            {
                Console.CursorVisible = initialCursorVisible;
            }
        }

        /// <summary>
        /// Executes the specified function while hiding the cursor.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="action">The function to be executed.</param>
        /// <returns>The value returned by the executed function.</returns>
        public static T RunWithoutCursor<T>(Func<T> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            bool initialCursorVisible = Console.CursorVisible;
            Console.CursorVisible = false;

            try
            {
                return action();
            }
            finally
            {
                Console.CursorVisible = initialCursorVisible;
            }
        }

        /// <summary>
        /// Executes asynchronously the specified function while hiding the cursor.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="action">The function to be executed.</param>
        /// <returns>A <see cref="Task{T}"/> instance representing the asynchronous execution.</returns>
        public static async Task<T> RunWithoutCursorAsync<T>(Func<Task<T>> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            bool initialCursorVisible = Console.CursorVisible;
            Console.CursorVisible = false;

            try
            {
                return await action();
            }
            finally
            {
                Console.CursorVisible = initialCursorVisible;
            }
        }
    }
}