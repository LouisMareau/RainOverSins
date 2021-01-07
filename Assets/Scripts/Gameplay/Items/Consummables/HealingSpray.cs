namespace RoS.Gameplay.Items
{
    using UnityEngine;
    using RoS.Gameplay.Entities;
    using RoS.Gameplay.Entities.Creatures;

    [System.Serializable]
    public class HealingSpray : Consumable
    {
        [Header("HEAL")]                                                                           
        public float healingAmount;

        /// <summary>
        /// Uses the item (Healing Spray variant) on the specified entity (Character or Creature) and consumes it. 
        /// Being consumed, it will then be removed from the backpack storage (see Backpack class for removal definition)
        /// </summary>
        public ExitCode Use(Creature creature) { 
            float health = creature.stats.health;
            float maxHealth = creature.stats.maxHealth;
            // To be usable, the creature's health needs to be between 0 (excluded) and it's max HP (excluded) 
            if (health >= maxHealth) { return ExitCode.Creature_HP_Maxed; }
            else if (health <= 0) { return ExitCode.Creature_Dead; }
            else {
                // We heal the creature's health by the healing amount
                health += healingAmount;
                // We check if the health is higher than the max health of the creature, in which case, we floor it to the max health value
                if (health > maxHealth) { health = maxHealth; }
                return ExitCode.Success;
            }
        }
    }
}