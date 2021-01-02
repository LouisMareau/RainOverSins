namespace RoS.QuestSystem.Editor
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using RoS.QuestSystem.Utils;
    
    public class QuestSystem : EditorWindow 
    {
        public static Quests quests;
    
        [MenuItem("RoS/Quest System")]
        private static void ShowWindow() {
            var window = GetWindow<QuestSystem>();
            window.titleContent = new GUIContent("Quest System");
            window.Show();
        }

        private void Awake() {
            quests = new Quests();
            // We try to retrieve all the quests from the JSON file
            try { quests = QuestUtilities.GetAllQuestsFromJSON("Quests.json"); }
            // Otherwise, we log the error
            catch (Exception e) { Debug.Log(e); }
        }
    
        private void OnGUI() {
            if (GUILayout.Button("New Quest")) { QuestEditor.Init(); }

            EditorGUILayout.LabelField("QUESTS", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
                DisplayQuestLabels(quests.mainQuests);
                DisplayQuestLabels(quests.secondaryQuests);
                DisplayQuestLabels(quests.hunts);
                DisplayQuestLabels(quests.freeQuests);
                DisplayQuestLabels(quests.officialQuests);
                DisplayQuestLabels(quests.criticalQuests);
                DisplayQuestLabels(quests.backstories);
                DisplayQuestLabels(quests.reputationQuests);
                DisplayQuestLabels(quests.secretQuests);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space(15);

            if (GUILayout.Button("Save Quests")) { QuestUtilities.SaveAllQuestsToJSON("Quests.json", quests); }
        }

        private void DisplayQuestLabels(List<Quest> list) {
            if (list.GetType() == typeof(List<Quest>)) {
                foreach (Quest q in list) {
                    EditorGUILayout.LabelField(string.Format("[{0}] {1} - {2}", q.id, q.questType.ToString(), q.title));
                }
            }
        }
    }
}