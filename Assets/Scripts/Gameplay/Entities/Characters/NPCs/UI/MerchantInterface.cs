namespace RoS.Gameplay.Entities.UI
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using RoS.Gameplay.Items;

    [System.Serializable]
    public class MerchantInterface : MonoBehaviour
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
        public Image selectedItemIcon;
        public TextMeshProUGUI selectedItemName;
        public TextMeshProUGUI selectedItemDescription;

        public void Init(NPC npc) {
            this.npc = npc;

            // We display the core info on screen
            npcName.text = npc.name;
            npcGoldAmount.text = string.Format("G {0}", npc.merchantInfo.equipment.backpack.GetCurrency(Currency.Type.GOLD).amount);
            
            // We populate the list with selectable items
            PopulateItemList(npc.merchantInfo.equipment.backpack.items);
        }

        public void CloseInterface() {
            if (this.gameObject.activeSelf) {
                // We remove the open modal from the game manager
                GameManager.openModals.Remove(this.gameObject);

                // We make sure that the list of selectable items is empty
                foreach (SelectableItem instance in selectableItems) {
                    Destroy(instance.gameObject);
                }
                // Make sure we clear the list to assure that we do not have any null object remaining
                selectableItems.Clear();

                // We close the modal by setting its active state to false
                this.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Used to initialized the itemToSell list. Populates the itemToSell list with game obejcts (SelectableItem (prefabs))
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
        /// Removes an item from the NPC and from the marchant interface's list associated with the NPC.
        /// </summary>
        /// <param name="item">The item to remove from the list.</param>
        public void RemoveItem(StackableItem itemToRemove, int amount) {
            // We remove the amount of items specified from the NPC
            foreach (StackableItem itemToSell in npc.merchantInfo.equipment.backpack.items) {
                if (itemToSell.item == itemToRemove.item) {
                    // NPC's list
                    itemToSell.amount -= amount;
                    // If the amount reaches 0, we remove the item from the NPC's "items to sell" list
                    // We also make sure the npc's list and its interface are sync
                    if (itemToSell.amount <= 0) { 
                        npc.merchantInfo.equipment.backpack.items.Remove(itemToRemove);
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