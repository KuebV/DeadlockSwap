using System.Text.Json;

namespace DeadlockSwap;

public class Configuration
{
    public string VirtualKey { get; set; }
    public string MainMenu_SelectedProfile { get; set; }
    public string MainMenu_SelectedFile { get; set; }
    public bool ShowWindowOnStartup { get; set; }
    public bool AllowMP4 { get; set; }
    public bool DebugMenu { get; set; }

    public static void WriteDefaultConfiguration()
    {
        using (StreamWriter sw = new StreamWriter(Paths.ConfigurationFile))
        {
            sw.WriteLine(JsonSerializer.Serialize(new Configuration
            {
                VirtualKey = "0x28",
                MainMenu_SelectedProfile = "Default",
                MainMenu_SelectedFile = "menu_streets_loop2.webm",
                ShowWindowOnStartup = true,
                AllowMP4 = false,
                DebugMenu = false
            }, new JsonSerializerOptions { WriteIndented = true }));
            sw.Close();
        }
    }
    
    public static Configuration GetConfiguration() => JsonSerializer.Deserialize<Configuration>(File.ReadAllText(Paths.ConfigurationFile));

    public void Write()
    {
        using (StreamWriter sw = new StreamWriter(Paths.ConfigurationFile))
        {
            sw.WriteLine(JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true }));
            sw.Close();
        }
    }
}