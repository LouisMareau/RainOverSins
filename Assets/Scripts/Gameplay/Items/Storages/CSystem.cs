namespace RoS.Gameplay.Storages
{
    using System.Collections.Generic;
    using UnityEngine;
    using RoS.Gameplay.Items;

    /// <summary>
    /// The Creature Storage system has 2 different usage:
    ///     • Setup an active team that can be traded easily through a high energy rift
    ///     • Store large amount of creatures but cannot use the high energy rift system used by the team (the creatures stored this way will not be able to fight)
    /// </summary>
    public class CSystem : Item
    {
        [Header("ACTIVE TEAM")]
        public int teamCost; // The total cost allowed in the active team
        public List<Entity> activeTeam; // List of all the creatures that can be called during fights
        [HideInInspector] public int cost; // The current cost of the active team

        [Header("ENTITY STORAGE")]
        public int maxEntitiesStored; // The total amount allowed in the storage
        public List<Entity> storedEntities; // List of all the creatures owned by the player but aren't part of the active team, making them unable to fight
        [HideInInspector] public int storedAmount; // The current amount of creatures in the storage

        public ExitCode AddEntityToStorage(Entity newEntity) {
            // If the storage is full, we cannot add the entity to the storage and we exit the function with an ExitCode 3
            if (storedEntities.Count >= maxEntitiesStored) { return ExitCode.Full_CSystem_Storage; }
            else {
                storedEntities.Add(newEntity);
                return ExitCode.Success;
            }
        }
    }
}