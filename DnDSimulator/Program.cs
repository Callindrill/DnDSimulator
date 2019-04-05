using System;
using System.Threading.Tasks;
using Autofac;
using DnDSimulator.Bootstrapper;
using DnDSimulator.Character;

namespace DnDSimulator
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DiceModule());
            builder.RegisterModule(new CharacterModule());
            builder.RegisterModule(new EncounterModule());

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                try
                {
                    //var die = scope.Resolve<Die.Factory>();

                    //Console.WriteLine("Press ESC to stop");
                    //do
                    //{
                    //    while (!Console.KeyAvailable)
                    //    {
                    //        Console.WriteLine(await die(20, 0, false).RollAsync());
                    //        await Task.Delay(1000);
                    //    }
                    //} while (Console.ReadKey(true).Key != ConsoleKey.Escape);
                    var actorFactory = scope.Resolve<Actor.Factory>();

                    var actor = actorFactory(
                        18,
                        60,
                        60,
                        3,
                        17,
                        15,
                        10,
                        10,
                        20,
                        20
                    );

                    Console.Write(ObjectDumper.Dump(actor));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}