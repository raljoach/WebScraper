using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Airbnb.Scraper.Workers.Generic
{
    public abstract class Worker<T>
    {
        protected Buffer<T> buffer;
        protected object wrt;
        protected Semaphore full;
        protected Semaphore empty;

        public Worker(Buffer<T> buffer)
        {
            this.buffer = buffer;
            buffer.Set(ref wrt, ref full, ref empty);
        }

        protected void Signal(object region)
        {
            Monitor.Exit(region);
        }

        protected void Wait(object region)
        {
            Monitor.Enter(region);
        }
    }
}
