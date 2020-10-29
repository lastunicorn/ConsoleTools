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
using System.IO;

namespace DustInTheWind.ConsoleTools
{
    internal abstract class ThreeStageFile : IDisposable
    {
        private readonly string targetFileName;
        private readonly string tempFileName;
        private readonly string backupFileName;

        public FileStream FileStream { get; private set; }

        protected ThreeStageFile(string fileName)
        {
            targetFileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            
            tempFileName = $"{fileName}.tmp";
            backupFileName = $"{fileName}.bak";
        }

        public void Open()
        {
            if (FileStream != null)
                return;

            if (File.Exists(tempFileName))
                throw new ApplicationException($"The previous process was not completed. Delete the temporary {tempFileName} file and then try again.");

            FileStream = File.OpenWrite(tempFileName);
        }

        public void Close()
        {
            if (FileStream == null)
                return;

            FileStream.Close();
            FileStream.Dispose();
            FileStream = null;

            if (File.Exists(targetFileName))
                File.Replace(tempFileName, targetFileName, backupFileName);
            else
                File.Move(tempFileName, targetFileName);
        }

        public void Dispose()
        {
            FileStream?.Dispose();
        }
    }
}