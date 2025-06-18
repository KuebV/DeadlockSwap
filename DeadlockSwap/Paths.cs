namespace DeadlockSwap;

public static class Paths
{
    //public static string HowToPlay = Path.Combine(Program.Config.Deadlock_Install_Path, "game\\citadel\\panorama\\videos");
    //public static string MainMenu = Path.Combine(HowToPlay, "main_menu");
    //public static string HeroAbilities = Path.Combine(HowToPlay, "hero_abilities");
    //public static string Executable = Path.Combine(Program.Config.Deadlock_Install_Path, "game\\bin\\win64\\deadlock.exe");

    public static string ConfigurationFile = Path.Combine(Directory.GetCurrentDirectory(), "config.json");
    public static string DeadlockInstallPath;
    public static string Executable() => Path.Combine(DeadlockInstallPath, "game\\bin\\win64\\deadlock.exe");
    public static string MainMenu() => Path.Combine(DeadlockInstallPath, "game\\citadel\\panorama\\videos\\main_menu");
    
    public static string MainMenuInput = Path.Combine(Directory.GetCurrentDirectory(), "MainMenu");

    public static string FirstTimeStartup = Path.Combine(Directory.GetCurrentDirectory(), "firsttime");
}