namespace RoS 
{
    using UnityEngine;
    using UnityEditor;

    using RoS.Gameplay.Entities.Editor;
    using RoS.Gameplay.Items.Editor;

    public class RoSEditor : EditorWindow 
    {
        [MenuItem("RoS/Editor")]
        private static void ShowWindow() {
            var window = GetWindow<RoSEditor>();
            window.titleContent = new GUIContent("RoS Editor");
            window.Show();
        }

        private void OnGUI() {
            if (GUILayout.Button("Item Editor")) { ItemEditor.ShowWindow(); }
            if (GUILayout.Button("Entity Editor")) { EntityEditor.ShowWindow(); }
        }
    }
}