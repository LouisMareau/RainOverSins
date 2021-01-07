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
        /// <returns>Returns the associated ExitCode if the item addition was successful (backpack not being full)or if the item couldn't be added because of full backpack.</returns>
        public ExitCode AddItem(Item item) {
            // Case: Backpack full
            if (items.Count >= maxItemSlots) { 
                Debug.Log(string.Format("The backpack is full! Item \"{0}\" couldn't be added to the backpack..."));
                return ExitCode.Full_Backpack_Storage; 
            }
            // Case: Backpack slot available (i.e item addition is a success)
            else {
                items.Add(item);
                Debug.Log(string.Format("Item \"{0}\" has been added to the backpack!", item.name));
                return ExitCode.Success;
            }
        }

        public void RemoveItem(Item item) {
            items.Remove(item);
        }
    }
}