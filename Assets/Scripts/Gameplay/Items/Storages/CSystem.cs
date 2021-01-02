namespace RoS.Gameplay.Storages
{
    using System.Collections.Generic;
    using UnityEngine;
    using RoS.Gameplay.Items;

    /// <summary>
    /// The Heav'n storage system will be used as a creature storage with 2 different sections:
    ///     • The active team
    ///     • The stored creatures (will not be able to fight)
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
    }
}