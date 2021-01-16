namespace RoS.Gameplay.Equipment
{
    using UnityEngine;
    using RoS.Gameplay.Equipment.Storages;
    
    [System.Serializable]
    public class Equipment
    {
        [Header("STORAGES")]
        public Backpack backpack;
        public RSystem rSystem;
    }
}