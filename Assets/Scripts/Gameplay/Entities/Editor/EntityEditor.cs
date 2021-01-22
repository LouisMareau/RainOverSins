namespace RoS.Gameplay.Entities.Editor
{
    using UnityEngine;
    using UnityEditor;

    public class EntityEditor : EditorWindow
    {
        [MenuItem("RoS/Editors/Entity")]
        public static void ShowWindow() {
            var window = GetWindow<EntityEditor>();
            window.titleContent = new GUIContent("Entity Editor");
            window.Show();
        }

        private void OnGUI() {
            //* Entity Core info
                // ID
                // Name
                // Description
            //* Entity Stats
            //* Entity Avatar (3D model + Rig)
            //* Entity Script reference
            //* Entity Export to Prefab >> Entity folder (button)
            //* Create instance and spawn it in the world (raycastHit straight from camera->ground?)
        }
    }
}