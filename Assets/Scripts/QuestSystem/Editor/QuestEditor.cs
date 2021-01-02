namespace RoS.QuestSystem.Editor 
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    public class QuestEditor : EditorWindow 
    {
        private static QuestEditor window;
        private Quest quest;

        // Quest fields (must reflect the Quest variables)
        private new string title;
        private string description;
        private string subDescription;
        private QuestType questType;
        private GameObject source;
        private List<Object> requirements;
        private List<Object> rewards;
        private float? timer;
        private List<Quest> parentQuests;
        private List<Quest> childQuests;

        public static void Init() {
            window = CreateInstance<QuestEditor>();
            window.Show();
        }

        private void Awake() {
            window = EditorWindow.GetWindow<QuestEditor>();
            window.titleContent = new GUIContent(string.Format("New Quest (Editor)"));

            // Variable initialization
            title = "";
            description = "";
            subDescription = "";
            questType = QuestType.MAIN;
            source = null;
            requirements = new List<Object>();
            rewards = new List<Object>();
            timer = null;
            parentQuests = new List<Quest>();
            childQuests = new List<Quest>();
        }
    
        private void OnGUI() {
            // IDENTIFICATION
            EditorGUILayout.LabelField("IDENTIFICATION", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
                // Title
                title = EditorGUILayout.TextField("Title", title);
                // Desciption
                description = EditorGUILayout.TextField("Description", description);
                // Sub-description
                subDescription = EditorGUILayout.TextField("Sub-description", subDescription);
                // Quest Type
                questType = (QuestType)EditorGUILayout.EnumPopup("Type", questType);
            EditorGUI.indentLevel--;

            // --------------------------------
            GUILayout.Space(15);
            // --------------------------------

            // SOURCES
            EditorGUILayout.LabelField("SOURCES", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
                // Source
                source = (GameObject)EditorGUILayout.ObjectField(source, typeof(GameObject), true);
            EditorGUI.indentLevel--;

            // --------------------------------
            GUILayout.Space(15);
            // --------------------------------

            // REQUIREMENTS
            EditorGUILayout.LabelField("REQUIREMENTS", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
                // Required materials
                if (GUILayout.Button("Add Required Material")) { requirements.Add(new Object()); }
                for (int i = 0; i < requirements.Count; i++) {
                    requirements[i] = EditorGUILayout.ObjectField(requirements[i], typeof(Object), true);
                }
            EditorGUI.indentLevel--;

            // --------------------------------
            GUILayout.Space(15);
            // --------------------------------

            // REWARDS
            EditorGUILayout.LabelField("REWARDS", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
                if (GUILayout.Button("Add reward")) { rewards.Add(new Object()); }
                for (int i = 0; i < rewards.Count; i++) {
                    rewards[i] = EditorGUILayout.ObjectField(rewards[i], typeof(Object), true);
                }
            EditorGUI.indentLevel--;

            // --------------------------------
            GUILayout.Space(15);
            // --------------------------------

            if (GUILayout.Button("Add Quest")) {
                // We setup a new quest
                quest = new Quest() {
                    title = this.title,
                    description = this.description,
                    subDescription = this.subDescription,
                    questType = this.questType,
                    source = this.source,
                    requirements = this.requirements,
                    rewards = this.rewards,
                    timer = this.timer,
                    parentQuests = this.parentQuests,
                    childQuests = this.childQuests
                };

                switch (quest.questType) {
                    case QuestType.MAIN: 
                        QuestSystem.quests.mainQuests.Add(quest);
                        break;
                    case QuestType.SECONDARY:
                        QuestSystem.quests.secondaryQuests.Add(quest);
                        break;
                    case QuestType.HUNT:
                        QuestSystem.quests.hunts.Add(quest);
                        break;
                    case QuestType.FREE:
                        QuestSystem.quests.freeQuests.Add(quest);
                        break;
                    case QuestType.OFFICIAL:
                        QuestSystem.quests.officialQuests.Add(quest);
                        break;
                    case QuestType.CRITICAL:
                        QuestSystem.quests.criticalQuests.Add(quest);
                        break;
                    case QuestType.BACKSTORY:
                        QuestSystem.quests.backstories.Add(quest);
                        break;
                    case QuestType.REPUTATION:
                        QuestSystem.quests.reputationQuests.Add(quest);
                        break;
                    case QuestType.SECRET:
                        QuestSystem.quests.secretQuests.Add(quest);
                        break;
                    default: break;
                }
                
                this.Close();
            }
        }
    }
}