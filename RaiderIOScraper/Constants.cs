namespace RaiderIOScraper
{
    public class ConstantClasses
    {
        public static string Rogue = "Rogue";
        public static string DeathKnight = "Death Knight";
        public static string Mage = "Mage";
        public static string Druid = "Druid";
        public static string Paladin = "Paladin";
        public static string Priest = "Priest";
        public static string Warlock = "Warlock";
        public static string Warrior = "Warrior";
        public static string Hunter = "Hunter";
        public static string Shaman = "Shaman";
        public static string Monk = "Monk";
        public static string Evoker = "Evoker";
    }

    public class Roles
    {
        public static string Tank = "Tank";
        public static string DPS = "DPS";
        public static string Healer = "Healer";
    }

    public record CharacterInfo(string CharacterName, string CharacterRealm, decimal CharacterIo, string Class, string Role);
}
