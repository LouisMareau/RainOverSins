namespace RoS.Gameplay.Items
{
    using UnityEngine;

    [System.Serializable]
    public class Trade
    {        
        /// <summary>
        /// This is the base Gold price for the item. Supply and Demand calculations aren't included in the base cost value
        /// </summary>
        public int baseCostGold;

        /// <summary>
        /// This is the base Blu price for the item. Supply and Demand calculations aren't included in the base cost value
        /// </summary>
        public int baseCostBlu; 

        [Space()]

        /// <summary>
        /// Suggested price (in Gold) of the item after market value calculations (help for players)
        /// </summary>
        public int suggestedCostGold;

        /// <summary>
        /// Suggested price (in Blu) of the item after market value calculations (help for player)
        /// </summary>
        public int suggestedCostBlu;

        /// <summary>
        /// The final, real cost of the item in Gold after supply and demand calculations and inflation are applied (NPCs should have an increase of prices (25% increase of base cost))
        /// </summary>
        public int marchandCostGold;

        /// <summary>
        /// The final, real cost of the item in Blu after supply and demand calculations and inflation are applied (NPCs shoudl have an increase of prices (25% increase of base cost))
        /// </summary>
        public int marchandCostBlu;

        /// <summary>
        /// * The player should have full control over selling its own items. *
        /// <br/> The Gold value of the item must be set by the player but if left blank, it should be equal to the suggested cost in Gold
        /// </summary>
        [HideInInspector]
        public int sellingPriceGold;

        /// <summary>
        /// * The player should have full control over selling its own items. * 
        /// <br/> The Blu value of the item must be set by the player but if left blank, it should be equal to the suggested cost in Blu
        /// </summary>
        [HideInInspector]
        public int sellingPriceBlu;

        public void CalculateCosts() {
            suggestedCostGold = Mathf.RoundToInt(baseCostGold * Market.inflation);
            suggestedCostBlu = Mathf.RoundToInt(baseCostBlu * Market.inflation);

            marchandCostGold = Mathf.RoundToInt(suggestedCostGold + (suggestedCostGold * Market.marchandTaxes));
            marchandCostBlu = Mathf.RoundToInt(suggestedCostBlu + (suggestedCostBlu * Market.marchandTaxes));

            sellingPriceGold = suggestedCostGold;
            sellingPriceBlu = suggestedCostBlu;
        }   
    }
}