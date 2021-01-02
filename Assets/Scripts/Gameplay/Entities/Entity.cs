namespace RoS.Gameplay
{
    using UnityEngine;

    public class Entity : MonoBehaviour
    {
        [Header("CORE")]
        public new string name; // The name of the entity
        public string title; // The title (or subName) of the entity
        [Multiline()] public string descriptionShort; // A short description of the entity
        [Multiline()] public string descriptionLong; // A long description of the entity 
    }
}