namespace RoS.Gameplay.Items
{
    using UnityEngine;
    using RoS.Gameplay.Storages;

    [System.Serializable]
    public class Item : MonoBehaviour
    {
        [Header("CORE")]
        public int id;
        public new string name;
        [Multiline()] public string description;
        public Sprite thumbnail;

        [Header("BACKPACK")]
        public float backpackWeight;

        [Header("TRADE INFO")]
        public Trade tradeInfo;

        protected virtual void OnValidate() {
            gameObject.name = this.name;
            tradeInfo.CalculateCosts();
        }

    }
}