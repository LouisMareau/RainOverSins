namespace RoS.Gameplay
{
    using UnityEngine;
    
    [System.Serializable]
    public class Stats 
    {
        [Header("Leveling")]
        public int level; // The current level of the subject
        public float exp; // The current amount of experience the subject has
        public float nextLevelExp; // The total amount of experience the subject needs to increase its level by 1

        [Header("Stat Points")]
        public float primaryStatPoints; // Points that can be distributed between any 'primary' stat
        public float secondaryStatPoints; // Points that can be distributed between any 'seconoday' stat
        
        [Header("Health")]
        public float maxHealth; // The total amount of 'health' the subject has
        [HideInInspector] public float health; // The current amount of 'health' the subject has
        public float healthRegen; // The amount of 'health' the subject will recover every turn
        [Space(7)]
        public float baseHealth; // The base amount of 'health' the subject has at level 1

        [Header("Mana")]
        public float maxMana; // The total amount of 'mana' the subject has
        [HideInInspector] public float mana; // The current amount of 'mana' the subject has
        public float manaRegen; // The amount of 'mana' the subject will recover every turn
        [Space(7)]
        public float baseMana; // The base amount of 'mana' the subject has at level 1

        [Header("Movement")]
        public float maxMp; // The total amount of 'MP' the subject has
        [HideInInspector] public float mp; // The current amount of 'MP' the subject has
        [Space(7)]
        public float baseMp; // The base amount of 'MP' the subject has at level 1

        [Header("HASTE")]
        public float haste;
        [Space(7)]
        public float baseHaste;

        [Header("Primary Attributes")]
        // STRENGTH Attribute
        // -----------------------------------------------
        // 1 point in strength will improve the following:
        //      • +9 health points
        //      • +1% of max health points as health regen
        //      • +1 raw (physical) damage
        //      • +0.3% physical resistance
        public float strength;

        // AGILITY Attribute
        // -----------------------------------------------
        // 1 point in agility will improve the following:
        //      • +0.15 movement point (MP)
        //      • +0.08% dodge
        //      • +3 haste
        public float agility;

        // INTEL Attribute
        // -----------------------------------------------
        // 1 point in intel will improve the following:
        //      • +5 mana points
        //      • +0.1% of max mana points as mana regen
        //      • +1 elemental damage
        //      • +0.2% elemental resistance
        public float intel;

        [Header("Secondary Attributes")]
        // CONSTITUTION Attribute
        // -----------------------------------------------
        // 1 point in constitution will improve the following:
        //      • +21 health points
        //      • +0.8% physical resistance
        //      • +2% of max health points as health regen
        public float constitution;

        // WISDOM Attribute
        // -----------------------------------------------
        // 1 point in wisdom will improve the following:
        //      • +15 mana points
        //      • +0.6% elemental resistance
        //      • +0.25% of max mana points as mana regen
        public float wisdom;

        // FOCUS Attribute
        // -----------------------------------------------
        //      • +0.25 movement point (MP)
        //      • +0.8% of max health points as health regen
        //      • +0.8% of max mana points as mana regen
        //      • +5 haste
        public float focus;

        #region METOHDS
        public float GetExpForNextLevel(int currentLevel) {
            float exp = 1.43f;
            float baseExp = 120;
            return Mathf.Floor(baseExp * (Mathf.Pow(currentLevel, exp)));
        }

        public void LevelUp() {
            if (exp >= nextLevelExp) {
                // We increase the subject level by 1
                level++;
                // We set the amount of exp necessary to get to the next level
                nextLevelExp = GetExpForNextLevel(level);
                // We reset the exp amount to 0
                exp = 0;

                // We give a certain amount of stat points after leveling up
                primaryStatPoints += 3;
                secondaryStatPoints += 1;

                // Each primary stat should increase by 1 (by default) when leveling up
                strength += 1;
                agility += 1;
                intel += 1;

                // We heal the subject with an extra 20% of its max health (check for the health being within the maxHealth limit)
                health += ((maxHealth * 20) / 100);
                if (health > maxHealth) { health = maxHealth; }
                // We regenerate the subject's mana by an extra 20% of its max mana (check for the mana being within the maxMana limit)
                mana += ((maxMana * 20) / 100);
                if (mana > maxMana) { mana = maxMana; }

                UpdateStats();
            }
        }

        public void UpdateStats() {
            // Max Health
            // ----------------------------------------------
            //      -> +4 health per level
            //      -> +9 health per point in strength
            //      -> +21 health per point in constitution 
            maxHealth = baseHealth + (level * 4) + (strength * 9) + (constitution * 21);
            
            // Health Regen
            // ----------------------------------------------
            //      -> 1% of maxHealth by default
            //      -> +1% of maxHealth per point in strength
            //      -> +2% of maxHealth per point in constitution
            //      -> +0.8% of maxHealth per point in focus
            healthRegen = ((1 + strength) * (maxHealth / 100)) + (constitution * ((2 * maxHealth) / 100)) + (focus * ((0.8f * maxHealth) / 100));

            // Max Mana
            // ----------------------------------------------
            //      -> +2 Mana per level
            //      -> +5 Mana per point in intel
            //      -> +15 Mana per point in wisdom
            maxMana = baseMana + (level * 2) + (intel * 5) + (wisdom * 15);

            // Mana Regen
            // ----------------------------------------------
            //      -> 1% of maxMana by default
            //      -> +1% of maxMana per point in intel
            //      -> +2% of maxMana per point in wisdom
            //      -> +0.8% of maxMana per point in focus
            manaRegen = ((1 + intel) * (maxMana / 100)) + (wisdom * ((2 * maxMana) / 100)) + (focus * ((0.8f * maxMana) / 100));

            // Movement Points (MP)
            // ----------------------------------------------
            //      -> +0.1 mp per level
            //      -> +0.15 mp per point in agility
            //      -> +0.25 mp per point in focus
            maxMp = baseMp + (level * 0.1f) + (agility * 0.15f) + (focus * 0.25f);

            // Haste
            // -----------------------------------------------
            //      -> +1 per level
            //      -> +3 per point in agility
            //      -> +5 per point in focus 
            haste = baseHaste + level + (agility * 3) + (focus * 5);
            #endregion
    }
}
}
