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

            

            var chairs = new List<Chair>();

            for (int i = 0; i < 100; i++)
            {
                chairs.Add(new Chair());
                cutSeatFilter.Queue(chairs.Last());
            }


            assembleFeetFilter.ConnectInput(cutSeatFilter);
            assembleBackrestFilter.ConnectInput(assembleFeetFilter);
            assembleStabilizerFilter.ConnectInput(assembleBackrestFilter);
            packageChairFilter.ConnectInput(assembleStabilizerFilter);


            var task = cutSeatFilter.Work();

            var task2 = assembleFeetFilter.Work();

            var task3 = assembleBackrestFilter.Work();
            var task4 = assembleStabilizerFilter.Work();

            var task5 = packageChairFilter.Work();

            await Task.WhenAll(task, task2, task3, task4,task5);

            chairs.ForEach(chair => Console.WriteLine(chair));

        }
    }
}