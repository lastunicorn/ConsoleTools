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

            DataRow dataRow = new DataRow();

            foreach (MemberInfo memberInfo in members)
            {
                switch (memberInfo)
                {
                    case FieldInfo fieldInfo:
                        {
                            object value = fieldInfo.GetValue(item);
                            dataRow.AddCell(value);
                            break;
                        }
                    case PropertyInfo propertyInfo:
                        {
                            object value = propertyInfo.GetValue(item);
                            dataRow.AddCell(value);
                            break;
                        }
                }
            }

            DataGrid.Rows.Add(dataRow);
        }
    }
}