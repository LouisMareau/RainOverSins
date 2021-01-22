namespace RoS.Gameplay.Entities
{
    using System.Collections.Generic;
    using UnityEngine;
    using RoS.Gameplay.Items;
    using RoS.Gameplay.Entities.UI;
    using RoS.Gameplay.Equipment;

    [System.Serializable]
    public class MerchantInfo
    {
        [Header("UI")]
        public MerchantInterface merchantInterface;

        [Header("INVENTORY")]
        public Equipment equipment;

        public void ShowInterface(NPC npc) {
            if (!merchantInterface.gameObject.activeSelf) {
                // We add the modal to the game manager list in order to keep track of any open modal
                GameManager.openModals.Add(merchantInterface.gameObject);

                // We show the modal/interface by setting its active state to true
                merchantInterface.gameObject.SetActive(true);
                // We initialize the interface with its base Init() method
                merchantInterface.Init(npc);
            }
        }
    }
}