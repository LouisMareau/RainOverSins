namespace RoS.Gameplay.Items
{
    using System.Collections.Generic;
    using UnityEngine;

    using RoS.Gameplay.Items;

    /// <summary>
    /// The Backpack must allow the player to carry items.
    /// Backpacks should be craftable by the player, can be bought from NPCs and sellable to other players (online trading)
    /// </summary>
    [System.Serializable]
    public class Backpack : Item
    {
        [Header("STORAGE")]
        public List<StackableItem> items;
        
        [Header("WEIGHT")]
        [HideInInspector] public float totalWeight;
        public float maxWeight;

        protected override void OnValidate() {
            base.OnValidate();

            // Weight should always be 0 as backpacks are the roots for weight system
            weight = 0;
        }

        /// <summary>
        /// Adds an item to the backpack (multiple if amount > 1), as long as there is still enough space.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Returns the associated ExitCode if the item addition was successful (backpack not being full)or if the item couldn't be added because of full backpack.</returns>
        public ExitCode AddItem(StackableItem itemToAdd, int amount) {
            Item item = itemToAdd.item;
            if (amount > 0) {
                // We check if the total amount of weight in the backpack with the added the weight of the item doesn't exceed the limit weight of the backpack
                if (GetTotalWeight() + item.weight > maxWeight) {
                    // Log display
                    Debug.Log(string.Format("The backpack is full! Item \"{0}\" couldn't be added to the backpack...", item.name));
                    return ExitCode.Backpack_Full;
                }

                // First, we check if the item is already in the Backpack
                foreach (StackableItem backpackItem in items) {
                    Item backpackItemS = backpackItem.item;
                    if (backpackItemS.name == item.name) {
                        // If yes, we add the amount to the item's amount
                        backpackItem.amount += amount;
                        // Log display
                        Debug.Log(string.Format("Item \"{0}\" has been added to the backpack!", item.name));
                        return ExitCode.Backpack_AmountIncreased;
                    }
                }

                // Otherwise, we create a new item in the list
                items.Add(itemToAdd);
                // Log display
                Debug.Log(string.Format("Item \"{0}\" has been added to the backpack!", item.name));
                return ExitCode.Backpack_NewItemAdded;
            }
            else {
                Debug.Log(string.Format("Amount is null. Please, review the amount specified. The item \"{0}\" couldn't be added to the backpack...", item.name));
                return ExitCode.Amount_IsZero;
            }
        }

        public void RemoveItem(StackableItem item) {
            // We first remove the item from the item list
            items.Remove(item);
        }

        /// <summary>
        /// Search by Item an item stored in the backpack.
        /// </summary>
        /// <param name="searcheditem">The item that need to the searched.</param>
        /// <returns>Returns the item if found, null if not.</returns>
        public Item GetItem(Item searcheditem) {
            foreach (StackableItem stackedItem in items) {
                if (stackedItem.item == searcheditem) { return stackedItem.item; }
            }
            return null;
        }

        /// <summary>
        /// Search by string an item stored in the backpack.
        /// </summary>
        /// <param name="searchedItemName">The name of the item that need to be searched.</param>
        /// <returns>Return the Item if found, null if not.</returns>
        public Item GetItem(string searchedItemName) {
            foreach (StackableItem stackedItem in items) {
                if (stackedItem.item.name == searchedItemName) { return stackedItem.item; }
            }
            return null;
        }

        /// <summary>
        /// Search by currency type a currency stored in the backpack.
        /// </summary>
        /// <param name="type">The type of currency that will be searched within the backapck.</param>
        /// <returns>Returns the currency if found, null if not.</returns>
        public StackableItem GetCurrency(Currency.Type currencyType) {
            foreach (StackableItem stackedItem in items) {
                if (stackedItem.item.GetType() == typeof(Currency)) {
                    Currency c = (Currency)stackedItem.item;
                    if (c.type == currencyType) {
                        return stackedItem;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the amount of the specified currency stored in the backpack.
        /// </summary>
        /// <param name="currencyType">The type of currency that will be searched within the backpack.</param>
        /// <returns>Returns the amount if the item was found, 0 if not.</returns>
        public float GetCurrencyAmount(Currency.Type currencyType) {
            float amount = 0;
            foreach (StackableItem stackedItem in items) {
                if (stackedItem.item.GetType() == typeof(Currency)) {
                    Currency c = (Currency)stackedItem.item;
                    if (c.type == currencyType) {
                        amount += stackedItem.amount;
                    }
                }
            }
            return amount;
        }

        public float GetTotalWeight() {
            float weight = 0;
            foreach (StackableItem stackedItem in items) {
                weight += stackedItem.GetStackWeight();
            }
            totalWeight = weight;
            return weight;
        }
    }
}