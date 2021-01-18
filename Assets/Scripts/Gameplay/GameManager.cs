namespace RoS.Gameplay 
{
    using System.Collections.Generic;
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        [Header("UI")]
        /// <summary>
        /// Must contain every currently open modals (backpack, equipment, skills, NPCs modals, etc...) in order to keep track of opened modals.
        /// </summary>
        public static List<GameObject> openModals;

        [Header("MARKET INFO")]
        /// <summary>
        /// The marchand tax over every item in stock.
        /// </summary>
        public float marchandTaxes = 0.25f;

        public float inflation = 1.00f;

        private void Init() {
            // We initialize the open modals list as an empty list
            openModals = new List<GameObject>();

            // We set the marchand taxes. This taxe should be added to the base cost of every item sold by marchand NPCs
            Market.marchandTaxes = this.marchandTaxes;
            // We set the base inflation for the market (should be set to 1.00 by default)
            Market.inflation = this.inflation;
        }

        private void OnValidate() {
            Init();
        }

        private void Start() 
        {
            Init();
        }
    }
}