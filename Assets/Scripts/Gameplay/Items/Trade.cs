namespace RoS.Gameplay.Items
{
    using UnityEngine;

    [System.Serializable]
    public class Trade
    {        
        /// <summary>
        /// This is the base Gold price for the item. Supply and Demand calculations aren't included in the base cost value
        /// </summary>
        public float baseCostGold;

        /// <summary>
        /// This is the base Blu price for the item. Supply and Demand calculations aren't included in the base cost value
        /// </summary>
        public float baseCostBlu; 

        [Space()]

        /// <summary>
        /// Suggested price (in Gold) of the item after market value calculations (help for players)
        /// </summary>
        public float suggestedCostGold;

        /// <summary>
        /// Suggested price (in Blu) of the item after market value calculations (help for player)
        /// </summary>
        public float suggestedCostBlu;

        /// <summary>
        /// The final, real cost of the item in Gold after supply and demand calculations and inflation are applied (NPCs should have an increase of prices (25% increase of base cost))
        /// </summary>
        public float marchandCostGold;

        /// <summary>
        /// The final, real cost of the item in Blu after supply and demand calculations and inflation are applied (NPCs shoudl have an increase of prices (25% increase of base cost))
        /// </summary>
        public float marchandCostBlu;

        /// <summary>
        /// * The player should have full control over selling its own items. *
        /// <br/> The Gold value of the item must be set by the player but if left blank, it should be equal to the suggested cost in Gold
        /// </summary>
        public float sellingPriceGold;

        /// <summary>
        /// * The player should have full control over selling its own items. * 
        /// <br/> The Blu value of the item must be set by the player but if left blank, it should be equal to the suggested cost in Blu
        /// </summary>
        public float sellingPriceBlu;

        public void CalculateCosts() {
            suggestedCostGold = baseCostGold * Market.inflation;
            suggestedCostBlu = baseCostBlu * Market.inflation;

            marchandCostGold = suggestedCostGold + (suggestedCostGold * Market.marchandTaxes);
            marchandCostBlu = suggestedCostBlu + (suggestedCostBlu * Market.marchandTaxes);

            sellingPriceGold = suggestedCostGold;
            sellingPriceBlu = suggestedCostBlu;
        }   
    }
}