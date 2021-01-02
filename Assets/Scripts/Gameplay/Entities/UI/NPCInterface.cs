namespace RoS.Gameplay.UI
{
    using UnityEngine;
    using TMPro;
    using RoS.Gameplay.Entities;

    public class NPCInterface : MonoBehaviour
    {
        [HideInInspector] public NPC npc;

        [Header("NPC CORE")]
        public new TextMeshProUGUI name;
        public TextMeshProUGUI title;
        public TextMeshProUGUI types;
        public TextMeshProUGUI description;

        public virtual void Init() {
            UpdateDisplay();
        }

        protected virtual void UpdateDisplay() {
            if (npc != null) {
                if (npc.name != "") { name.text = npc.name; }
                if (npc.title != "") { title.text = npc.title; }
                if (npc.descriptionLong != "") { description.text = string.Format("\" {0} \"", npc.descriptionLong); }
                else if (npc.descriptionShort != "") { description.text = string.Format("\" {0} \"", npc.descriptionShort); }
                else { description.text = "Nothing interesting about this person..."; }
            }
        } 
    }
}