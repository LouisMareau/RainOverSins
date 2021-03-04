namespace RoS.Gameplay.Equipment
{
    using UnityEngine;
    using RoS.Gameplay.Items;
    
    [System.Serializable]
    public class Equipment
    {
        /// <summary>
        /// A reference to the backpack gameobject (instanced)
        /// </summary>
        [HideInInspector] public GameObject backpackGO;
        /// <summary>
        /// A reference to the backpack component (instanced)
        /// </summary>
        [HideInInspector] public Backpack backpack;
        [Space()]
        /// <summary>
        /// A reference to the rift system gameobject (instanced)
        /// </summary>
        [HideInInspector] public GameObject rSystemGO;
        /// <summary>
        /// A reference to the rift system component (instanced)
        /// </summary>
        [HideInInspector] public RSystem rSystem;

        [Header("REFS")]
        /// <summary>
        /// A reference to the storage transform
        /// </summary>
        public Transform storagesT;
        /// <summary>
        /// A reference to the clothing transform
        /// </summary>
        public Transform clothingT;

        /// <summary>
        /// Equips the specified piece of equipment on the Entity
        /// </summary>
        /// <param name="obj">The object to equip</param>
        public void Equip(GameObject obj) {
            // The object is a Backpack
            if (obj.GetComponent<Backpack>()) {
                // We first instantiate the object under the appropriate parent
                GameObject instance = MonoBehaviour.Instantiate<GameObject>(obj, storagesT);
                // We assign the instance to the references
                backpackGO = instance;
                backpack = instance.GetComponent<Backpack>();
            }

            // The object is a Rift System (RSystem)
            else if (obj.GetComponent<RSystem>()) {
                // We first instantiate the object under the appropriate parent
                GameObject instance = MonoBehaviour.Instantiate<GameObject>(obj, storagesT);
                // We assign the instance to the references
                rSystemGO = instance;
                rSystem = instance.GetComponent<RSystem>();
            }

            // Others...
        }
    }
}