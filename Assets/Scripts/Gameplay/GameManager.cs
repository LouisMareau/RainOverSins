namespace RoS.Gameplay 
{
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        [Header("MARKET INFO")]
        public float marchandTaxes = 0.25f;
        public float inflation = 1.00f;

        private void Init() {
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