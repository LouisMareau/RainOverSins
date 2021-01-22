namespace RoS.Gameplay
{
    using UnityEngine;
    
    [System.Serializable]
    public class CoreInfo
    {
        public string name; // The name of the entity
        public string title; // The title (or subName) of the entity
        [Multiline()] public string descriptionShort; // A short description of the entity
        [Multiline()] public string descriptionLong; // A long description of the entity 
    }
}