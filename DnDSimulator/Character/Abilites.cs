using System;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Character
{
    public class Abilities : IAbilities
    {
        public delegate Abilities Factory(
            int strength,
            int dexterity,
            int constitution,
            int intelligence,
            int wisdom,
            int charisma);

        public Abilities(
            IStrength strength,
            IDexterity dexterity,
            IConstitution constitution,
            IIntelligence intelligence,
            IWisdom wisdom,
            ICharisma charisma)
        {
            Strength = strength ?? throw new ArgumentNullException(nameof(strength));
            Dexterity = dexterity ?? throw new ArgumentNullException(nameof(dexterity));
            Constitution = constitution ?? throw new ArgumentNullException(nameof(constitution));
            Intelligence = intelligence ?? throw new ArgumentNullException(nameof(intelligence));
            Wisdom = wisdom ?? throw new ArgumentNullException(nameof(wisdom));
            Charisma = charisma ?? throw new ArgumentNullException(nameof(charisma));
        }

        public Abilities(
            int strength,
            int dexterity,
            int constitution,
            int intelligence,
            int wisdom,
            int charisma,
            Func<int, IStrength> strengthFactory,
            Func<int, IDexterity> dexterityFactory,
            Func<int, IConstitution> constitutionFactory,
            Func<int, IIntelligence> intelligenceFactory,
            Func<int, IWisdom> wisdomFactory,
            Func<int, ICharisma> charismaFactory)
        {
            if (strengthFactory == null) throw new ArgumentNullException(nameof(strengthFactory));
            if (dexterityFactory == null) throw new ArgumentNullException(nameof(dexterityFactory));
            if (constitutionFactory == null) throw new ArgumentNullException(nameof(constitutionFactory));
            if (intelligenceFactory == null) throw new ArgumentNullException(nameof(intelligenceFactory));
            if (wisdomFactory == null) throw new ArgumentNullException(nameof(wisdomFactory));
            if (charismaFactory == null) throw new ArgumentNullException(nameof(charismaFactory));
            Strength = strengthFactory(strength);
            Dexterity = dexterityFactory(dexterity);
            Constitution = constitutionFactory(constitution);
            Intelligence = intelligenceFactory(intelligence);
            Wisdom = wisdomFactory(wisdom);
            Charisma = charismaFactory(charisma);
        }

        public IStrength Strength { get; set; }
        public IDexterity Dexterity { get; set; }
        public IConstitution Constitution { get; set; }
        public IIntelligence Intelligence { get; set; }
        public IWisdom Wisdom { get; set; }
        public ICharisma Charisma { get; set; }
    }
}