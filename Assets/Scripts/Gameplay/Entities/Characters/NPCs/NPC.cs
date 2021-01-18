namespace RoS.Gameplay.Entities
{
    using UnityEngine;
    using RoS.Gameplay.Items;

    public class NPC : Entity
    {
        // ** Create a custom editor to create rapidly items and assign them to NPCs
        [Header("NPC TYPES")]
        // ** Setup a enum that can have multiple options selected for easier access
        public bool isMerchand = false;
        public bool isQuestGiver = false;

        [Header("PHYSICS")]
        private new Collider collider;

        [Header("MERCHAND")]
        public MerchandInfo merchandInfo;

        private void Awake() {
            collider = GetComponent<Collider>();
        }

        public void OnSelection() {
            if (isMerchand) {
                merchandInfo.ShowInterface(this);
            }
        }

        /// <summary>
        /// Search by currency type a currency stored in the backpack.
        /// </summary>
        /// <param name="type">The type of currency that will be searched within the backapck.</param>
        /// <returns>Returns the currency if found, null if not.</returns>
        public StackableItem GetCurrency(Currency.Type currencyType) {
            foreach (StackableItem stackedItem in merchandInfo.itemsToSell) {
                if (stackedItem.item.GetType() == typeof(Currency)) {
                    Currency c = (Currency)stackedItem.item;
                    if (c.type == currencyType) {
                        return stackedItem;
                    }
                }
            }
            return null;
        }
    }
}