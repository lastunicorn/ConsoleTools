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
using System.IO;

namespace DustInTheWind.ConsoleTools
{
    internal abstract class ThreeStageFile : IDisposable
    {
        private readonly string targetFileName;
        private readonly string tempFileName;
        private readonly string backupFileName;

        private FileStream fileStream;

        protected ThreeStageFile(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));

            targetFileName = fileName;
            tempFileName = $"{fileName}.tmp";
            backupFileName = $"{fileName}.bak";
        }

        public void Open()
        {
            if (fileStream != null)
                return;

            if (File.Exists(tempFileName))
                throw new ApplicationException($"The previous process was not completed. Delete the temporary {tempFileName} file and then try again.");

            fileStream = File.OpenWrite(tempFileName);
        }

        public void Execute()
        {
            if (fileStream == null)
                throw new ApplicationException("File is not opened.");

            DoExecute(fileStream);
        }

        protected abstract void DoExecute(FileStream fileStream);

        public void Close()
        {
            if (fileStream == null)
                return;

            fileStream.Close();
            fileStream.Dispose();
            fileStream = null;

            if (File.Exists(targetFileName))
                File.Replace(tempFileName, targetFileName, backupFileName);
            else
                File.Move(tempFileName, targetFileName);
        }

        public void Dispose()
        {
            fileStream?.Dispose();
        }
    }
}