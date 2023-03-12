using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipesFactory
{
    internal class Pipe
    {
        private readonly Filter filter1;
        private readonly Filter filter2;


        public Pipe(Filter filter1, Filter filter2)
        {
            this.filter1 = filter1;
            this.filter2 = filter2;
        }

        public async Task Start()
        {
            filter1.Work();

            filter2.Work();

            while (!Filter.Stopped)
            {
                Chair? toFeed;

                while (!filter1.TryDequeue(out toFeed))
                {
                    await Task.Delay(10);
                }

                filter2.Queue(toFeed);
            }
        }
    }
}
