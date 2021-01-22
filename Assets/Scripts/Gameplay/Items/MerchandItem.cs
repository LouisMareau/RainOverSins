namespace RoS.Gameplay.Items
{
    [System.Serializable]
    public class MerchantItem
    {
        /// <summary>
        /// The item that can be stacked. This means that the item can have multiple copies of itself possessed by the player.
        /// </summary>
        public Item item;
        
        /// <summary>
        /// The amount of the same item stacked. Default value should be 1 and cannot be 0.
        /// </summary>
        public int amount = 1;

        /// <summary>
        /// Returns the total weight of the stack.
        /// </summary>
        public float GetTotalWeight() {
            return item.weight * amount;
        }
    }
}