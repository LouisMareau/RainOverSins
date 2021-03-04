namespace RoS.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Entity : MonoBehaviour
    {
        [Header("CORE")]
        public CoreInfo info;

        [Header("ATTRIBUTES")]
        public Stats stats;

        [Header("STATUSES")]
        public List<Status> statuses;

        protected virtual void OnValidate() {
            gameObject.name = string.Format("{0} - {1}", info.name, info.title);
        }
    }
}