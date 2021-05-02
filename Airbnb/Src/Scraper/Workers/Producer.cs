using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper.Workers
{
    public class Producer<T> : Worker<T>
    {
        private static object mutex;
        //private static Random random = new Random();

        public Producer(Buffer<T> buffer) : base(buffer)
        {
        }        

        /*public void Start()
        {
            Wait(wrt);
            Produce();
            Signal(wrt);
        }*/

        public void Add(T item)
        {
            Wait(empty);
            Wait(mutex);
            buffer.Write(item);
            Signal(mutex);
            Signal(full);
        }
    }
}
