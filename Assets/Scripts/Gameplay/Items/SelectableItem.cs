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
        [HideInInspector] public NPCInterfaceMarchand marchandInterface;

        public void Init(GameObject itemGO, NPCInterfaceMarchand interfaceMarchand) {
            this.itemGO = itemGO;
            this.item = itemGO.GetComponent<Item>();
            this.marchandInterface = interfaceMarchand;

            name.text = item.name;
            costGold.text = item.buyingCostGold.ToString("F0");
            costBlu.text = item.buyingCostBlu.ToString("F0");
        }

        public void OnItemSelection() {
            marchandInterface.selectedItemName.text = item.name;
            marchandInterface.selectedItemDescription.text = item.description;
            if (marchandInterface.selectedItemCostGold != null) { marchandInterface.selectedItemCostGold.text = item.buyingCostGold.ToString("F0"); }
            if (marchandInterface.selectedItemCostBlu != null) { marchandInterface.selectedItemCostBlu.text = item.buyingCostBlu.ToString("F0"); }
        }

        public void Buy() {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if (player) {
                // We check if the player has enough gold and blu to buy the item
                if (player.gold.amount >= item.buyingCostGold && player.blu.amount >= item.buyingCostBlu) {
                    // We substract the cost of the items off the player
                    player.gold.amount -= item.buyingCostGold;
                    player.blu.amount -= item.buyingCostBlu;

                    // We check if the items are Backpacks or C-Systems, since they are not added to a backpack, but replaces the current backpack or C-System
                    if (item.GetType() == typeof(Backpack)) {
                        // We check if the player already have a backpack in the "Storages" game object, under the player object, and destroy it before adding the new one
                        // * ADDITION: Ask the player if he/she is sure to remove the current backpack *
                        // * ADDITION: Give the player the ability to switch its items to the new backpack he/she is about the buy *
                        if (player.storagesT.childCount > 0) { Destroy(player.backpack); }
                        // We create a clone of the backpack prefab and add it to the "Storages" game object

                        GameObject backpack = Instantiate<GameObject>(itemGO, Vector3.zero, Quaternion.identity, player.storagesT);
                        player.backpack = backpack;
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
                        if (player.backpack.GetComponent<Backpack>().AddItem(item)) {
                            // Do something... ?
                        }
                        // If the backpack is full, we exit and show a message
                        else {
                            Debug.Log(string.Format("Action of buying item \"{0} [{1}]\" canceled... Backpack is full!", item.name, item.id));
                            return;
                        }
                    }

                    // We remove the item from the list
                    marchandInterface.RemoveItemFromList(itemGO);
                    // We destroy this game object, which will remove it from the npc interface
                    Destroy(this.gameObject);
                }
                else {
                    if (player.gold.amount < item.buyingCostGold) { Debug.Log("The Player doesn'thave enough Gold to buy the item..."); }
                    else if (player.blu.amount < item.buyingCostBlu) { Debug.Log("The Player doesn't have enough Blu to buy the item..."); }
                }
            }
            else {
                Debug.Log("Player couldn't be found in hierarchy...");
            }
        }
    }
}