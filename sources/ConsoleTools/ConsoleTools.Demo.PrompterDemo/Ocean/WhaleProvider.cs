﻿// ConsoleTools
// Copyright (C) 2017-2024 Dust in the Wind
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

using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Demo.PrompterDemo.Ocean
{
    internal class WhaleProvider
    {
        public IEnumerable<Whale> CreateWhales()
        {
            return new List<Whale>
            {
                new Whale
                {
                    Name = "Blue whale",
                    Count = "10,000-25,000",
                    Weight = "50-150 tonnes"
                },
                new Whale
                {
                    Name = "Humpback whale",
                    Count = "80,000",
                    Weight = "25–30 tonnes"
                },
                new Whale
                {
                    Name = "Killer whale",
                    Count = "100,000",
                    Weight = "4.5 tonnes"
                },
                new Whale
                {
                    Name = "Beluga",
                    Count = "100,000",
                    Weight = "1.5 tonnes"
                },
                new Whale
                {
                    Name = "Narwhal",
                    Count = "25,000",
                    Weight = "900-1,500 kilograms"
                },
                new Whale
                {
                    Name = "Sperm whale",
                    Count = "200,000–2,000,000",
                    Weight = "25–50 tonnes"
                }
            };
        }
    }
}