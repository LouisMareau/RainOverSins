namespace RoS.Gameplay.QuestSystem.Utils
{
    using System.IO;
    using UnityEngine;

    public class QuestUtilities
    {
        public static Quests GetAllQuestsFromJSON(string jsonFile) {
            string path = Application.dataPath + "/Scripts/QuestSystem/" + jsonFile;
            string res = File.ReadAllText(path);
            if (res != null || res != "") {
                return JsonUtility.FromJson<Quests>(res);
            }

            return null;
        }  

        /// <summary>
        ///Saves all quests passed in parameter in the specified file as a JSON object 
        /// </summary>
        /// <param name="jsonFile">The file that the quests will be saved in. The file is being overriden by the quests object.</param>
        /// <param name="quests">The quests object that will be saved.</param>
        public static void SaveAllQuestsToJSON(string jsonFile, Quests quests) {
            string path = Application.dataPath + "/Scripts/QuestSystem/" + jsonFile;
            string questsJSON = JsonUtility.ToJson(quests);
            File.WriteAllText(path, questsJSON);
        }
    }
}