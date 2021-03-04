namespace RoS.Gameplay.Items.Editor
{
    using System.IO;
    using UnityEngine;
    using UnityEditor;

    using RoS.Gameplay.Equipment;

    public class ItemEditor : EditorWindow
    {
        #region LOCAL FIELDS
        /// <summary>
        /// This boolean checks whether or not the item is currently being created (button pushed?)
        /// </summary>
        private bool isCreated = false;

        private Item origin;

        private new string name;
        private string description;

        private GameObject template;
        #endregion

        [MenuItem("RoS/Editors/Item")]
        public static void ShowWindow() {
            var window = GetWindow<ItemEditor>();
            window.titleContent = new GUIContent("Item Editor");
            window.Show();
        }

        private void OnGUI() {
            // On button press, we start the creation of a new button
            // We need to instantiate a new game object in the editor, assign it the 
            if (GUILayout.Button("Create New Item")) {
                isCreated = true;
            }
            if (GUILayout.Button("Reset")) {
                isCreated = false;
            }

            if (isCreated) {
                // Item Core info
                EditorGUILayout.LabelField("CORE", EditorStyles.boldLabel);
                EditorGUI.indentLevel++;
                    name = EditorGUILayout.TextField(new GUIContent("Name", "The name of the item."), name);
                    description = EditorGUILayout.TextField(new GUIContent("Description", "The description of the item."), description);
                EditorGUI.indentLevel--;

                // Item Script reference
                EditorGUILayout.LabelField("SCRIPT", EditorStyles.boldLabel);
                EditorGUI.indentLevel++;
                    origin = (Item)EditorGUILayout.ObjectField(new GUIContent("Item", "A reference to the script that will be used to create the prefab (must be an Item inheriting from MonoBehaviour)."), origin, typeof(Item), false);
                EditorGUI.indentLevel--;

                // Button that will open the "Crafting Bench"
                // This new popup window is used to define the crafting options for the item
                if (GUILayout.Button("Open Crafting Bench")) { CraftEditor.ShowWindow(origin); }

                //* Item Export to Prefab >> Item folder (button)
                if (GUILayout.Button("Export to Prefab")) {
                    // We define the path to save the prefab (needs to be specific to each type of item)
                    string path = GetItemExportPath();
                    // We create a new GameObject (template) to base the prefab off of it 
                    template = new GameObject(name);
                    // We save the template as a prefab asset to the specified location
                    PrefabUtility.SaveAsPrefabAsset(template, path);
                    
                    // We finally destroy the game object in the inspector
                    Destroy(template); 
                }
                //* Create instance and give to any entity
            }
        }

        /// <summary>
        /// Returns the full export path depending on the item (origin).
        /// </summary>
        private string GetItemExportPath() {
            // We define the base export path
            string path = Application.dataPath + "/RoS/Items";

            // then, we add the folder depending on the type of item created
            if (origin.GetType() == typeof(Backpack)) {
                path += "/Backpacks";
            }
            else if (origin.GetType() == typeof(Consumable)) {
                path += "/Consumables";
            }
            else if (origin.GetType() == typeof(Currency)) {
                path += "/Currencies";
            }
            else if (origin.GetType() == typeof(RSystem)) {
                path += "/RSystems";
            }

            // We return the full path
            return path;
        }
    } 
}