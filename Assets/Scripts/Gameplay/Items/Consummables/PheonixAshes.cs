namespace RoS.Gameplay.Items 
{
    using UnityEngine;
    using RoS.Gameplay.Entities;

    /// <summary>
    /// This item can revive any playable entity, as long as it is during a fight.
    /// Once dead, the creature outside the fight cannot be revived by any object. Only a Great Pheonix can revive dead creatures, after the fight they died at is over.
    /// Pheonix ashes are extremely rare and cannot be found from NPC. They can only be dropped by a Great Pheonix.
    /// </summary>
    public class PheonixAshes : Consumable
    {
        [Header("HEAL")]
        [Range(0f, 100f)] public float healingPercentageAfterRevive; // The percentage the entity's health is going to be healed for (based on the maxHealth) 
        public float regen; // The amount of regen that will be added to the entity's base regen
        public int regenDuration; // The amount of turns the extra regen will be applied to the entity's base regen
        
        /// <summary>
        /// Uses the item (Pheonix Ashes variant) on the specific playable entity and consumes it.
        /// Being consumed, it will then be removed from the backpack storage (see Backpack class for removal definition).       
        /// </summary>
        /// <param name="entity">The playable entity this item will be used on.</param>
        /// <returns>Returns the exit code with the value of the output depending on the state of the entity passed as argument.</returns>
        public ExitCode Use(PlayableEntity entity) {
            float health = entity.stats.health;
            float maxHealth = entity.stats.maxHealth;
            float regen = entity.stats.healthRegen;

            // To be usable, the entity needs to be recently dead (within the last three (3) turns)
            if (entity.state == PlayableEntity.State.ALIVE) { return ExitCode.Entity_Alive; }
            else if (entity.state == PlayableEntity.State.DEAD_LATE) { return ExitCode.Entity_Dead_Late; }
            else if (entity.state == PlayableEntity.State.DEAD_RECENTLY) {
                // We heal the entity's health by the healing amount
                health += (maxHealth * (healingPercentageAfterRevive / 100));
                // We add the extra regen to the base regen of the creature
                regen += this.regen;
                // We check if (for some reasons) the health is higher than the max health of the entity, then we floor it to the max health value
                if (health > maxHealth) { health = maxHealth; }
                // We create a new status for the entity
                entity.statuses.Add(new Status(Status.Type.REGENERATION, regen, regenDuration));
            }
            return ExitCode.Success;
        }
    }
}