
namespace DeadlockSwap;

public class Program
{
    public static string FindDeadlockInstallPath()
    {
        const string libraryfolders_vdf = "C:\\Program Files (x86)\\Steam\\steamapps\\libraryfolders.vdf";
        if (!File.Exists(libraryfolders_vdf)) // Probably needs a better way of handling this, but you should have this folder
            Environment.Exit(1);
        
        string[] lines = File.ReadAllLines(libraryfolders_vdf);
        string[] paths = lines.Where(x => x.Contains("\"path\"")).ToArray();

        const string deadlockPath = "\\steamapps\\common\\Deadlock";
        for (int i = 0; i < paths.Length; i++)
        {
            string path = paths[i].Trim().Replace("\"path\"", "").Trim().Replace("\"", ""); // Stupid!
            string combinedPath = path + deadlockPath;
            if (Path.Exists(combinedPath))
                return combinedPath;
        }
        
        return "Not Found";
    }

    public static void FirstTimeSetup()
    {
        if (!Directory.Exists(Paths.MainMenuInput))
            Directory.CreateDirectory(Paths.MainMenuInput);
        

        if (Paths.DeadlockInstallPath == null)
            return;

        string defaultMainMenu = Path.Combine(Paths.MainMenuInput, "Default");

        if (!Directory.Exists(defaultMainMenu))
        {
            Directory.CreateDirectory(defaultMainMenu);
            
            string[] originalFiles = Directory.GetFiles(Paths.MainMenu());
            for (int i = 0; i < originalFiles.Length; i++)
            {
                string fileName = new FileInfo(originalFiles[i]).Name;
                File.Copy(originalFiles[i], Path.Combine(defaultMainMenu, fileName));
            }
        }
        
        


    }
    
    public static void Main(string[] args)
    {
        StaticVariables.StartupArguments = string.Join(" ", args);
        Console.WriteLine("Starting ImGUI");
        Paths.DeadlockInstallPath = FindDeadlockInstallPath();
        
        if (!Path.Exists(Paths.ConfigurationFile))
            Configuration.WriteDefaultConfiguration();
        
        Configuration config = Configuration.GetConfiguration();
        StaticVariables.CurrentFile = config.MainMenu_SelectedFile;
        StaticVariables.ShowWindowOnStartup = config.ShowWindowOnStartup;
        StaticVariables.VirtualKey = config.VirtualKey;
        StaticVariables.ShowWindow = config.ShowWindowOnStartup;
        StaticVariables.AllowMp4 = config.AllowMP4;
        
        if (!File.Exists(Paths.FirstTimeStartup)) // First time startup
        {
            FirstTimeSetup();
            using (StreamWriter sw = new StreamWriter(Paths.FirstTimeStartup))
                sw.Close();
        }
        
        MainForm form = new MainForm();
        
        form.Start().Wait();
    }
}