using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DnDSimulator.Bootstrapper;
using DnDSimulator.Character;
using DnDSimulator.Interfaces;

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

            Console.WriteLine("Press ESC to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                    using (var scope = container.BeginLifetimeScope())
                    {
                        try
                        {
                            var encounter = scope.Resolve<IEncounter>();

                            var blueFaction = scope.Resolve<IFaction>();
                            var redFaction = scope.Resolve<IFaction>();

                            //Put N lvl 5 actors into their own group and add that group to the appropriate faction.
                            for (var i = 0; i < 1; i++)
                            {
                                var blueGroup = scope.Resolve<IActorGroup>();
                                blueGroup.Add(await GetRandomActor(scope, 5, "Sir Reginald"));
                                blueFaction.Participants.Add(blueGroup);

                                var redGroup = scope.Resolve<IActorGroup>();
                                redGroup.Add(await GetRandomActor(scope, 5, "Nigel"));
                                redFaction.Participants.Add(redGroup);
                            }

                            encounter.Factions.Add(blueFaction);
                            encounter.Factions.Add(redFaction);

                            await encounter.StartEncounter();

                            //Console.WriteLine(ObjectDumper.Dump(encounter));

                            var winner = await encounter.RunEncounter();

                            //Console.WriteLine(ObjectDumper.Dump(encounter));

                            Console.WriteLine($"{string.Join(", ", winner.Participants.SelectMany(p => p.Select(a => a.Name)))} Wins!");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(value: e);
                            throw;
                        }
                    }
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static async Task<IActor> GetRandomActor(ILifetimeScope scope, int characterLevel, string name)
        {
            // This is an oversimplification for testing purposes... Don't take it too seriously. Or work too hard on it.
            var actorFactory = scope.Resolve<Actor.Factory>();
            var proficiency = (characterLevel - 1) / 4 + 2; //2 to 6, increases by 1 every 4th level.
            var statsDice = GetStatisticsDice(scope);

            var strength = await statsDice.RollAndTakeHighestAsync(3);
            var dexterity = await statsDice.RollAndTakeHighestAsync(3);
            var constitution = await statsDice.RollAndTakeHighestAsync(3);
            var intelligence = await statsDice.RollAndTakeHighestAsync(3);
            var wisdom = await statsDice.RollAndTakeHighestAsync(3);
            var charisma = await statsDice.RollAndTakeHighestAsync(3);

            var hitPoints = await GetHitPoints(scope, constitution, characterLevel);

            return actorFactory(
                name: name,
                armorClass: GetPossibleArmorClass(strength, dexterity),
                currentHitPoints: hitPoints,
                maxHitPoints: hitPoints,
                temporaryHitPoints: 0,
                proficiency: proficiency,
                strength: strength,
                dexterity: dexterity,
                constitution: constitution,
                intelligence: intelligence,
                wisdom: wisdom,
                charisma: charisma
                );
        }

        private static async Task<int> GetHitPoints(ILifetimeScope scope, int constitution, int characterLevel)
        {
            var con = scope.Resolve<Constitution.Factory>()(constitution);
            var hitDie = scope.Resolve<Die.Factory>()(8, con.Modifier, false);
            var hp = 8 + con.Modifier;
            for (var i = 2; i <= characterLevel; i++)
            {
                hp += Math.Max(await hitDie.RollAsync(), 1);
            }

            return hp;
        }

        // ReSharper disable once ConvertIfStatementToReturnStatement
        private static int GetPossibleArmorClass(int strength, int dexterity)
        {
            //This assumes the character has access to heavy armor.
            if (strength >= 15) return 20; //Plate and a Shield.
            if (dexterity >= 14) return 19; //Half-plate and a Shield.
            if (strength >= 13) return 18; //Chain Mail and a Shield
            return 16; //Ring mail and a Shield.
        }

        private static IDice GetStatisticsDice(ILifetimeScope scope)
        {
            var dieMaker = scope.Resolve<Die.Factory>();
            var statsDice = scope.Resolve<IDice>();
            for (var i = 0; i < 4; i++)
            {
                statsDice.Add(
                    dieMaker(
                        numberOfSides: 6,
                        bonusToResult: 0,
                        alwaysRollsAverage: false
                    )
                );
            }
            return statsDice;
        }

    }
}