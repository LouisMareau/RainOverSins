/// <summary>
/// This class is used to store lists that are used as JSON objects.
/// DO NOT MODIFY, IN ANY CASE, THIS FILE WITHOUT KNWOING EXACTLY WHAT YOU ARE DOING AND THE STRUCTURE OF THE JSON OBJECTS.
/// </summary>

namespace RoS.QuestSystem
{
    using System.Collections.Generic;

    [System.Serializable]
    public class Quests 
    {
        public List<Quest> mainQuests;
        public List<Quest> secondaryQuests;
        public List<Quest> hunts;
        public List<Quest> freeQuests;
        public List<Quest> officialQuests;
        public List<Quest> criticalQuests;
        public List<Quest> backstories;
        public List<Quest> reputationQuests;
        public List<Quest> secretQuests;

        public Quests() {
            this.mainQuests = new List<Quest>();
            this.secondaryQuests = new List<Quest>();
            this.hunts = new List<Quest>();
            this.freeQuests = new List<Quest>();
            this.officialQuests = new List<Quest>();
            this.criticalQuests = new List<Quest>();
            this.backstories = new List<Quest>();
            this.reputationQuests = new List<Quest>();
            this.secretQuests = new List<Quest>();
        }
    }
}