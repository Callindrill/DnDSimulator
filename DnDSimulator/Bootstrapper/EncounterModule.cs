using System;
using Autofac;
using DnDSimulator.Encounter;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Bootstrapper
{
    public class EncounterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Encounter.Encounter>()
                .As<IEncounter>();

            builder.RegisterType<Faction>()
                .As<IFaction>();

            builder.RegisterType<ActorGroup>()
                .As<IActorGroup>();

            builder.RegisterType<Attack>()
                .As<IAttack>();
        }
    }
}