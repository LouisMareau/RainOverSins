namespace RoS.Gameplay.UI
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using RoS.Gameplay.Entities;
    using RoS.Gameplay.Items;

    public class NPCInterface : MonoBehaviour
    {
        [HideInInspector] public NPC npc;

        [Header("CORE")]
        public new TextMeshProUGUI name;
        public TextMeshProUGUI title;
        public TextMeshProUGUI description;
        
        [Header("TYPES")]
        [Space()]
        public List<GameObject> typeIcons;
        public Transform typeIconsList;

        [Header("MARCHAND")]
        public TextMeshProUGUI marchandMessage;
        [HideInInspector] public List<Item> items;
        [Space()]
        private int listViewID;
        [Space()]
        public Transform itemList;
        public GameObject selectableItemPrefab;
        [Space()]
        public TextMeshProUGUI selectedItemName;
        public TextMeshProUGUI selectedItemDescription;
        public TextMeshProUGUI selectedItemCostGold;
        public TextMeshProUGUI selectedItemCostBlu;

        public virtual void Init() {
            UpdateDisplay();
        }

        protected virtual void UpdateDisplay() {
            if (npc != null) {
                if (npc.name != "") { name.text = npc.name; }
                if (npc.title != "") { title.text = npc.title; }
                if (npc.descriptionLong != "") { description.text = string.Format("\" {0} \"", npc.descriptionLong); }
                else if (npc.descriptionShort != "") { description.text = string.Format("\" {0} \"", npc.descriptionShort); }
                else { description.text = "Nothing interesting about this person..."; }

                DisplayNPCTypes();
                
                if (npc.isMarchand) {
                    marchandMessage.text = string.Format("\"\n {0} \n\"", npc.sellingMessage);

                    items = npc.itemsToSell;
                    SetupItemsToSellList();
                }
            }
        }

        /// <summary>
        /// For each type the NPC has, displays a icon and a button to switch menus rapidly
        /// ** Need to add buttons to switch between menus, when NPC has more than one type **
        /// </summary>
        private void DisplayNPCTypes() {
            if (npc.isMarchand) { Instantiate<GameObject>(typeIcons[0], typeIconsList.position, typeIconsList.rotation, typeIconsList); }
            if (npc.isQuestGiver) { Instantiate<GameObject>(typeIcons[1], typeIconsList.position, typeIconsList.rotation, typeIconsList); }
        }

        public void DisplayListView(int id) {
            listViewID = id;
            
            switch (listViewID) {
                // View ID 0 => GRID
                case 0:
                    // ** Display the list as a grid
                    break;
                
                case 1:
                    // ** Display the list as a list
                    break;

                default:
                    // ** Display the list as a grid
                    break;
            }
        } 

        #region MARCHAND (TYPE)
        public void SetupItemsToSellList() {
            foreach (Item item in items) {
                // We first setup all the items in the list, with their respective amounts
                CreateSelectableItem(item);
            }
        }

        /// <summary>
        /// Adds an item to the marchand interface's list associated with the NPC.
        /// </summary>
        /// <param name="item">The item to add to the list.</param>
        public void CreateSelectableItem(Item item) {
            GameObject newSelectableItemGO = Instantiate<GameObject>(selectableItemPrefab, selectableItemPrefab.transform.position, selectableItemPrefab.transform.rotation, itemList);
            SelectableItem newSelectableItem = newSelectableItemGO.GetComponent<SelectableItem>();
            newSelectableItem.Init(item, this);
        }

        /// <summary>
        /// Removes an item from the NPC and from the marchand interface's list associated with the NPC.
        /// </summary>
        /// <param name="item">The item to remove from the list.</param>
        public void RemoveItemFromList(Item item, int amount) {
            // We remove the amount of items specified from the NPC
            foreach (Item npcItem in npc.itemsToSell) {
                if (npcItem == item) {
                    // NPC's list
                    npcItem.amount -= amount;
                    // NPC's Interface's list (from this interface)
                    item.amount -= amount;
                    // If the amount reaches 0, we remove the item from the NPC's "items to sell" list
                    // We also make sure the npc's list and its interface are sync
                    if (npcItem.amount <= 0 && item.amount <= 0) { 
                        npc.itemsToSell.Remove(npcItem);
                        items.Remove(item);
                    }
                }
                else {
                    Debug.Log(string.Format("The item \"{0}\" couldn't be found... Try again."));
                }
            }
        }
        #endregion
    }
}