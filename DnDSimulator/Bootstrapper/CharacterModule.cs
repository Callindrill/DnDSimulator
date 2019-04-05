using System;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using DnDSimulator.Character;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Bootstrapper
{
    internal class CharacterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Ability>()
                .As<IAbility>();


            builder.RegisterType<Abilities>()
                .As<IAbilities>();

            builder.RegisterGeneratedFactory<Abilities.Factory>(new TypedService(typeof(IAbilities)));

            builder.RegisterType<Actor>()
                .As<IActor>();

            builder.RegisterGeneratedFactory<Actor.Factory>(new TypedService(typeof(IActor)));

            builder.RegisterType<Strength>()
                .As<IStrength>();
            builder.RegisterGeneratedFactory<Strength.Factory>(new TypedService(typeof(IStrength)));

            builder.RegisterType<Dexterity>()
                .As<IDexterity>();
            builder.RegisterGeneratedFactory<Dexterity.Factory>(new TypedService(typeof(IDexterity)));

            builder.RegisterType<Constitution>()
                .As<IConstitution>();
            builder.RegisterGeneratedFactory<Constitution.Factory>(new TypedService(typeof(IConstitution)));

            builder.RegisterType<Intelligence>()
                .As<IIntelligence>();
            builder.RegisterGeneratedFactory<Intelligence.Factory>(new TypedService(typeof(IIntelligence)));

            builder.RegisterType<Wisdom>()
                .As<IWisdom>();
            builder.RegisterGeneratedFactory<Wisdom.Factory>(new TypedService(typeof(IWisdom)));

            builder.RegisterType<Charisma>()
                .As<ICharisma>();
            builder.RegisterGeneratedFactory<Charisma.Factory>(new TypedService(typeof(ICharisma)));
        }
    }
}