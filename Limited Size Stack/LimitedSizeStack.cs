using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        private readonly LinkedList<T> stack = new LinkedList<T>();
        private readonly int limitation;

        public LimitedSizeStack(int limit)
        {
            limitation = limit;
        }

        public void Push(T item)
        {
            if (limitation != 0)
            {
                if (stack.Count == limitation)
                    stack.RemoveFirst();
                stack.AddLast(item);
            }
        }

        public T Pop()
        {
            var result = stack.Last.Value;
            stack.RemoveLast();
            return result;
        }

        public int Count => stack.Count;
    }
}