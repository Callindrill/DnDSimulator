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
            builder.RegisterModule(module: new DiceModule());
            builder.RegisterModule(module: new CharacterModule());
            builder.RegisterModule(module: new EncounterModule());

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
                        armorClass: 18,
                        currentHitPoints: 60,
                        maxHitPoints: 60,
                        temporaryHitPoints:0,
                        proficiency: 3,
                        strength: 17,
                        dexterity: 15,
                        constitution: 10,
                        intelligence: 10,
                        wisdom: 20,
                        charisma: 20
                    );

                    Console.Write(value: ObjectDumper.Dump(element: actor));
                }
                catch (Exception e)
                {
                    Console.WriteLine(value: e);
                    throw;
                }

                Console.WriteLine(value: "Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}