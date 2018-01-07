// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.CommandProviders
{
    /// <summary>
    /// Creates <see cref="CliCommand"/> objects.
    /// </summary>
    public interface ICommandProvider
    {
        /// <summary>
        /// Event raised when the user writes a new command at the console.
        /// </summary>
        event EventHandler<NewCommandEventArgs> NewCommand;

        /// <summary>
        /// Continously creates new commands (<see cref="CliCommand"/>).
        /// After a command is created, the <see cref="NewCommand"/> event is raised.
        /// The <see cref="Run"/> method blocks the current execution thread.
        /// The infinite loop that creates commands can be stopped
        /// by setting the <see cref="NewCommandEventArgs.Exit"/> property in the <see cref="NewCommand"/> event
        /// or by calling the <see cref="RequestStop"/> method.
        /// </summary>
        void Run();

        /// <summary>
        /// Creates a single command (<see cref="CliCommand"/>) and returns it.
        /// </summary>
        /// <returns>A <see cref="CliCommand"/> object containing the new command.</returns>
        CliCommand RunOnce();

        /// <summary>
        /// Sets the stop flag.
        /// The Prompter's loop will exit next time when it checks the stop flag.
        /// </summary>
        void RequestStop();
    }
}