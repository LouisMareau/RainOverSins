namespace RoS.Gameplay.Items.Editor
{
    using UnityEngine;
    using UnityEditor;

    public class CraftEditor : EditorWindow
    {
        private Item item;

        private void Init(Item item) {
            
        }

        public static void ShowWindow(Item item) {
            var window = GetWindow<CraftEditor>();
            window.titleContent = new GUIContent("Craft Bench");
            window.Show();
        }

        private void OnGUI() {
            // Show the core info of the associated item

            //* Reference to the item
            //* List of items necessary to craft the item
            //* Amount of gold necessary to complete the craft
            //* Location(s) and method (anvil, mana-infused anvil, forge, unique location, etc...) where the item can be crafted
            //* Export data to Item prefab (ask to replace any previous data)
        }
    }
}