namespace RoS.Gameplay.Items
{
    using UnityEngine;
    using RoS.Gameplay.Storages;

    [System.Serializable]
    [CreateAssetMenu(fileName = "New Item", menuName = "RoS/Items/Item")]
    public class Item : ScriptableObject
    {
        [Header("CORE")]
        public new string name;
        [Multiline()] public string description;
        public Sprite thumbnail;

        [Header("INVENTORY")]
        public float backpackWeight;
        public int amount;

        [Header("TRADE INFO")]
        public Trade tradeInfo;

        protected virtual void OnValidate() {
            tradeInfo.CalculateCosts();
        }

    }
}