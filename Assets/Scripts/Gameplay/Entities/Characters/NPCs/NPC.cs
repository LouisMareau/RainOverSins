namespace RoS.Gameplay.Entities
{
    using UnityEngine;
    using RoS.Gameplay.Items;
    using RoS.Gameplay.Equipment;

    public class NPC : PlayableEntity
    {
        // ** Create a custom editor to create/setup/edit rapidly NPCs
        
        [Header("NPC TYPES")]
        // ** Setup a enum that can have multiple options selected for easier access
        public bool isMerchant = false;
        public bool isQuestGiver = false;

        [Header("MERCHAND")]
        public MerchantInfo merchantInfo;

        public void OnSelection() {
            if (isMerchant) {
                merchantInfo.ShowInterface(this);
            }
        }
    }
}