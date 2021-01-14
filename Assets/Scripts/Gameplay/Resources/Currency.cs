namespace RoS.Gameplay.Resources
{
    using UnityEngine;

    [System.Serializable]
    public class Currency : Resource
    {
        public enum Type {
            GOLD,
            BLU
        }

        [Header("TYPE")]
        public Type type;

        [Header("QUANTITY")]
        public float pool;
        [HideInInspector] public float amount;

        public void Init() {
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
        }
    }
}