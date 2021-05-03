using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper.Workers.Generic
{
    public class Consumer<T> : Worker<T>
    {
        private static object mutex;
        private static int read;

        public Consumer(Buffer<T> buffer) : base(buffer)
        {
        }

        public void Start(Action<T> handle)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    Wait(mutex);
                    read++;
                    if (read == 1)
                        Wait(wrt);
                    Signal(mutex);

                    Consume(handle);

                    Wait(mutex);
                    --read;
                    if (read == 0)
                        Signal(wrt);
                    Signal(mutex);
                }
            });
        }

        private void Consume(Action<T> handle)
        {
            Wait(full);
            Wait(mutex);
            //Console.WriteLine("Consume: " + buffer.Read());
            handle(buffer.Read());
            Signal(mutex);
            Signal(empty);
        }
    }
}
