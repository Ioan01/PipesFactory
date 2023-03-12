namespace PipesFactory
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Filter cutSeatFilter = new Filter("cut seat", chair => chair.CutSeat = true,1000);

            Filter assembleFeetFilter = new Filter("assemble feet", chair => chair.AssembledFeet = true,2000);

            Filter assembleBackrestFilter =
                new Filter("assemble backrest", chair => chair.AssembledBackrest = true, 1000);

            Filter assembleStabilizerFilter =
                new Filter("assemble stabilizer", chair => chair.AssembledStabilizerBar = true,1000);

            Filter packageChairFilter = new Filter("package chair", chair => chair.Packaged = true,1);

            Pipe pipe1 = new Pipe(cutSeatFilter, assembleFeetFilter);

            Pipe pipe2 = new Pipe(assembleFeetFilter, assembleBackrestFilter);

            Pipe pipe3 = new Pipe(assembleBackrestFilter, assembleStabilizerFilter);

            Pipe pipe4 = new Pipe(assembleStabilizerFilter, packageChairFilter);

            var chairs = new List<Chair>();

            for (int i = 0; i < 100; i++)
            {
                chairs.Add(new Chair());
                cutSeatFilter.Queue(chairs.Last());
            }

            var task = pipe1.Start();

            var task2 = pipe2.Start();

            var task3 = pipe3.Start();
            var task4 = pipe4.Start();

            await Task.WhenAll(task, task2, task3, task4);

            chairs.ForEach(chair => Console.WriteLine(chair));

        }
    }
}