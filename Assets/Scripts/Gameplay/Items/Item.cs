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
        [HideInInspector] public string specsInfo;

        [Header("MARCHAND INFO")]
        public float buyingCostGold;
        public float buyingCostBlu;
        [HideInInspector] public float sellingCostGold;
        [HideInInspector] public float sellingCostBlu;

        private void OnValidate() {
            gameObject.name = this.name;
        }
    }
}