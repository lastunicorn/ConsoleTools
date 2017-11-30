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

namespace DustInTheWind.ConsoleTools.Musical
{
    public static class Sound
    {
        /// <summary>
        /// Retuns the frequency of a musical note.
        /// </summary>
        /// <param name="note">The musical not for which to return the frequency.</param>
        /// <returns>The frewuency of the musical note.</returns>
        public static double GetFrequency(MusicalNote note)
        {
            switch (note)
            {
                case MusicalNote.C0:
                    return 16.35;

                case MusicalNote.Cd0:
                case MusicalNote.Db0:
                    return 17.32;

                case MusicalNote.D0:
                    return 18.35;

                case MusicalNote.Dd0:
                case MusicalNote.Eb0:
                    return 19.45;

                case MusicalNote.E0:
                    return 20.60;

                case MusicalNote.F0:
                    return 21.83;

                case MusicalNote.Fd0:
                case MusicalNote.Gb0:
                    return 23.12;

                case MusicalNote.G0:
                    return 24.50;

                case MusicalNote.Gd0:
                case MusicalNote.Ab0:
                    return 25.96;

                case MusicalNote.A0:
                    return 27.50;

                case MusicalNote.Ad0:
                case MusicalNote.Bb0:
                    return 29.14;

                case MusicalNote.B0:
                    return 30.87;

                case MusicalNote.C1:
                    return 32.70;

                case MusicalNote.Cd1:
                case MusicalNote.Db1:
                    return 34.65;

                case MusicalNote.D1:
                    return 36.71;

                case MusicalNote.Dd1:
                case MusicalNote.Eb1:
                    return 38.89;

                case MusicalNote.E1:
                    return 41.20;

                case MusicalNote.F1:
                    return 43.65;

                case MusicalNote.Fd1:
                case MusicalNote.Gb1:
                    return 46.25;

                case MusicalNote.G1:
                    return 49.00;

                case MusicalNote.Gd1:
                case MusicalNote.Ab1:
                    return 51.91;

                case MusicalNote.A1:
                    return 55.00;

                case MusicalNote.Ad1:
                case MusicalNote.Bb1:
                    return 58.27;

                case MusicalNote.B1:
                    return 61.74;

                case MusicalNote.C2:
                    return 65.41;

                case MusicalNote.Cd2:
                case MusicalNote.Db2:
                    return 69.30;

                case MusicalNote.D2:
                    return 73.42;

                case MusicalNote.Dd2:
                case MusicalNote.Eb2:
                    return 77.78;

                case MusicalNote.E2:
                    return 82.41;

                case MusicalNote.F2:
                    return 87.31;

                case MusicalNote.Fd2:
                case MusicalNote.Gb2:
                    return 92.50;

                case MusicalNote.G2:
                    return 98.00;

                case MusicalNote.Gd2:
                case MusicalNote.Ab2:
                    return 103.83;

                case MusicalNote.A2:
                    return 110.00;

                case MusicalNote.Ad2:
                case MusicalNote.Bb2:
                    return 116.54;

                case MusicalNote.B2:
                    return 123.47;

                case MusicalNote.C3:
                    return 130.81;

                case MusicalNote.Cd3:
                case MusicalNote.Db3:
                    return 138.59;

                case MusicalNote.D3:
                    return 146.83;

                case MusicalNote.Dd3:
                case MusicalNote.Eb3:
                    return 155.56;

                case MusicalNote.E3:
                    return 164.81;

                case MusicalNote.F3:
                    return 174.61;

                case MusicalNote.Fd3:
                case MusicalNote.Gb3:
                    return 185.00;

                case MusicalNote.G3:
                    return 196.00;

                case MusicalNote.Gd3:
                case MusicalNote.Ab3:
                    return 207.65;

                case MusicalNote.A3:
                    return 220.00;

                case MusicalNote.Ad3:
                case MusicalNote.Bb3:
                    return 233.08;

                case MusicalNote.B3:
                    return 246.94;

                case MusicalNote.C4:
                    return 261.63;

                case MusicalNote.Cd4:
                case MusicalNote.Db4:
                    return 277.18;

                case MusicalNote.D4:
                    return 293.66;

                case MusicalNote.Dd4:
                case MusicalNote.Eb4:
                    return 311.13;

                case MusicalNote.E4:
                    return 329.63;

                case MusicalNote.F4:
                    return 349.23;

                case MusicalNote.Fd4:
                case MusicalNote.Gb4:
                    return 369.99;

                case MusicalNote.G4:
                    return 392.00;

                case MusicalNote.Gd4:
                case MusicalNote.Ab4:
                    return 415.30;

                case MusicalNote.A4:
                    return 440.00;

                case MusicalNote.Ad4:
                case MusicalNote.Bb4:
                    return 466.16;

                case MusicalNote.B4:
                    return 493.88;

                case MusicalNote.C5:
                    return 523.25;

                case MusicalNote.Cd5:
                case MusicalNote.Db5:
                    return 554.37;

                case MusicalNote.D5:
                    return 587.33;

                case MusicalNote.Dd5:
                case MusicalNote.Eb5:
                    return 622.25;

                case MusicalNote.E5:
                    return 659.26;

                case MusicalNote.F5:
                    return 698.46;

                case MusicalNote.Fd5:
                case MusicalNote.Gb5:
                    return 739.99;

                case MusicalNote.G5:
                    return 783.99;

                case MusicalNote.Gd5:
                case MusicalNote.Ab5:
                    return 830.61;

                case MusicalNote.A5:
                    return 880.00;

                case MusicalNote.Ad5:
                case MusicalNote.Bb5:
                    return 932.33;

                case MusicalNote.B5:
                    return 987.77;

                case MusicalNote.C6:
                    return 1046.50;

                case MusicalNote.Cd6:
                case MusicalNote.Db6:
                    return 1108.73;

                case MusicalNote.D6:
                    return 1174.66;

                case MusicalNote.Dd6:
                case MusicalNote.Eb6:
                    return 1244.51;

                case MusicalNote.E6:
                    return 1318.51;

                case MusicalNote.F6:
                    return 1396.91;

                case MusicalNote.Fd6:
                case MusicalNote.Gb6:
                    return 1479.98;

                case MusicalNote.G6:
                    return 1567.98;

                case MusicalNote.Gd6:
                case MusicalNote.Ab6:
                    return 1661.22;

                case MusicalNote.A6:
                    return 1760.00;

                case MusicalNote.Ad6:
                case MusicalNote.Bb6:
                    return 1864.66;

                case MusicalNote.B6:
                    return 1975.53;

                case MusicalNote.C7:
                    return 2093.00;

                case MusicalNote.Cd7:
                case MusicalNote.Db7:
                    return 2217.46;

                case MusicalNote.D7:
                    return 2349.32;

                case MusicalNote.Dd7:
                case MusicalNote.Eb7:
                    return 2489.02;

                case MusicalNote.E7:
                    return 2637.02;

                case MusicalNote.F7:
                    return 2793.83;

                case MusicalNote.Fd7:
                case MusicalNote.Gb7:
                    return 2959.96;

                case MusicalNote.G7:
                    return 3135.96;

                case MusicalNote.Gd7:
                case MusicalNote.Ab7:
                    return 3322.44;

                case MusicalNote.A7:
                    return 3520.00;

                case MusicalNote.Ad7:
                case MusicalNote.Bb7:
                    return 3729.31;

                case MusicalNote.B7:
                    return 3951.07;

                case MusicalNote.C8:
                    return 4186.01;

                case MusicalNote.Cd8:
                case MusicalNote.Db8:
                    return 4434.92;

                case MusicalNote.D8:
                    return 4698.64;

                case MusicalNote.Dd8:
                case MusicalNote.Eb8:
                    return 4978.03;

                default:
                    return 0;
            }
        }
        
        /// <summary>
        /// Retuns the wavelength of a musical note.
        /// </summary>
        /// <param name="note">The musical not for which to return the wavelength.</param>
        /// <returns>The wavelength of the musical note.</returns>
        public static double GetWavelength(MusicalNote note)
        {
            switch (note)
            {
                case MusicalNote.C0:
                    return 2100;

                case MusicalNote.Cd0:
                case MusicalNote.Db0:
                    return 1990;

                case MusicalNote.D0:
                    return 1870;

                case MusicalNote.Dd0:
                case MusicalNote.Eb0:
                    return 1770;

                case MusicalNote.E0:
                    return 1670;

                case MusicalNote.F0:
                    return 1580;

                case MusicalNote.Fd0:
                case MusicalNote.Gb0:
                    return 1490;

                case MusicalNote.G0:
                    return 1400;

                case MusicalNote.Gd0:
                case MusicalNote.Ab0:
                    return 1320;

                case MusicalNote.A0:
                    return 1250;

                case MusicalNote.Ad0:
                case MusicalNote.Bb0:
                    return 1180;

                case MusicalNote.B0:
                    return 1110;

                case MusicalNote.C1:
                    return 1050;

                case MusicalNote.Cd1:
                case MusicalNote.Db1:
                    return 996;

                case MusicalNote.D1:
                    return 940;

                case MusicalNote.Dd1:
                case MusicalNote.Eb1:
                    return 887;

                case MusicalNote.E1:
                    return 837;

                case MusicalNote.F1:
                    return 790;

                case MusicalNote.Fd1:
                case MusicalNote.Gb1:
                    return 746;

                case MusicalNote.G1:
                    return 704;

                case MusicalNote.Gd1:
                case MusicalNote.Ab1:
                    return 665;

                case MusicalNote.A1:
                    return 627;

                case MusicalNote.Ad1:
                case MusicalNote.Bb1:
                    return 592;

                case MusicalNote.B1:
                    return 559;

                case MusicalNote.C2:
                    return 527;

                case MusicalNote.Cd2:
                case MusicalNote.Db2:
                    return 498;

                case MusicalNote.D2:
                    return 470;

                case MusicalNote.Dd2:
                case MusicalNote.Eb2:
                    return 444;

                case MusicalNote.E2:
                    return 419;

                case MusicalNote.F2:
                    return 395;

                case MusicalNote.Fd2:
                case MusicalNote.Gb2:
                    return 373;

                case MusicalNote.G2:
                    return 352;

                case MusicalNote.Gd2:
                case MusicalNote.Ab2:
                    return 332;

                case MusicalNote.A2:
                    return 314;

                case MusicalNote.Ad2:
                case MusicalNote.Bb2:
                    return 296;

                case MusicalNote.B2:
                    return 279;

                case MusicalNote.C3:
                    return 264;

                case MusicalNote.Cd3:
                case MusicalNote.Db3:
                    return 249;

                case MusicalNote.D3:
                    return 235;

                case MusicalNote.Dd3:
                case MusicalNote.Eb3:
                    return 222;

                case MusicalNote.E3:
                    return 209;

                case MusicalNote.F3:
                    return 198;

                case MusicalNote.Fd3:
                case MusicalNote.Gb3:
                    return 186;

                case MusicalNote.G3:
                    return 176;

                case MusicalNote.Gd3:
                case MusicalNote.Ab3:
                    return 166;

                case MusicalNote.A3:
                    return 157;

                case MusicalNote.Ad3:
                case MusicalNote.Bb3:
                    return 148;

                case MusicalNote.B3:
                    return 140;

                case MusicalNote.C4:
                    return 132;

                case MusicalNote.Cd4:
                case MusicalNote.Db4:
                    return 124;

                case MusicalNote.D4:
                    return 117;

                case MusicalNote.Dd4:
                case MusicalNote.Eb4:
                    return 111;

                case MusicalNote.E4:
                    return 105;

                case MusicalNote.F4:
                    return 98.8;

                case MusicalNote.Fd4:
                case MusicalNote.Gb4:
                    return 93.2;

                case MusicalNote.G4:
                    return 88.0;

                case MusicalNote.Gd4:
                case MusicalNote.Ab4:
                    return 83.1;

                case MusicalNote.A4:
                    return 78.4;

                case MusicalNote.Ad4:
                case MusicalNote.Bb4:
                    return 74.0;

                case MusicalNote.B4:
                    return 69.9;

                case MusicalNote.C5:
                    return 65.9;

                case MusicalNote.Cd5:
                case MusicalNote.Db5:
                    return 62.2;

                case MusicalNote.D5:
                    return 58.7;

                case MusicalNote.Dd5:
                case MusicalNote.Eb5:
                    return 55.4;

                case MusicalNote.E5:
                    return 52.3;

                case MusicalNote.F5:
                    return 49.4;

                case MusicalNote.Fd5:
                case MusicalNote.Gb5:
                    return 46.6;

                case MusicalNote.G5:
                    return 44.0;

                case MusicalNote.Gd5:
                case MusicalNote.Ab5:
                    return 41.5;

                case MusicalNote.A5:
                    return 39.2;

                case MusicalNote.Ad5:
                case MusicalNote.Bb5:
                    return 37.0;

                case MusicalNote.B5:
                    return 34.9;

                case MusicalNote.C6:
                    return 33.0;

                case MusicalNote.Cd6:
                case MusicalNote.Db6:
                    return 31.1;

                case MusicalNote.D6:
                    return 29.4;

                case MusicalNote.Dd6:
                case MusicalNote.Eb6:
                    return 27.7;

                case MusicalNote.E6:
                    return 26.2;

                case MusicalNote.F6:
                    return 24.7;

                case MusicalNote.Fd6:
                case MusicalNote.Gb6:
                    return 23.3;

                case MusicalNote.G6:
                    return 22.0;

                case MusicalNote.Gd6:
                case MusicalNote.Ab6:
                    return 20.8;

                case MusicalNote.A6:
                    return 19.6;

                case MusicalNote.Ad6:
                case MusicalNote.Bb6:
                    return 18.5;

                case MusicalNote.B6:
                    return 17.5;

                case MusicalNote.C7:
                    return 16.5;

                case MusicalNote.Cd7:
                case MusicalNote.Db7:
                    return 15.6;

                case MusicalNote.D7:
                    return 14.7;

                case MusicalNote.Dd7:
                case MusicalNote.Eb7:
                    return 13.9;

                case MusicalNote.E7:
                    return 13.1;

                case MusicalNote.F7:
                    return 12.3;

                case MusicalNote.Fd7:
                case MusicalNote.Gb7:
                    return 11.7;

                case MusicalNote.G7:
                    return 11.0;

                case MusicalNote.Gd7:
                case MusicalNote.Ab7:
                    return 10.4;

                case MusicalNote.A7:
                    return 9.8;

                case MusicalNote.Ad7:
                case MusicalNote.Bb7:
                    return 9.3;

                case MusicalNote.B7:
                    return 8.7;

                case MusicalNote.C8:
                    return 8.2;

                case MusicalNote.Cd8:
                case MusicalNote.Db8:
                    return 7.8;

                case MusicalNote.D8:
                    return 7.3;

                case MusicalNote.Dd8:
                case MusicalNote.Eb8:
                    return 6.9;

                default:
                    return 0;
            }
        }
        
        /// <summary>
        /// Playes a musical not at the speaker.
        /// </summary>
        /// <param name="note">The musical to be played.</param>
        /// <param name="duration">The time duration in milliseconds for which to play the note.</param>
        public static void Play(MusicalNote note, int duration)
        {
            double frequency = GetFrequency(note);
            int frequencyInteger = Convert.ToInt32(frequency);
            Console.Beep(frequencyInteger, duration);
        }
    }
}
