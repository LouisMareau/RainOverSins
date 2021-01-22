namespace RoS.Gameplay.Items.Editor
{
    using UnityEngine;
    using UnityEditor;

    public class ItemEditor : EditorWindow
    {
        #region LOCAL FIELDS
        private int id;
        private new string name;
        private string description;
        private Item origin;
        #endregion

        [MenuItem("RoS/Editors/Item")]
        public static void ShowWindow() {
            var window = GetWindow<ItemEditor>();
            window.titleContent = new GUIContent("Item Editor");
            window.Show();
        }

        private void OnGUI() {
            //* Item Core info
                
                // ID
                // Name
                // Description
            //* Item Script reference
            
            if (GUILayout.Button("Open Crafting Bench")) { CraftEditor.ShowWindow(); }

            //* Item Export to Prefab >> Item folder (button)
            string path = Application.dataPath + "/RoS/Items";
            if (GUILayout.Button("Export to Prefab")) {

            }
            //* Create instance and give to any entity
        }

        /// <summary>
        /// Returns the full export path depending on the item specified as parameter.
        /// </summary>
        /// <param name="item">The item which will be used to find the associated path.</param>
        public string GetItemExportPath(Item item) {
            // We define the base export path
            string path = Application.dataPath + "/RoS/Items";

            // then, we add the folder depending on the type of item created
            if (item.GetType() == typeof(Backpack)) {
                path += "/Backpacks";
            }
            else if (item.GetType() == typeof(Consumable)) {
                path += "/Consumables";
            }
            else if (item.GetType() == typeof(Currency)) {
                path += "/Currencies";
            }
            else if (item.GetType() == typeof(RSystem)) {
                path += "/RSystems";
            }

            // We return the full path
            return path;
        }
    } 
}