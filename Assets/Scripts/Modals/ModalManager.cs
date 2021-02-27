namespace RoS.Modals 
{
    using System.Collections.Generic;
    using UnityEngine;

    public class ModalManager : MonoBehaviour 
    {
        public Dictionary<string, GameObject> modals;

        public void OpenModal(GameObject modal) { modal.SetActive(true); }
        public void CloseModal(GameObject modal) { modal.SetActive(false); }
        public void ToggleModal(GameObject modal) { modal.SetActive(!modal.activeSelf); }

        private void Init() {
            // Add the modals to the dictionary (using a string as the key and the associated GameObject as the value)
        }
    }
}