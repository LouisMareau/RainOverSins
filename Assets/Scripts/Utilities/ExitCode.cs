/// <summary>
/// Allows a returned ExitCode to associate different exit/return values to a specific action and/or output
/// </summary>
public enum ExitCode {
    Success = 0,
    Full_CSystem_Storage = 1,
    Full_Backpack_Storage = 2,
    Creature_HP_Maxed = 3,
    Creature_Dead = 4
}