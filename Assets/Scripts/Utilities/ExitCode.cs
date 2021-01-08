/// <summary>
/// Allows a returned ExitCode to associate different exit/return values to a specific action and/or output
/// </summary>
public enum ExitCode {
    Success = 0, // Returns a success for any type of action/output
    Full_CSystem_Storage = 1, // CSystem storedEntities.Count >= CSystem.maxEntitiesStored
    Full_Backpack_Storage = 2, // Backpack items.Count >= Backpack.maxItemSlots
    Entity_HP_Maxed = 3, // Entity's HP >= entity.stats.maxHealth
    Entity_Dead = 4, // Entity's HP <= 0
    Entity_Dead_Recently = 5, // The entity has been dead for less than three (3) turns (third (3rd) turn excluded)
    Entity_Dead_Late = 6, // The entity has been dead for more than three (3) turns (third (3rd) turn included)
    Entity_Alive = 7, // Entity's HP > 0
}