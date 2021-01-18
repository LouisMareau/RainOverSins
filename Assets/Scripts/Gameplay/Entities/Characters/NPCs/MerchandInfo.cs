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
                merchandInterface.gameObject.SetActive(true);
                merchandInterface.Init(npc);
            }
        }
    }
}