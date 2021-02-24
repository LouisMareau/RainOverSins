namespace RoS.Gameplay 
{
    using System.Collections.Generic;
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        [Header("UI")]
        /// <summary>
        /// Must contain every currently open modals (backpack, equipment, skills, NPCs modals, etc...) in order to keep track of opened modals
        /// </summary>
        public static List<GameObject> openModals;

        [Header("MARKET INFO")]
        public float merchantTaxes = 0.25f;
        public float inflation = 1.00f;

        private void Init() {
            // We initialize the open modals list as an empty list
            if (openModals == null) {   
                openModals = new List<GameObject>();
            }

            // We set the merchant taxes. This taxe should be added to the base cost of every item sold by merchant NPCs
            Market.merchantTaxes = this.merchantTaxes;
            // We set the base inflation for the market (should be set to 1.00 by default)
            Market.inflation = this.inflation;
        }

        private void OnValidate() {
            Init();
        }

        private void Start() {
            Init();
        }
    }
}