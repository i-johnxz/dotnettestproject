using System;
using System.Collections.Generic;
using System.Text;

namespace UseMethodChaining
{
    public static class ListExtensions
    {
        public static List<T> FluentAdd<T>(this List<T> list, T item)
        {
            list.Add(item);
            return list;
        }

        public static List<T> FluentClear<T>(this List<T> list)
        {
            list.Clear();
            return list;
        }

        public static List<T> FluentForEach<T>(this List<T> list, Action<T> action)
        {
            list.ForEach(action);
            return list;
        }

        public static List<T> FluentInsert<T>(this List<T> list, int index, T item)
        {
            list.Insert(index, item);
            return list;
        }

        public static List<T> FluentRemoveAt<T>(this List<T> list, int index)
        {
            list.RemoveAt(index);
            return list;
        }

        public static List<T> FluentReverse<T>(this List<T> list)
        {
            list.Reverse();
            return list;
        }
    }
}
