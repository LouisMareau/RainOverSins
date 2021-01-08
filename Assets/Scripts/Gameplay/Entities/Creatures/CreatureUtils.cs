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
    ///     AGGRESSIVE: The creature will likely attack on sight and/or be difficult to link to.
    ///     CALME: The creature is relaxed and can be approached easily without it attacking or running away. A calm creature doesn't mean it will be easy to link to.
    ///     DOCILE: The creature is friendly by nature and will be very easy to approach and probably easy to link to.
    ///     SLY: The creature is smart and is conscient of its actions and surroundings. A wise creature can either be easy or difficult to link to, and will more than likely try to 
    ///         find a way to mess with the player.
    ///     WISE: The creature is smart by nature and will think things though. It will act to benefit the greater good but doesn't have to include the player in its plans.
    ///     MISCHEVIOUS: The creature is smart and fully aware of its evilness. Mischevious creatures might act differently depending on the situation but will always try to decieve the player.
    ///         Mischevious creatures are very dangerous by nature because they will act as non-mischevious creatures. 
    /// </summary>
    public enum CreatureBehaviour {
        AGGRESSIVE,
        CALME,
        DOCILE,
        SLY,
        WISE
    }
}