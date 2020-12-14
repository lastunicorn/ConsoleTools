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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DustInTheWind.ConsoleTools.Controls.Tables
{
    internal class DataGridBuilderFromObject
    {
        private readonly IEnumerable<MemberInfo> members;

        public DataGrid DataGrid { get; }

        public DataGridBuilderFromObject(Type type)
        {
            DataGrid = new DataGrid(type.Name);

            members = EnumerateMembers(type);

            foreach (MemberInfo memberInfo in members)
                DataGrid.Columns.Add(memberInfo.Name);
        }

        private static IEnumerable<MemberInfo> EnumerateMembers(IReflect type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);

            foreach (FieldInfo fieldInfo in fields)
                yield return fieldInfo;

            IEnumerable<PropertyInfo> properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.CanRead);

            foreach (PropertyInfo propertyInfo in properties)
                yield return propertyInfo;
        }

        public void Add(IEnumerable items)
        {
            foreach (object item in items)
                Add(item);
        }

        public void Add(object item)
        {
            if (item == null)
                return;

            NormalRow normalRow = new NormalRow();

            foreach (MemberInfo memberInfo in members)
            {
                switch (memberInfo)
                {
                    case FieldInfo fieldInfo:
                        {
                            object value = fieldInfo.GetValue(item);
                            normalRow.AddCell(value);
                            break;
                        }
                    case PropertyInfo propertyInfo:
                        {
                            object value = propertyInfo.GetValue(item);
                            normalRow.AddCell(value);
                            break;
                        }
                }
            }

            DataGrid.Rows.Add(normalRow);
        }
    }
}