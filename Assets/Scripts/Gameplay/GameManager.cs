namespace RoS.Gameplay 
{
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        private void Start() 
        {
            // We set the marchand taxes. This taxe should be added to the base cost of every item sold by marchand NPCs
            Market.marchandTaxes = 0.25f;
            // We set the base inflation for the market (should be set to 1.00 by default)
            Market.inflation = 1.00f;
        }
    }
}