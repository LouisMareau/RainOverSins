namespace RoS.Gameplay.Items
{
    using UnityEngine;
    using TMPro;
    using RoS.Gameplay.Entities;
    using RoS.Gameplay.Entities.UI;

    [System.Serializable]
    public class SelectableItem : MonoBehaviour
    {
        // ** Make a custom editor that would display the correct variables for each type of viewing options
        // ** The cost can be any item desired as any item can be traded, with a default value as Gold (if no item was chosen)

        /// <summary>
        /// A reference to the name of the item.
        /// </summary>
        public new TextMeshProUGUI name;

        /// <summary>
        /// A reference to the amount of this item the npc owns.
        /// </summary>
        public TextMeshProUGUI npcCurrentAmount;

        /// <summary>
        /// A reference to the foldout (containing trade info).
        /// </summary>
        public GameObject foldout;

        /// <summary>
        /// ** Temporary variable
        /// A reference to the total amount of gold for the transaction (amount included).
        /// </summary>
        public TextMeshProUGUI unitCostGold;

        /// <summary>
        /// The item that this selectable will target (the item to sell).
        /// </summary>
        public StackableItem itemToSell;

        /// <summary>
        /// The amount of the item that will be sold (at once).
        /// </summary>
        public int tradeAmount = 1;

        /// <summary>
        /// A reference to the merchand interface (UI).
        /// </summary>
        public MerchantInterface merchantInterface;

        public void Init(StackableItem itemToSell, MerchantInterface merchantInterface) {
            this.merchantInterface = merchantInterface;
            this.itemToSell = itemToSell;

            // We setup the core info (should be hidden until selected)
            name.text = this.itemToSell.item.name;
            unitCostGold.text = string.Format("G {0}", this.itemToSell.item.tradeInfo.merchantCostGold.ToString("F0"));
            npcCurrentAmount.text = this.itemToSell.amount.ToString("F0");
        }

        public void OnItemSelection() {
            if (!foldout.activeSelf) { 
                // We close any other active foldout (different than this one)
                foreach (SelectableItem selectableItem in merchantInterface.selectableItems) {
                    if (selectableItem != this) {
                        if (selectableItem.foldout.activeSelf) {
                            selectableItem.CloseFoldout();
                        }
                    }
                }
                // We open the foldout
                OpenFoldout();
            }
            else { 
                CloseFoldout();
            }
        }

        public void OpenFoldout() {
            // We first actualize the item info from the merchant interface class
            merchantInterface.selectedItemIcon.sprite = itemToSell.item.thumbnail;
            merchantInterface.selectedItemName.text = itemToSell.item.name;
            merchantInterface.selectedItemDescription.text = itemToSell.item.description;
            // We then place the foldout at the end of the list so it appears in front of the other items
            if (foldout.transform.parent != merchantInterface.listTransform) {
                foldout.transform.SetParent(merchantInterface.listTransform);
                foldout.transform.SetAsLastSibling();
            }
            // Finally, we show the foldout
            foldout.SetActive(true); 
        }

        public void CloseFoldout() {
            // We make sure to reset the transform of the foldout to be child of this object
            if (foldout.transform.parent != this.transform) {
                foldout.transform.SetParent(this.transform);
                foldout.transform.SetAsLastSibling();
            }
            // We clear the item info from merchantInterface
            merchantInterface.selectedItemIcon.sprite = null;
            merchantInterface.selectedItemName.text = "";
            merchantInterface.selectedItemDescription.text = "";
            // We hide the foldout
            foldout.SetActive(false);
        }

        public void Buy() {
            Item item = itemToSell.item;
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            Backpack backpack = player.equipment.backpack;
            RSystem rSystem = player.equipment.rSystem;
            Trade tradeInfo = item.tradeInfo;
            if (player && backpack != null) {
                // We check if the player has enough gold and blu to buy the item
                if (backpack.GetCurrencyAmount(Currency.Type.GOLD) >= tradeInfo.merchantCostGold) {
                    // We substract the cost of the item
                    backpack.GetCurrency(Currency.Type.GOLD).amount -= tradeInfo.merchantCostGold;

                    // We check if the items are Backpacks or C-Systems, since they are not added to a backpack, but replaces the current backpack or C-System (under Player.Equipment)
                    if (item.GetType() == typeof(Backpack)) {

                        // * ADDITION: Ask the player if he/she is sure to remove the current backpack *
                        // * ADDITION: Give the player the ability to switch its items to the new backpack he/she is about the buy *

                        // We replace any existing backpack with the new one
                        backpack = (Backpack)item;

                        Debug.Log("Player's BACKPACK has changed!");
                        Debug.Log(string.Format("{0} GOLD has been debited from the player...", tradeInfo.merchantCostGold));
                    }
                    else if (item.GetType() == typeof(RSystem)) {
                        // We replace any existing RSystem with the new one
                        rSystem = (RSystem)item;

                        // * ADDITION: Ask the player if he/she is sure to remove the current c-system *
                        // * NOTE: The csystem carries over automatically all the entities to the new one, the rift key/ID being the same one *

                        Debug.Log("Player's C-SYSTEM has changed!");
                        Debug.Log(string.Format("{0} GOLD has been debited from the player...", tradeInfo.merchantCostGold));
                    }
                    else {
                        // ...Otherwise, we add the new item to the backpack list
                        ExitCode add = backpack.AddItem(itemToSell, tradeAmount);
                        if (add == ExitCode.Backpack_AmountIncreased) {
                            // We display a message saying that the item was already in the backpack and its amount has increased
                            Debug.Log(string.Format("The item \"{0}\" is already in the backpack. The amount has increased by {1}.\nTotal amount: {2}", item.name, tradeAmount, itemToSell.amount));
                        }
                        else if (add == ExitCode.Backpack_Full) {
                            // We display a message saying that the backpack is full and the item couldn't be added
                            Debug.Log(string.Format("Action of buying item \"{0}\" canceled... Backpack is full!", item.name));
                            return;
                        }
                    }

                    // Then, we remove the item from the list before destroying this object if amount = 0
                    merchantInterface.RemoveItem(itemToSell, tradeAmount);
                    // We destroy this game object, which will remove it from the npc interface
                    Destroy(this.gameObject);
                }
                else {
                    if (backpack.GetCurrency(Currency.Type.GOLD).amount < tradeInfo.merchantCostGold) { Debug.Log("The Player doesn'thave enough Gold to buy the item..."); }
                }
            }
            else {
                Debug.Log("Player couldn't be found in hierarchy...");
            }
        }
    }
}