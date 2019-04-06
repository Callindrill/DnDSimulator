using System;
using System.Collections.Generic;
using DnDSimulator.Character;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Encounter
{
    public class ActorGroup : List<IActor>, IActorGroup
    {
        public ActorGroup(Actor.Factory actorFactory)
        {
            ActorFactory = actorFactory ?? throw new ArgumentNullException(nameof(actorFactory));
        }

        public Actor.Factory ActorFactory { get; }

        public void Add(
            int armorClass,
            int currentHitPoints,
            int maxHitPoints,
            int temporaryHitPoints,
            int proficiency,
            int strength,
            int dexterity,
            int constitution,
            int intelligence,
            int wisdom,
            int charisma)
        {
            this.Add(ActorFactory(
                armorClass,
                currentHitPoints,
                maxHitPoints,
                temporaryHitPoints,
                proficiency,
                strength,
                dexterity,
                constitution,
                intelligence,
                wisdom,
                charisma)
                );
        }
    }
}