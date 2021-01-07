namespace RoS.Gameplay.Items
{
    using UnityEngine;
    using RoS.Gameplay.Storages;

    [System.Serializable]
    public class Consumable : Item
    {
        [Header("CONSUMABLE")]
        public int usagePerTurn; // Defines the amount of time the item can be used within one turn
    }
}