namespace RoS.Gameplay.Entities.UI
{
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using RoS.Gameplay.Items;

    [System.Serializable]
    public class MerchandInterface : MonoBehaviour
    {
        private NPC npc;

        [Header("CORE")]
        public TextMeshProUGUI npcName;
        public TextMeshProUGUI npcGoldAmount;

        [Header("ITEM LIST")]
        public List<SelectableItem> selectableItems;
        public GameObject selectableItemPrefab;
        public Transform listTransform;

        [Header("SELECTED ITEM")]
        public Sprite selectedItemIcon;
        public TextMeshProUGUI selectedItemName;
        public TextMeshProUGUI selectedItemDesctiption;

        public void Init(NPC npc) {
            this.npc = npc;

            // We display the core info on screen
            npcName.text = npc.name;
            npcGoldAmount.text = string.Format("G {0}", npc.GetCurrency(Currency.Type.GOLD).amount);
            
            // We populate the list with selectable items
            PopulateItemList(npc.merchandInfo.itemsToSell);
        }

        /// <summary>
        /// Used to initialized the itemToSell listPopulates the itemToSell list with the 
        /// </summary>
        /// <param name="itemsToSell"></param>
        public void PopulateItemList(List<StackableItem> itemsToSell) {
            foreach (StackableItem itemToSell in itemsToSell) {
                GameObject go = Instantiate<GameObject>(selectableItemPrefab, listTransform);
                go.GetComponent<SelectableItem>().Init(itemToSell, this);
                selectableItems.Add(go.GetComponent<SelectableItem>());
            }
        }

        /// <summary>
        /// Gets the selectable item (UI element) using stackableItem search.
        /// </summary>
        /// <param name="itemToSearch">The stackable item to search in the list.</param>
        /// <returns>Returns the selectableItem if found, null if not.</returns>
        public SelectableItem GetSelectableItem(StackableItem itemToSearch) {
            foreach (SelectableItem selectable in selectableItems) {
                if (selectable.itemToSell.item == itemToSearch.item) {
                    return selectable;
                }
            }
            return null;
        }

        /// <summary>
        /// Removes an item from the NPC and from the marchand interface's list associated with the NPC.
        /// </summary>
        /// <param name="item">The item to remove from the list.</param>
        public void RemoveItem(StackableItem itemToRemove, int amount) {
            // We remove the amount of items specified from the NPC
            foreach (StackableItem itemToSell in npc.merchandInfo.itemsToSell) {
                if (itemToSell.item == itemToRemove.item) {
                    // NPC's list
                    itemToSell.amount -= amount;
                    // NPC's Interface's list (from this interface)
                    //itemToRemove.amount -= amount;
                    // If the amount reaches 0, we remove the item from the NPC's "items to sell" list
                    // We also make sure the npc's list and its interface are sync
                    if (itemToSell.amount <= 0) { 
                        npc.merchandInfo.itemsToSell.Remove(itemToRemove);
                        // FYI: The selectable item is destroy by the instance itself when amount reaches 0
                    }
                }
                else {
                    Debug.Log(string.Format("The item \"{0}\" couldn't be found... Try again."));
                }
            }
        }
    }
}