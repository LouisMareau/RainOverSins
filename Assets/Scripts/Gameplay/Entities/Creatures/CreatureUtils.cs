namespace RoS.Gameplay.Creatures.Utils
{
    /// <summary>
    ///     BEAST: Quite common, beasts are usually bipeds or quadripeds found in forests, deserts, oceans, volcanic or mountain areas.
    ///     MONARQUE: The player can only be linked to one creature of this type at a time. Once linked, other monarques are going to become more aggressive towards the player.
    ///     RUNIC: Exploits the power of runes found in the world. Very uncommon and smart, these creatures can be very tricky to link to.
    /// </summary>
    public enum CreatureType {
        BEAST,
        MONARQUE,
        RUNIC
    }

    /// <summary>
    ///     AGGRESSIVE: The creature will likely attack on sight or be difficult to link to.
    ///     CALME: The creature is relaxed and can be approached easily without it attacking or running away. A calm creature doesn't mean it will be easy to link to.
    ///     DOCILE: The creature is friendly by nature and will be very easy to approach and probably easy to link to.
    ///     WISE: The creature is smart and is conscient of its actions and surroundings. A wise creature can either be difficult to link to or easy and will try to 
    ///           find a way to find its way with the player.
    /// </summary>
    public enum CreatureBehaviour {
        AGGRESSIVE,
        CALME,
        DOCILE,
        WISE
    }
}