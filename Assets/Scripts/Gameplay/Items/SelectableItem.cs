namespace RoS.Gameplay.Items
{
    using UnityEngine;
    using TMPro;
    using RoS.Gameplay.Entities;
    using RoS.Gameplay.Storages;
    using RoS.Gameplay.UI;

    [System.Serializable]
    public class SelectableItem : MonoBehaviour
    {
        public new TextMeshProUGUI name;
        public TextMeshProUGUI costGold;
        public TextMeshProUGUI costBlu;
        public TextMeshProUGUI npcCurrentAmount;
        [HideInInspector] public Item itemToSell;
        [HideInInspector] public int npcSellingAmount;
        [HideInInspector] public NPCInterface npcInterface;

        public void Init(Item itemToSell, NPCInterface npcInterface) {
            this.npcInterface = npcInterface;
            this.itemToSell = itemToSell;

            name.text = itemToSell.name;
            costGold.text = string.Format("G {0}", itemToSell.tradeInfo.marchandCostGold.ToString("F0"));
            costBlu.text = string.Format("B {0}", itemToSell.tradeInfo.marchandCostBlu.ToString("F0"));
            npcCurrentAmount.text = itemToSell.amount.ToString("F0");
        }

        public void OnItemSelection() {
            npcInterface.selectedItemName.text = itemToSell.name;
            npcInterface.selectedItemDescription.text = itemToSell.description;
            if (npcInterface.selectedItemCostGold != null) { npcInterface.selectedItemCostGold.text = string.Format("G {0}", itemToSell.tradeInfo.marchandCostGold.ToString("F0")); }
            if (npcInterface.selectedItemCostBlu != null) { npcInterface.selectedItemCostBlu.text = string.Format("B {0}", itemToSell.tradeInfo.marchandCostBlu.ToString("F0")); }
        }

        public void Buy() {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            Backpack backpack = player.backpack;
            RSystem rSystem = player.rSystem;
            Trade tradeInfo = itemToSell.tradeInfo;
            if (player && backpack != null) {
                // We check if the player has enough gold and blu to buy the item
                if (backpack.goldPocket.amount >= tradeInfo.marchandCostGold && backpack.bluPocket.amount >= tradeInfo.marchandCostBlu) {
                    // We substract the cost of the items off the player
                    backpack.goldPocket.amount -= tradeInfo.marchandCostGold;
                    backpack.bluPocket.amount -= tradeInfo.marchandCostBlu;

                    // We check if the items are Backpacks or C-Systems, since they are not added to a backpack, but replaces the current backpack or C-System
                    if (itemToSell.GetType() == typeof(Backpack)) {
                        // We replace any existing backpack with the new one
                        backpack = (Backpack)itemToSell;
                        
                        // * ADDITION: Ask the player if he/she is sure to remove the current backpack *
                        // * ADDITION: Give the player the ability to switch its items to the new backpack he/she is about the buy *

                        Debug.Log("Player's BACKPACK has changed!");
                        Debug.Log(string.Format("{0} GOLD and {1} BLU has been debited from the player...", tradeInfo.marchandCostGold, tradeInfo.marchandCostBlu));
                    }
                    else if (itemToSell.GetType() == typeof(RSystem)) {
                        // We replace any existing RSystem with the new one
                        rSystem = (RSystem)itemToSell;

                        // * ADDITION: Ask the player if he/she is sure to remove the current c-system *
                        // * NOTE: The csystem carries over automatically all the entities to the new one, the rift key/ID being the same one *

                        Debug.Log("Player's C-SYSTEM has changed!");
                        Debug.Log(string.Format("{0} GOLD and {1} BLU has been debited from the player...", tradeInfo.marchandCostGold, tradeInfo.marchandCostBlu));
                    }
                    else {
                        // We try to add the item to the backpack (where all the other items should be stored)
                        // If the backpack already has the item, we add the amount specified in the amount field...
                        foreach (Item item in backpack.items) {
                            if (item == itemToSell) {
                                item.amount += this.npcSellingAmount;
                            }
                        }

                        // ...Otherwise, we add the new item to the backpack list
                        ExitCode add = backpack.AddItem(itemToSell);
                        if (add == ExitCode.Success) {
                            // Do something... ?
                        }
                        else if (add == ExitCode.Full_Backpack_Storage) {
                            // We display a message saying that the backpack is full and the item couldn't be added
                            Debug.Log(string.Format("Action of buying item \"{0}\" canceled... Backpack is full!", itemToSell.name));
                            return;
                        }
                    }

                    // We remove the item from the list
                    npcInterface.RemoveItemFromList(itemToSell, npcSellingAmount);
                    // We destroy this game object, which will remove it from the npc interface
                    Destroy(this.gameObject);
                }
                else {
                    if (backpack.goldPocket.amount < tradeInfo.marchandCostGold) { Debug.Log("The Player doesn'thave enough Gold to buy the item..."); }
                    else if (backpack.bluPocket.amount < tradeInfo.marchandCostBlu) { Debug.Log("The Player doesn't have enough Blu to buy the item..."); }
                }
            }
            else {
                Debug.Log("Player couldn't be found in hierarchy...");
            }
        }
    }
}