// ConsoleTools
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

using DustInTheWind.ConsoleTools.Controls.Tables;
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.DataGridDemo.Demo.TitleDemo;

internal class MultilineTitleDemo : DemoBase
{
    public override string Title => "Multiline Title";

    protected override void DoExecute()
    {
        DataGrid dataGrid = new()
        {
            Title = "Title can be split on multiple lines.\rAny of the characters:\nCR (\\r), LF (\\n) or CRLF (\\r\\n)\r\ncan be used to mark the end of a line."
        };

        dataGrid.Rows.Add(1.ToString(), "First item");
        dataGrid.Rows.Add(2.ToString(), "Second item");
        dataGrid.Rows.Add(3.ToString(), "Third item");
        dataGrid.Rows.Add(4.ToString(), "Forth item");
        dataGrid.Rows.Add(5.ToString(), "Fifth item");

        dataGrid.Display();
    }
}