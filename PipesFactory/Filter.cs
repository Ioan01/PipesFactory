using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipesFactory
{
    internal class Filter
    {
        public static bool Stopped { get; set; }


        private readonly string _name;
        private readonly Action<Chair> _doAction;
        private readonly int delay;

        private ConcurrentQueue<Chair> _inQueue = new ConcurrentQueue<Chair>();

        private ConcurrentQueue<Chair> _outQueue = new ConcurrentQueue<Chair>();

        public async Task Work()
        {
            while (!Stopped)
            {

                Chair? toWorkOn;
                while (!_inQueue.TryDequeue(out toWorkOn))
                {
                    await Task.Delay(10);

                    //Console.WriteLine($"{_name}");
                }


                //Console.WriteLine($"{_name} started action on {toWorkOn.Id}.");
                _doAction(toWorkOn);

                await Task.Delay(delay);

                _outQueue.Enqueue(toWorkOn);

                Console.WriteLine($"{_name} ended action on {toWorkOn.Id}.");

            }

        }

        public Filter(string name, Action<Chair> doAction, int delay)
        {
            _name = name;
            this._doAction = doAction;
            this.delay = delay;
        }

        public void Queue(Chair toWorkOn)
        {
            _inQueue.Enqueue(toWorkOn);
        }

        public void ConnectInput(Filter filter)
        {
            _inQueue = filter._outQueue;
        }

        public void ConnectOutput(Filter filter)
        {
            filter._inQueue = _outQueue;
        }
    }
}
