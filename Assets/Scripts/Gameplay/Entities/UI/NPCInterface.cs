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
        [HideInInspector] public List<GameObject> items;
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
                    foreach (GameObject item in items) {
                        AddItemToList(item);
                    }
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

        #region MARCHAND (TYPE)
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
        #endregion
    }
}