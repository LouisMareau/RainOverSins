namespace RoS.Gameplay.QuestSystem
{
    using System.Collections.Generic;
    using UnityEngine;

    public enum QuestType {
        MAIN,
        SECONDARY,
        HUNT,
        FREE,
        OFFICIAL,
        CRITICAL,
        BACKSTORY,
        REPUTATION,
        SECRET
    }

    [System.Serializable]
    public class Quest 
    {
        public int id; // Required
        private static int count;

        public string title; // Required
        public string description; // Optional
        public string subDescription; // Optional
        public QuestType questType; // Required

        [SerializeField] public GameObject source; // Required

        public List<Object> requirements; // Optional
        public List<Object> rewards; // Optional

        public float? timer; // Optional 

        public List<Quest> parentQuests; // Optional
        public List<Quest> childQuests; // Optional

        public Quest() {
            this.id = count++;
            this.title = "";
            this.description = "";
            this.subDescription = "";
            this.questType = QuestType.FREE;
            this.source = null;
            this.requirements = new List<Object>();
            this.rewards = new List<Object>();
            this.timer = null;
            this.parentQuests = new List<Quest>();
            this.childQuests = new List<Quest>();
        }
    }
}