namespace RoS.Gameplay.Items
{
    using UnityEngine;

    [System.Serializable]
    public class Consumable : Item
    {
        [Header("CONSUMABLE")]
        /// <summary>
        /// Defines the amount of time the item can be used
        /// </summary>
        public int uses; 
    }
}