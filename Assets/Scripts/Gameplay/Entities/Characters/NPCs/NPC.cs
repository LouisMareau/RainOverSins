namespace RoS.Gameplay.Entities
{
    using System.Collections.Generic;
    using UnityEngine;
    using RoS.Gameplay.Items;
    using RoS.Gameplay.UI;

    public class NPC : Entity
    {
        [Header("NPC TYPES")]
        // ** Setup a enum that can have multiple options selected for easier access ** //
        public bool isMarchand = false;
        public bool isQuestGiver = false;

        [Header("PHYSICS")]
        private new Collider collider;

        [Header("UI")]
        public GameObject npcWindowPrefab;
        [HideInInspector] public GameObject npcWindow;

        [Header("SELLER")]
        public string sellingMessage; // The line the NPC says when buying from it
        public List<Item> itemsToSell; // The item its selling

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

            if(isMarchand) {
                NPCInterface npcInterface = npcWindow.GetComponent<NPCInterface>();
                npcInterface.npc = this;
                npcInterface.Init();
            }
        }
    }
}