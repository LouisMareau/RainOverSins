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
        /// Suggested price (in Gold) of the item after market value calculations (help for players)
        /// </summary>
        public int suggestedCostGold;

        /// <summary>
        /// The final, real cost of the item in Gold after supply and demand calculations and inflation are applied (NPCs should have an increase of prices (25% increase of base cost))
        /// </summary>
        public int marchandCostGold;

        /// <summary>
        /// * The player should have full control over selling its own items. *
        /// <br/> The Gold value of the item must be set by the player but if left blank, it should be equal to the suggested cost in Gold
        /// </summary>
        [HideInInspector]
        public int sellingPriceGold;

        public void CalculateCosts() {
            suggestedCostGold = Mathf.RoundToInt(baseCostGold * Market.inflation);
            marchandCostGold = Mathf.RoundToInt(suggestedCostGold + (suggestedCostGold * Market.marchandTaxes));
            sellingPriceGold = suggestedCostGold;
        }   
    }
}