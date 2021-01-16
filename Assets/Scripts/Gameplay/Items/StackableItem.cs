namespace RoS.Gameplay.Items
{
    [System.Serializable]
    public class StackableItem
    {
        public Item item;

        public int amount = 1;

        public float GetStackWeight() {
            return item.weight * amount;
        }
    }
}