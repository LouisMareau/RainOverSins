/// <summary>
/// Allows a returned ExitCode to associate different exit/return values to a specific action and/or output
/// </summary>
public enum ExitCode {
    Success = 0, // Returns a success for any type of action/output
    Entity_HP_Maxed = 1, // Entity's HP >= entity.stats.maxHealth
    Entity_Dead = 2, // Entity's HP <= 0
    Entity_Dead_Recently = 3, // The entity has been dead for less than three (3) turns (third (3rd) turn excluded)
    Entity_Dead_Late = 4, // The entity has been dead for more than three (3) turns (third (3rd) turn included)
    Entity_Alive = 5, // Entity's HP > 0
    Amount_IsZero = 6, // Amount == 0
    Backpack_NewItemAdded = 7, // A new item has been successfuly added to the backpack
    Backpack_AmountIncreased = 8, // The amount of the item has been increased
    Backpack_AmountDecreased = 9, // The amount of the item has been decreased
    Backpack_Full = 10, // Backpack items.Count >= Backpack.maxItemSlots
    RSystem_Full = 11 // RSystem storedEntities.Count >= RSystem.maxEntitiesStored 
}