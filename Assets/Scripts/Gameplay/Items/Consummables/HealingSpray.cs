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
        public void Use(Entity entity) {
            if (entity.GetType() == typeof(Player) || entity.GetType() == typeof(Creature)) {
                
            }
        }
    }
}