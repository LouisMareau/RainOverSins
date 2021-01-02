namespace RoS.Gameplay.Entities.SkillSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class Skill : MonoBehaviour
    {
        [Header("IDENTITY")]
        public int id;
        [Space]
        public new string name; // The name of the skill
        [TextArea()] public string description; // The description of the skill

        [Header("HUD")]
        public Image icon;
        public Image splash;
        public ExtendedInput.KeyCode shortcut;
    }
}