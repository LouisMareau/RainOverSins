namespace RoS.Gameplay.Items
{
    using UnityEngine;
    using RoS.Gameplay.Entities;

    [System.Serializable]
    public class HealingSpray : Consumable
    {
        [Header("HEAL")]
        public float fixedHealth; // The amount of health the entity is going to be healed for instantly
        public float regen; // The amount of regen that will be added to the entity's base regen
        public int regenDuration; // The amount of turns the extra regen will be applied to the entity's base regen

        /// <summary>
        /// Uses the item (Healing Spray variant) on the specified playable entity and consumes it. 
        /// Being consumed, it will then be removed from the backpack storage (see Backpack class for removal definition).
        /// </summary>
        /// <param name="creature">The playable entity this item will be used on.</param>
        /// <returns>Returns the exit code with the value of the output depending on the state of the entity passed as argument.</returns>
        public ExitCode Use(PlayableEntity entity) { 
            float health = entity.stats.health;
            float maxHealth = entity.stats.maxHealth;
            float regen = entity.stats.healthRegen;
            // To be usable, the entity's health needs to be between 0 (excluded) and it's max HP (excluded) 
            if (health >= maxHealth) { return ExitCode.Entity_HP_Maxed; }
            else if (health <= 0) { return ExitCode.Entity_Dead; }
            else {
                // We heal the entity's health by the healing amount
                health += fixedHealth;
                // We add the extra regen to the base regen of the creature
                regen += this.regen;
                // We check if the health is higher than the max health of the entity, in which case, we floor it to the max health value
                if (health > maxHealth) { health = maxHealth; }
                // We create a new status for the entity
                entity.statuses.Add(new Status(Status.Type.REGENERATION, regen, regenDuration));
            }
            return ExitCode.Success;
        }
    }
}