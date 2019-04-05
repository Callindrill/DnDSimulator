using System;
using Autofac;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Bootstrapper
{
    public class DiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RandomAsync>()
                .As<IRandomAsync>()
                .SingleInstance();

            builder.RegisterType<Die>()
                .As<IDie>()
                .AsSelf();

            builder.RegisterType<Dice>()
                .As<IDice>();
        }
    }
}