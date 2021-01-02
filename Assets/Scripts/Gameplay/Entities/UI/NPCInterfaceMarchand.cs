namespace RoS.Gameplay.UI
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using RoS.Gameplay.Items;

    public class NPCInterfaceMarchand : NPCInterface
    {
        [Header("SELLERS")]
        public TextMeshProUGUI marchandMessage;
        public List<GameObject> items;
        [Space()]
        public Transform itemList;
        public GameObject selectableItemPrefab;
        [Space()]
        public TextMeshProUGUI selectedItemName;
        public TextMeshProUGUI selectedItemDescription;
        public TextMeshProUGUI selectedItemCostGold;
        public TextMeshProUGUI selectedItemCostBlu;

        public override void Init() {
            UpdateDisplay();
        }

        protected override void UpdateDisplay()
        {
            base.UpdateDisplay();

            if (npc != null) {
                marchandMessage.text = string.Format("\"\n {0} \n\"", npc.sellingMessage);

                items = npc.itemsToSell;
                foreach (GameObject item in items) {
                    AddItemToList(item);
                }
            }
        }

        /// <summary>
        /// Adds an item to the marchand interface's list associated with the NPC.
        /// </summary>
        /// <param name="item">The item to add to the list.</param>
        public void AddItemToList(GameObject item) {
            GameObject newItemGO = Instantiate<GameObject>(selectableItemPrefab, selectableItemPrefab.transform.position, selectableItemPrefab.transform.rotation, itemList);
            SelectableItem newSelectableItem = newItemGO.GetComponent<SelectableItem>();
            newSelectableItem.Init(item, this);
        }

        /// <summary>
        /// Removes an item from the NPC and from the marchand interface's list associated with the NPC.
        /// Though, it does not remove the item displayed on the screen, only the one from the variable list (NPCInterfaceMarchand.items).
        /// </summary>
        /// <param name="item">The item to remove from the list.</param>
        public void RemoveItemFromList(GameObject item) {
            // We remove the item from the npc
            bool removed = npc.itemsToSell.Remove(item);
            if (removed) {
                // If removed, we remove it from the item list as well
                items.Remove(item);
            }
        }
    }
}