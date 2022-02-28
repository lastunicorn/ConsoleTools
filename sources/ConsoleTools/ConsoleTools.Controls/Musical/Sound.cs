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
using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls.Musical
{
    public static class Sound
    {
        private static readonly Dictionary<MusicalNote, MusicalNoteInfo> MusicalNotes = new Dictionary<MusicalNote, MusicalNoteInfo>
        {
            { MusicalNote.C0, new MusicalNoteInfo(MusicalNote.C0, 16.35, 2100) },
            { MusicalNote.Cd0, new MusicalNoteInfo(MusicalNote.Cd0, 17.32, 1990) },
            { MusicalNote.Db0, new MusicalNoteInfo(MusicalNote.Db0, 17.32, 1990) },
            { MusicalNote.D0, new MusicalNoteInfo(MusicalNote.D0, 18.35, 1870) },
            { MusicalNote.Dd0, new MusicalNoteInfo(MusicalNote.Dd0, 19.45, 1770) },
            { MusicalNote.Eb0, new MusicalNoteInfo(MusicalNote.Eb0, 19.45, 1770) },
            { MusicalNote.E0, new MusicalNoteInfo(MusicalNote.E0, 20.6, 1670) },
            { MusicalNote.F0, new MusicalNoteInfo(MusicalNote.F0, 21.83, 1580) },
            { MusicalNote.Fd0, new MusicalNoteInfo(MusicalNote.Fd0, 23.12, 1490) },
            { MusicalNote.Gb0, new MusicalNoteInfo(MusicalNote.Gb0, 23.12, 1490) },
            { MusicalNote.G0, new MusicalNoteInfo(MusicalNote.G0, 24.5, 1400) },
            { MusicalNote.Gd0, new MusicalNoteInfo(MusicalNote.Gd0, 25.96, 1320) },
            { MusicalNote.Ab0, new MusicalNoteInfo(MusicalNote.Ab0, 25.96, 1320) },
            { MusicalNote.A0, new MusicalNoteInfo(MusicalNote.A0, 27.5, 1250) },
            { MusicalNote.Ad0, new MusicalNoteInfo(MusicalNote.Ad0, 29.14, 1180) },
            { MusicalNote.Bb0, new MusicalNoteInfo(MusicalNote.Bb0, 29.14, 1180) },
            { MusicalNote.B0, new MusicalNoteInfo(MusicalNote.B0, 30.87, 1110) },
            { MusicalNote.C1, new MusicalNoteInfo(MusicalNote.C1, 32.7, 1050) },
            { MusicalNote.Cd1, new MusicalNoteInfo(MusicalNote.Cd1, 34.65, 996) },
            { MusicalNote.Db1, new MusicalNoteInfo(MusicalNote.Db1, 34.65, 996) },
            { MusicalNote.D1, new MusicalNoteInfo(MusicalNote.D1, 36.71, 940) },
            { MusicalNote.Dd1, new MusicalNoteInfo(MusicalNote.Dd1, 38.89, 887) },
            { MusicalNote.Eb1, new MusicalNoteInfo(MusicalNote.Eb1, 38.89, 887) },
            { MusicalNote.E1, new MusicalNoteInfo(MusicalNote.E1, 41.2, 837) },
            { MusicalNote.F1, new MusicalNoteInfo(MusicalNote.F1, 43.65, 790) },
            { MusicalNote.Fd1, new MusicalNoteInfo(MusicalNote.Fd1, 46.25, 746) },
            { MusicalNote.Gb1, new MusicalNoteInfo(MusicalNote.Gb1, 46.25, 746) },
            { MusicalNote.G1, new MusicalNoteInfo(MusicalNote.G1, 49, 704) },
            { MusicalNote.Gd1, new MusicalNoteInfo(MusicalNote.Gd1, 51.91, 665) },
            { MusicalNote.Ab1, new MusicalNoteInfo(MusicalNote.Ab1, 51.91, 665) },
            { MusicalNote.A1, new MusicalNoteInfo(MusicalNote.A1, 55, 627) },
            { MusicalNote.Ad1, new MusicalNoteInfo(MusicalNote.Ad1, 58.27, 592) },
            { MusicalNote.Bb1, new MusicalNoteInfo(MusicalNote.Bb1, 58.27, 592) },
            { MusicalNote.B1, new MusicalNoteInfo(MusicalNote.B1, 61.74, 559) },
            { MusicalNote.C2, new MusicalNoteInfo(MusicalNote.C2, 65.41, 527) },
            { MusicalNote.Cd2, new MusicalNoteInfo(MusicalNote.Cd2, 69.3, 498) },
            { MusicalNote.Db2, new MusicalNoteInfo(MusicalNote.Db2, 69.3, 498) },
            { MusicalNote.D2, new MusicalNoteInfo(MusicalNote.D2, 73.42, 470) },
            { MusicalNote.Dd2, new MusicalNoteInfo(MusicalNote.Dd2, 77.78, 444) },
            { MusicalNote.Eb2, new MusicalNoteInfo(MusicalNote.Eb2, 77.78, 444) },
            { MusicalNote.E2, new MusicalNoteInfo(MusicalNote.E2, 82.41, 419) },
            { MusicalNote.F2, new MusicalNoteInfo(MusicalNote.F2, 87.31, 395) },
            { MusicalNote.Fd2, new MusicalNoteInfo(MusicalNote.Fd2, 92.5, 373) },
            { MusicalNote.Gb2, new MusicalNoteInfo(MusicalNote.Gb2, 92.5, 373) },
            { MusicalNote.G2, new MusicalNoteInfo(MusicalNote.G2, 98, 352) },
            { MusicalNote.Gd2, new MusicalNoteInfo(MusicalNote.Gd2, 103.83, 332) },
            { MusicalNote.Ab2, new MusicalNoteInfo(MusicalNote.Ab2, 103.83, 332) },
            { MusicalNote.A2, new MusicalNoteInfo(MusicalNote.A2, 110, 314) },
            { MusicalNote.Ad2, new MusicalNoteInfo(MusicalNote.Ad2, 116.54, 296) },
            { MusicalNote.Bb2, new MusicalNoteInfo(MusicalNote.Bb2, 116.54, 296) },
            { MusicalNote.B2, new MusicalNoteInfo(MusicalNote.B2, 123.47, 279) },
            { MusicalNote.C3, new MusicalNoteInfo(MusicalNote.C3, 130.81, 264) },
            { MusicalNote.Cd3, new MusicalNoteInfo(MusicalNote.Cd3, 138.59, 249) },
            { MusicalNote.Db3, new MusicalNoteInfo(MusicalNote.Db3, 138.59, 249) },
            { MusicalNote.D3, new MusicalNoteInfo(MusicalNote.D3, 146.83, 235) },
            { MusicalNote.Dd3, new MusicalNoteInfo(MusicalNote.Dd3, 155.56, 222) },
            { MusicalNote.Eb3, new MusicalNoteInfo(MusicalNote.Eb3, 155.56, 222) },
            { MusicalNote.E3, new MusicalNoteInfo(MusicalNote.E3, 164.81, 209) },
            { MusicalNote.F3, new MusicalNoteInfo(MusicalNote.F3, 174.61, 198) },
            { MusicalNote.Fd3, new MusicalNoteInfo(MusicalNote.Fd3, 185, 186) },
            { MusicalNote.Gb3, new MusicalNoteInfo(MusicalNote.Gb3, 185, 186) },
            { MusicalNote.G3, new MusicalNoteInfo(MusicalNote.G3, 196, 176) },
            { MusicalNote.Gd3, new MusicalNoteInfo(MusicalNote.Gd3, 207.65, 166) },
            { MusicalNote.Ab3, new MusicalNoteInfo(MusicalNote.Ab3, 207.65, 166) },
            { MusicalNote.A3, new MusicalNoteInfo(MusicalNote.A3, 220, 157) },
            { MusicalNote.Ad3, new MusicalNoteInfo(MusicalNote.Ad3, 233.08, 148) },
            { MusicalNote.Bb3, new MusicalNoteInfo(MusicalNote.Bb3, 233.08, 148) },
            { MusicalNote.B3, new MusicalNoteInfo(MusicalNote.B3, 246.94, 140) },
            { MusicalNote.C4, new MusicalNoteInfo(MusicalNote.C4, 261.63, 132) },
            { MusicalNote.Cd4, new MusicalNoteInfo(MusicalNote.Cd4, 277.18, 124) },
            { MusicalNote.Db4, new MusicalNoteInfo(MusicalNote.Db4, 277.18, 124) },
            { MusicalNote.D4, new MusicalNoteInfo(MusicalNote.D4, 293.66, 117) },
            { MusicalNote.Dd4, new MusicalNoteInfo(MusicalNote.Dd4, 311.13, 111) },
            { MusicalNote.Eb4, new MusicalNoteInfo(MusicalNote.Eb4, 311.13, 111) },
            { MusicalNote.E4, new MusicalNoteInfo(MusicalNote.E4, 329.63, 105) },
            { MusicalNote.F4, new MusicalNoteInfo(MusicalNote.F4, 349.23, 98.8) },
            { MusicalNote.Fd4, new MusicalNoteInfo(MusicalNote.Fd4, 369.99, 93.2) },
            { MusicalNote.Gb4, new MusicalNoteInfo(MusicalNote.Gb4, 369.99, 93.2) },
            { MusicalNote.G4, new MusicalNoteInfo(MusicalNote.G4, 392, 88) },
            { MusicalNote.Gd4, new MusicalNoteInfo(MusicalNote.Gd4, 415.3, 83.1) },
            { MusicalNote.Ab4, new MusicalNoteInfo(MusicalNote.Ab4, 415.3, 83.1) },
            { MusicalNote.A4, new MusicalNoteInfo(MusicalNote.A4, 440, 78.4) },
            { MusicalNote.Ad4, new MusicalNoteInfo(MusicalNote.Ad4, 466.16, 74) },
            { MusicalNote.Bb4, new MusicalNoteInfo(MusicalNote.Bb4, 466.16, 74) },
            { MusicalNote.B4, new MusicalNoteInfo(MusicalNote.B4, 493.88, 69.9) },
            { MusicalNote.C5, new MusicalNoteInfo(MusicalNote.C5, 523.25, 65.9) },
            { MusicalNote.Cd5, new MusicalNoteInfo(MusicalNote.Cd5, 554.37, 62.2) },
            { MusicalNote.Db5, new MusicalNoteInfo(MusicalNote.Db5, 554.37, 62.2) },
            { MusicalNote.D5, new MusicalNoteInfo(MusicalNote.D5, 587.33, 58.7) },
            { MusicalNote.Dd5, new MusicalNoteInfo(MusicalNote.Dd5, 622.25, 55.4) },
            { MusicalNote.Eb5, new MusicalNoteInfo(MusicalNote.Eb5, 622.25, 55.4) },
            { MusicalNote.E5, new MusicalNoteInfo(MusicalNote.E5, 659.26, 52.3) },
            { MusicalNote.F5, new MusicalNoteInfo(MusicalNote.F5, 698.46, 49.4) },
            { MusicalNote.Fd5, new MusicalNoteInfo(MusicalNote.Fd5, 739.99, 46.6) },
            { MusicalNote.Gb5, new MusicalNoteInfo(MusicalNote.Gb5, 739.99, 46.6) },
            { MusicalNote.G5, new MusicalNoteInfo(MusicalNote.G5, 783.99, 44) },
            { MusicalNote.Gd5, new MusicalNoteInfo(MusicalNote.Gd5, 830.61, 41.5) },
            { MusicalNote.Ab5, new MusicalNoteInfo(MusicalNote.Ab5, 830.61, 41.5) },
            { MusicalNote.A5, new MusicalNoteInfo(MusicalNote.A5, 880, 39.2) },
            { MusicalNote.Ad5, new MusicalNoteInfo(MusicalNote.Ad5, 932.33, 37) },
            { MusicalNote.Bb5, new MusicalNoteInfo(MusicalNote.Bb5, 932.33, 37) },
            { MusicalNote.B5, new MusicalNoteInfo(MusicalNote.B5, 987.77, 34.9) },
            { MusicalNote.C6, new MusicalNoteInfo(MusicalNote.C6, 1046.5, 33) },
            { MusicalNote.Cd6, new MusicalNoteInfo(MusicalNote.Cd6, 1108.73, 31.1) },
            { MusicalNote.Db6, new MusicalNoteInfo(MusicalNote.Db6, 1108.73, 31.1) },
            { MusicalNote.D6, new MusicalNoteInfo(MusicalNote.D6, 1174.66, 29.4) },
            { MusicalNote.Dd6, new MusicalNoteInfo(MusicalNote.Dd6, 1244.51, 27.7) },
            { MusicalNote.Eb6, new MusicalNoteInfo(MusicalNote.Eb6, 1244.51, 27.7) },
            { MusicalNote.E6, new MusicalNoteInfo(MusicalNote.E6, 1318.51, 26.2) },
            { MusicalNote.F6, new MusicalNoteInfo(MusicalNote.F6, 1396.91, 24.7) },
            { MusicalNote.Fd6, new MusicalNoteInfo(MusicalNote.Fd6, 1479.98, 23.3) },
            { MusicalNote.Gb6, new MusicalNoteInfo(MusicalNote.Gb6, 1479.98, 23.3) },
            { MusicalNote.G6, new MusicalNoteInfo(MusicalNote.G6, 1567.98, 22) },
            { MusicalNote.Gd6, new MusicalNoteInfo(MusicalNote.Gd6, 1661.22, 20.8) },
            { MusicalNote.Ab6, new MusicalNoteInfo(MusicalNote.Ab6, 1661.22, 20.8) },
            { MusicalNote.A6, new MusicalNoteInfo(MusicalNote.A6, 1760, 19.6) },
            { MusicalNote.Ad6, new MusicalNoteInfo(MusicalNote.Ad6, 1864.66, 18.5) },
            { MusicalNote.Bb6, new MusicalNoteInfo(MusicalNote.Bb6, 1864.66, 18.5) },
            { MusicalNote.B6, new MusicalNoteInfo(MusicalNote.B6, 1975.53, 17.5) },
            { MusicalNote.C7, new MusicalNoteInfo(MusicalNote.C7, 2093, 16.5) },
            { MusicalNote.Cd7, new MusicalNoteInfo(MusicalNote.Cd7, 2217.46, 15.6) },
            { MusicalNote.Db7, new MusicalNoteInfo(MusicalNote.Db7, 2217.46, 15.6) },
            { MusicalNote.D7, new MusicalNoteInfo(MusicalNote.D7, 2349.32, 14.7) },
            { MusicalNote.Dd7, new MusicalNoteInfo(MusicalNote.Dd7, 2489.02, 13.9) },
            { MusicalNote.Eb7, new MusicalNoteInfo(MusicalNote.Eb7, 2489.02, 13.9) },
            { MusicalNote.E7, new MusicalNoteInfo(MusicalNote.E7, 2637.02, 13.1) },
            { MusicalNote.F7, new MusicalNoteInfo(MusicalNote.F7, 2793.83, 12.3) },
            { MusicalNote.Fd7, new MusicalNoteInfo(MusicalNote.Fd7, 2959.96, 11.7) },
            { MusicalNote.Gb7, new MusicalNoteInfo(MusicalNote.Gb7, 2959.96, 11.7) },
            { MusicalNote.G7, new MusicalNoteInfo(MusicalNote.G7, 3135.96, 11) },
            { MusicalNote.Gd7, new MusicalNoteInfo(MusicalNote.Gd7, 3322.44, 10.4) },
            { MusicalNote.Ab7, new MusicalNoteInfo(MusicalNote.Ab7, 3322.44, 10.4) },
            { MusicalNote.A7, new MusicalNoteInfo(MusicalNote.A7, 3520, 9.8) },
            { MusicalNote.Ad7, new MusicalNoteInfo(MusicalNote.Ad7, 3729.31, 9.3) },
            { MusicalNote.Bb7, new MusicalNoteInfo(MusicalNote.Bb7, 3729.31, 9.3) },
            { MusicalNote.B7, new MusicalNoteInfo(MusicalNote.B7, 3951.07, 8.7) },
            { MusicalNote.C8, new MusicalNoteInfo(MusicalNote.C8, 4186.01, 8.2) },
            { MusicalNote.Cd8, new MusicalNoteInfo(MusicalNote.Cd8, 4434.92, 7.8) },
            { MusicalNote.Db8, new MusicalNoteInfo(MusicalNote.Db8, 4434.92, 7.8) },
            { MusicalNote.D8, new MusicalNoteInfo(MusicalNote.D8, 4698.64, 7.3) },
            { MusicalNote.Dd8, new MusicalNoteInfo(MusicalNote.Dd8, 4978.03, 6.9) },
            { MusicalNote.Eb8, new MusicalNoteInfo(MusicalNote.Eb8, 4978.03, 6.9) }
        };

        public static MusicalNoteInfo GetMusicalNoteInfo(MusicalNote musicalNote)
        {
            return MusicalNotes.ContainsKey(musicalNote)
                ? MusicalNotes[musicalNote]
                : MusicalNoteInfo.Empty;
        }

        /// <summary>
        /// Returns the frequency of a musical note.
        /// </summary>
        /// <param name="musicalNote">The musical note for which to return the frequency.</param>
        /// <returns>The frequency of the musical note.</returns>
        public static double GetFrequency(MusicalNote musicalNote)
        {
            return MusicalNotes.ContainsKey(musicalNote)
                ? MusicalNotes[musicalNote].Frequency
                : 0;
        }

        /// <summary>
        /// Returns the wavelength of a musical note.
        /// </summary>
        /// <param name="musicalNote">The musical note for which to return the wavelength.</param>
        /// <returns>The wavelength of the musical note.</returns>
        public static double GetWavelength(MusicalNote musicalNote)
        {
            return MusicalNotes.ContainsKey(musicalNote)
                ? MusicalNotes[musicalNote].Wavelength
                : 0;
        }

        /// <summary>
        /// Playes a musical note at the speaker.
        /// </summary>
        /// <param name="musicalNote">The musical note to be played.</param>
        /// <param name="duration">The time duration in milliseconds for which to play the note.</param>
        public static void Play(MusicalNote musicalNote, int duration)
        {
            double frequency = GetFrequency(musicalNote);
            int frequencyInteger = Convert.ToInt32(frequency);
            Console.Beep(frequencyInteger, duration);
        }
    }
}