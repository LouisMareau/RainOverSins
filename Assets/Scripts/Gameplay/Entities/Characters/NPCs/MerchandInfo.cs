namespace RoS.Gameplay.Entities
{
    using System.Collections.Generic;
    using UnityEngine;
    using RoS.Gameplay.Items;
    using RoS.Gameplay.Entities.UI;

    [System.Serializable]
    public class MerchandInfo
    {
        [Header("UI")]
        public MerchandInterface merchandInterface;

        [Header("ITEMS")]
        public List<StackableItem> itemsToSell;

        public void ShowInterface(NPC npc) {
            if (!merchandInterface.gameObject.activeSelf) {
                // We add the modal to the game manager list in order to keep track of any open modal
                GameManager.openModals.Add(merchandInterface.gameObject);

                // We show the modal/interface by setting its active state to true
                merchandInterface.gameObject.SetActive(true);
                // We initialize the interface with its base Init() method
                merchandInterface.Init(npc);
            }
        }
    }
}