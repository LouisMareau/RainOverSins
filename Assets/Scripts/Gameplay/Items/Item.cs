namespace RoS.Gameplay.Items
{
    using UnityEngine;

    [System.Serializable]
    public class Item : MonoBehaviour
    {
        // ** Create a custom editor to create/setup/edit rapidly items

        [Header("CORE")]
        public new string name;
        [Multiline()] public string description;
        public Sprite thumbnail;

        [Header("WEIGHT")]
        public float weight;

        [Header("TRADE INFO")]
        public Trade tradeInfo;

        protected virtual void OnValidate() {
            gameObject.name = this.name;
            tradeInfo.CalculateCosts();
        }
    }
}