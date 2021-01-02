namespace RoS.Gameplay.Storages
{
    using System.Collections.Generic;
    using UnityEngine;

    using RoS.Gameplay.Resources;
    using RoS.Gameplay.Items;

    /// <summary>
    /// The Backpack must allow the player to carry items.
    /// Backpacks should be craftable by the player, can be bought from NPCs and sellable to other players (online)
    /// </summary>
    public class Backpack : Item
    {
        [Header("STORAGE")]
        public Resource goldPocket;
        public Resource bluPocket;
        [Space()]
        public int maxItemSlots;
        public List<Item> items;

        /// <summary>
        /// Adds an item to the backpack, as long as there is still enough space.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Returns TRUE if the item addition was successful (backpack not being full), and FALSE if the item couldn't be added because of full backpack.</returns>
        public bool AddItem(Item item) {
            if (items.Count <= maxItemSlots) {
                items.Add(item);
                Debug.Log(string.Format("Item \"{0}\" has been added to the backpack!", item.name));
                return true;
            }
            else {
                Debug.Log(string.Format("The backpack is full! Item \"{0}\" couldn't be added to the backpack..."));
                return false;
            }
        }
    }
}