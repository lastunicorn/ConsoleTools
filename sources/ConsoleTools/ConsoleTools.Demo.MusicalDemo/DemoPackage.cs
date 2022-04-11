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

using DustInTheWind.ConsoleTools.Controls.Musical;
using DustInTheWind.ConsoleTools.Demo.NetCore;

namespace DustInTheWind.ConsoleTools.Demo.MusicalDemo
{
    public class DemoPackage : IDemoPackage
    {
        public string ShortDescription => "Musical Demo";

        public void ExecuteDemo()
        {
            CustomConsole.WriteLine("The sound is playing.");
            PlayGreetingSound();
            CustomConsole.WriteLine("The sound has finished.");
        }

        private static void PlayGreetingSound()
        {
            Sound.Play(MusicalNote.G4, 600);
            Sound.Play(MusicalNote.E4, 600);

            Sound.Play(MusicalNote.G4, 300);
            Sound.Play(MusicalNote.G4, 300);
            Sound.Play(MusicalNote.E4, 600);

            Sound.Play(MusicalNote.G4, 300);
            Sound.Play(MusicalNote.G4, 300);
            Sound.Play(MusicalNote.E4, 300);
            Sound.Play(MusicalNote.E4, 300);

            Sound.Play(MusicalNote.G4, 300);
            Sound.Play(MusicalNote.G4, 300);
            Sound.Play(MusicalNote.E4, 600);

            Sound.Play(MusicalNote.G4, 300);
            Sound.Play(MusicalNote.G4, 300);
            Sound.Play(MusicalNote.E4, 300);
            Sound.Play(MusicalNote.E4, 300);

            Sound.Play(MusicalNote.G4, 600);
            Sound.Play(MusicalNote.E4, 600);

            Sound.Play(MusicalNote.G4, 300);
            Sound.Play(MusicalNote.G4, 300);
            Sound.Play(MusicalNote.E4, 300);
            Sound.Play(MusicalNote.E4, 300);

            Sound.Play(MusicalNote.G4, 600);
            Sound.Play(MusicalNote.E4, 600);
        }
    }
}