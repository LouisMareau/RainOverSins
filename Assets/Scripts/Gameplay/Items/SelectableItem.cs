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
        [HideInInspector] public GameObject itemGO;
        [HideInInspector] public Item item;
        [HideInInspector] public NPCInterface npcInterface;

        public void Init(GameObject itemGO, NPCInterface npcInterface) {
            this.itemGO = itemGO;
            this.item = itemGO.GetComponent<Item>();
            this.npcInterface = npcInterface;

            name.text = item.name;
            costGold.text = string.Format("G {0}", item.buyingCostGold.ToString("F0"));
            costBlu.text = string.Format("B {0}", item.buyingCostBlu.ToString("F0"));
        }

        public void OnItemSelection() {
            npcInterface.selectedItemName.text = item.name;
            npcInterface.selectedItemDescription.text = item.description;
            if (npcInterface.selectedItemCostGold != null) { npcInterface.selectedItemCostGold.text = string.Format("G {0}", item.buyingCostGold.ToString("F0")); }
            if (npcInterface.selectedItemCostBlu != null) { npcInterface.selectedItemCostBlu.text = string.Format("B {0}", item.buyingCostBlu.ToString("F0")); }
        }

        public void Buy() {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            Backpack backpack = player.backpack.GetComponent<Backpack>();
            if (player) {
                // We check if the player has enough gold and blu to buy the item
                if (backpack.goldPocket.amount >= item.buyingCostGold && backpack.bluPocket.amount >= item.buyingCostBlu) {
                    // We substract the cost of the items off the player
                    backpack.goldPocket.amount -= item.buyingCostGold;
                    backpack.bluPocket.amount -= item.buyingCostBlu;

                    // We check if the items are Backpacks or C-Systems, since they are not added to a backpack, but replaces the current backpack or C-System
                    if (item.GetType() == typeof(Backpack)) {
                        // We check if the player already have a backpack in the "Storages" game object, under the player object, and destroy it before adding the new one
                        // * ADDITION: Ask the player if he/she is sure to remove the current backpack *
                        // * ADDITION: Give the player the ability to switch its items to the new backpack he/she is about the buy *
                        if (player.storagesT.childCount > 0) { Destroy(player.backpack); }

                        // We create a clone of the backpack prefab and add it to the "Storages" game object
                        GameObject newBackpack = Instantiate<GameObject>(itemGO, Vector3.zero, Quaternion.identity, player.storagesT);
                        player.backpack = newBackpack;
                        Debug.Log("Player's BACKPACK has changed!");
                        Debug.Log(string.Format("{0} GOLD and {1} BLU has been debited from the player...", item.buyingCostGold, item.buyingCostBlu));
                    }
                    else if (item.GetType() == typeof(CSystem)) {
                        // We check if the player already have a c-system in the "Storages" game object, under the player object, and destroy it before adding the new one
                        // * ADDITION: Ask the player if he/she is sure to remove the current c-system *
                        // * NOTE: The csystem carries over automatically all the entities to the new one, the rift key/ID being the same one *
                        if (player.storagesT.childCount > 0) { Destroy(player.cSystem); }
                        // We create a clone of the backpack prefab and add it to the "Storages" game object, under the player object

                        GameObject cSystem = Instantiate<GameObject>(itemGO, Vector3.zero, Quaternion.identity, player.storagesT);
                        player.cSystem = cSystem;
                        Debug.Log("Player's C-SYSTEM has changed!");
                        Debug.Log(string.Format("{0} GOLD and {1} BLU has been debited from the player...", item.buyingCostGold, item.buyingCostBlu));
                    }
                    else {
                        // We try to add the item to the backpack
                        ExitCode code = player.backpack.GetComponent<Backpack>().AddItem(item);
                        if (code == ExitCode.Success) {
                            // Do something... ?
                        }
                        // If the backpack is full, we exit and show a message
                        else if (code == ExitCode.Full_Backpack_Storage) {
                            Debug.Log(string.Format("Action of buying item \"{0} [{1}]\" canceled... Backpack is full!", item.name, item.id));
                            return;
                        }
                    }

                    // We remove the item from the list
                    npcInterface.RemoveItemFromList(itemGO);
                    // We destroy this game object, which will remove it from the npc interface
                    Destroy(this.gameObject);
                }
                else {
                    if (backpack.goldPocket.amount < item.buyingCostGold) { Debug.Log("The Player doesn'thave enough Gold to buy the item..."); }
                    else if (backpack.bluPocket.amount < item.buyingCostBlu) { Debug.Log("The Player doesn't have enough Blu to buy the item..."); }
                }
            }
            else {
                Debug.Log("Player couldn't be found in hierarchy...");
            }
        }
    }
}