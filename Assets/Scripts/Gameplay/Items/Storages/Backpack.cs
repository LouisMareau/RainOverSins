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
    [CreateAssetMenu(fileName = "New Backpack", menuName = "RoS/Items/Backpack")]
    public class Backpack : Item
    {
        [Header("STORAGE")]
        public Currency goldPocket;
        public Currency bluPocket;
        [Space()]
        public float maxWeight;
        public List<Item> items;

        protected override void OnValidate() {
            base.OnValidate();
            goldPocket.Init();
            bluPocket.Init();
        }

        /// <summary>
        /// Adds an item to the backpack, as long as there is still enough space.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Returns the associated ExitCode if the item addition was successful (backpack not being full)or if the item couldn't be added because of full backpack.</returns>
        public ExitCode AddItem(Item item) {
            // Case: Backpack full
            // We check if the total amount of weight in the backpack with the added the weight of the item doesn't exceed the limit weight of the backpack
            if (GetTotalWeight() + item.backpackWeight > maxWeight) { 
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

        public float GetTotalWeight() {
            float weight = 0;
            foreach (Item item in items) {
                weight += item.backpackWeight;
            }
            return weight;
        }
    }
}