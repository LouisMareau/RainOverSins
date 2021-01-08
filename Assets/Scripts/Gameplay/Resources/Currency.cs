namespace RoS.Gameplay.Resources
{
    using UnityEngine;
    
    [System.Serializable]
    public class Currency : Resource
    {
        [Header("QUANTITY")]
        public float amount;
        public float pool;
    }
}