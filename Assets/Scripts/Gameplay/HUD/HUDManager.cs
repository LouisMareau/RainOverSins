namespace RoS.HUD 
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class HUDManager : MonoBehaviour 
    {
        public static TextMeshProUGUI entityName;
        public static TextMeshProUGUI entityTitle;
        public static Slider hpBar;
        public static Slider mpBar;
        public static TextMeshProUGUI hpValue;
        public static TextMeshProUGUI mpValue;

        private void Awake() {
            entityName = this.transform.Find("Entity INFO").Find("Entity Name").GetComponent<TextMeshProUGUI>();
            entityTitle = this.transform.Find("Entity INFO").Find("Entity Title").GetComponent<TextMeshProUGUI>();
            hpBar = this.transform.Find("Entity INFO").Find("Bars").Find("Entity HP").GetComponent<Slider>();
            mpBar = this.transform.Find("Entity INFO").Find("Bars").Find("Entity MP").GetComponent<Slider>();
            hpValue = this.transform.Find("Entity INFO").Find("Bars").Find("Entity HP").Find("Labels").Find("Value").GetComponent<TextMeshProUGUI>();
            mpValue = this.transform.Find("Entity INFO").Find("Bars").Find("Entity MP").Find("Labels").Find("Value").GetComponent<TextMeshProUGUI>();
        }

        public static void SetName(string name) { entityName.text = name; }
        public static void SetTitle(string title) { entityTitle.text = title; }
        public static void SetHPBar(float maxHP, float currentHP) {
            hpBar.minValue = 0;
            hpBar.maxValue = maxHP;
            hpBar.value = currentHP;
            hpValue.text = currentHP.ToString("F0");
        }
        public static void SetMPBar(float maxMP, float currentMP) {
            mpBar.minValue = 0;
            mpBar.maxValue = maxMP;
            mpBar.value = currentMP;
            mpValue.text = currentMP.ToString("F0");
        }
        public static void SetBars(float maxHP, float currentHP, float maxMP, float currentMP) {
            SetHPBar(maxHP, currentHP);
            SetMPBar(maxMP, currentMP);
        }
    }
}