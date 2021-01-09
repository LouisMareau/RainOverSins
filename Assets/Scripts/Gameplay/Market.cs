namespace RoS.Gameplay 
{
    [System.Serializable]
    public class Market
    {
        /// <summary>
        /// Marchand taxes are present to force the player to sell its own items and make money that way.
        /// This should force crafting and doing in-game jobs.<br/>
        /// Marchand taxes have to be set automatically and be global.
        /// </summary>
        public static float marchandTaxes;

        /// <summary>
        /// Inflation must be processed on all transactitons, without any excepions.<br/>
        /// The more player are using their currencies, the higher the inflation is going to be.
        /// This means that the higher players' capitals are, the higher inflation is going to be.
        /// Once players stop buying or using their currencies, the inflation is going to lower with time.
        /// </summary>
        public static float inflation;
    }
}