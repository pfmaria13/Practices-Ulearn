using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace rocket_bot
{
    public class Channel<T> where T : class
    {
        private readonly List<T> list = new List<T>();

        public T this[int index]
        {
            get
            {
                lock (list)
                {
                    if (index < 0) return null;
                    return index < list.Count ? list[index] : null;
                }
            }
            set
            {
                lock (list)
                {
                    if (list.Count != index)
                        list.RemoveRange(index, list.Count - index);
                    list.Add(value);
                }
            }
        }

        public T LastItem()
        {
            lock (list)
            {
                return list.Count > 0 ? list.Last() : null;
            }
        }

        public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
        {
            lock (list)
            {
                if (Equals(LastItem(), knownLastItem))
                    list.Add(item);
            }
        }

        public int Count
        {
            get
            {
                lock (list)
                {
                    return list.Count;
                }
            }
        }
    }
}