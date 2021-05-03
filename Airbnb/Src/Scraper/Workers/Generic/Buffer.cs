using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Airbnb.Scraper.Workers.Generic
{
    public class Buffer<T>
    {
        private int capacity;
        private T[] buffer;
        private int start;
        private int end;

        public Buffer(int capacity)
        {
            this.capacity = capacity;
            this.buffer = new T[capacity];
            start = 0;
            end = -1;

            full = new Semaphore(0, capacity);
            empty = new Semaphore(capacity, capacity);
        }

        public T Read()
        {
            if (end < 0)
                throw new Exception("Empty buffer");

            var val = buffer[start];
            start = ++start % capacity;

            return val;
        }

        public void Write(T val)
        {
            end = ++end % capacity;
            buffer[end] = val;
        }

        public void Set(ref object wrt, ref Semaphore full, ref Semaphore empty)
        {
            wrt = this.wrt;
            full = this.full;
            empty = this.empty;
        }

        private object wrt = new object();
        private Semaphore full;
        private Semaphore empty;
    }
}
