using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DnDSimulator.Character;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Encounter
{
    //TODO: This list should really just be a "repeater" for a single actor, not a list of whatever actors you want in there.

    public class ActorGroup : List<IActor>, IActorGroup
    {
        public ActorGroup(Actor.Factory actorFactory)
        {
            ActorFactory = actorFactory ?? throw new ArgumentNullException(nameof(actorFactory));
        }

        public Actor.Factory ActorFactory { get; }

        public int Initiative { get; set; }

        public void Add(
            string name,
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
                name,
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

        public async Task RollInitiativeAsync()
        {
            var actor = this.FirstOrDefault();
 
            if (actor != null)
            {
                Initiative = await actor.RollInitiativeAsync();
            }

        }
    }
}   