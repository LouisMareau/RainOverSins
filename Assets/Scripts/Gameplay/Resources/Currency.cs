namespace RoS.Gameplay.Items
{
    using UnityEngine;
    using RoS.Gameplay.Items;

    [System.Serializable]
    public class Currency : Item
    {
        public enum Type {
            GOLD,
            BLU
        }

        [Header("TYPE")]
        public Type type;

        protected override void OnValidate() {
            switch (type) {
                case Type.GOLD:
                    this.name = "Gold";
                    this.description = "The most common currency on Olidor. It can be traded against (almost) anything, with (almost) anyone.";
                break;

                case Type.BLU:
                    this.name = "Blu";
                    this.description = "A rare currency infused with mana that can only be crafted by mana-infused individuals. Its rarity is intrinsic to the rarity of the materials composing the coin itself. The mana serves as a stamp of authenticity, tracking back to the maker itself.";
                break;
            }

            tradeInfo.CalculateCosts();
        }
    }
}