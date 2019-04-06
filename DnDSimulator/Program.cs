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

            using (var scope = container.BeginLifetimeScope())
            {
                try
                {
                    var encounter = scope.Resolve<IEncounter>();

                    var blueFaction = scope.Resolve<IFaction>();
                    var blueGroup = scope.Resolve<IActorGroup>();

                    var redFaction = scope.Resolve<IFaction>();
                    var redGroup = scope.Resolve<IActorGroup>();

                    //Put 4 lvl 5 actors in each group.
                    for (var i = 0; i <= 4; i++)
                    {
                        blueGroup.Add(await GetRandomActor(scope, 5));
                        redGroup.Add(await GetRandomActor(scope, 5));
                    }

                    blueFaction.Participants.Add(blueGroup);
                    redFaction.Participants.Add(redGroup);

                    encounter.Combatants.Add(blueFaction);
                    encounter.Combatants.Add(redFaction);
                    Console.WriteLine(ObjectDumper.Dump(encounter));

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

        private static async Task<IActor> GetRandomActor(ILifetimeScope scope, int characterLevel)
        {
            // This is an oversimplification for testing purposes... Don't take it too seriously. Or work too hard on it.

            using (var workerScope = scope.BeginLifetimeScope())
            {
                var actorFactory = workerScope.Resolve<Actor.Factory>();
                var proficiency = (characterLevel - 1) / 4 + 2; //2 to 6, increases by 1 every 4th level.
                var strength = RollStatistics4D6DropLowest(scope);
                var dexterity = RollStatistics4D6DropLowest(scope);
                var constitution = RollStatistics4D6DropLowest(scope);
                var intelligence = RollStatistics4D6DropLowest(scope);
                var wisdom = RollStatistics4D6DropLowest(scope);
                var charisma = RollStatistics4D6DropLowest(scope);

                var hitPoints = await GetHitPoints(scope, constitution, characterLevel);

                return actorFactory(
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
        }

        private static async Task<int> GetHitPoints(ILifetimeScope scope, int constitution, int characterLevel)
        {
            using (var workerScope = scope.BeginLifetimeScope())
            {
                var con = workerScope.Resolve<Constitution.Factory>()(constitution);
                var hitDie = workerScope.Resolve<Die.Factory>()(8, con.Modifier, false);
                var hp = 8 + con.Modifier;
                for (var i = 2; i <= characterLevel; i++)
                {
                    hp += Math.Max(await hitDie.RollAsync(), 1);
                }

                return hp;
            }
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

        private static int RollStatistics4D6DropLowest(ILifetimeScope scope)
        {
            using (var workerScope = scope.BeginLifetimeScope())
            {
                var statsDice = GetStatisticsDice(scope);
                var results = statsDice.RollAll();
                return results.OrderByDescending(i => i).Take(3).Sum();
            }
        }

        private static IDice GetStatisticsDice(ILifetimeScope scope)
        {
            using (var workerScope = scope.BeginLifetimeScope())
            {
                var dieMaker = scope.Resolve<Die.Factory>();
                var statsDice = workerScope.Resolve<IDice>();
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
}