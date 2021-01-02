namespace RoS.Gameplay.Entities
{
    using System.Collections.Generic;
    using UnityEngine;
    using RoS.Gameplay.Items;
    using RoS.Gameplay.UI;

    public class NPC : Entity
    {
        public enum NPCType {
            QUEST,
            MARCHAND,
            DIALOGUE
        }
        
        [Header("BEHAVIOUR")]
        public NPCType npcType;

        [Header("PHYSICS")]
        private new Collider collider;

        [Header("UI")]
        public GameObject npcWindowPrefab;
        [HideInInspector] public GameObject npcWindow;

        [Header("SELLER")]
        public string sellingMessage; // The line the NPC says when buying from it
        public List<GameObject> itemsToSell; // The item its selling

        private void Awake() {
            collider = GetComponent<Collider>();
        }

        private void Update() {
            if (npcWindowPrefab.activeSelf && Input.GetKeyDown(KeyCode.Escape)) {
                Destroy(npcWindow);
            }
        }

        public void OnSelection() {
            Transform npcModalT = GameObject.Find("Modals").transform;
            npcWindow = Instantiate<GameObject>(npcWindowPrefab, npcModalT.position, npcModalT.rotation, npcModalT);

            switch (npcType) {
                case NPCType.QUEST: break;
                
                case NPCType.MARCHAND:
                    NPCInterfaceMarchand npcInterface = npcWindow.GetComponent<NPCInterfaceMarchand>();
                    npcInterface.npc = this;
                    npcInterface.Init();
                    break;

                case NPCType.DIALOGUE: break;

                default: break;
            }
        }
    }
}