namespace RoS.Modals 
{
    using UnityEngine;

    public class ModalManager : MonoBehaviour 
    {
        public void OpenModal(GameObject modal) { modal.SetActive(true); }
        public void CloseModal(GameObject modal) { modal.SetActive(false); }
        public void ToggleModal(GameObject modal) { modal.SetActive(!modal.activeSelf); }
    }
}