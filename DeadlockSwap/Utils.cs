using System.Runtime.InteropServices;
using System.Text;

namespace DeadlockSwap;

public class Utils
{
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    public static extern bool GetAsyncKeyState(int vKey);
    
    public static string[] GetProfileNames(string parentDirectory)
    {
        string[] profiles = Directory.GetDirectories(parentDirectory);
        for (int i = 0; i < profiles.Length; i++)
            profiles[i] = new DirectoryInfo(profiles[i]).Name;
        return profiles;
    }

    public static string[] GetProfileVideoNames(string parentDirectory, string profileName)
    {
        string[] videos = Directory.GetFiles(Path.Combine(parentDirectory, profileName), StaticVariables.AllowMp4 ? "*" : "*.webm");
        for (int i = 0; i < videos.Length; i++)
            videos[i] = new FileInfo(videos[i]).Name;
        return videos;
    }

    public static string GetProfileVideoPath()
    {
        string getProfile = Directory.GetDirectories(Paths.MainMenuInput)[StaticVariables.CurrentMainMenuProfileIndex];
        string getVideo = Directory.GetFiles(getProfile, StaticVariables.AllowMp4 ? "*" : "*.webm")[StaticVariables.CurrentMainMenuVideoIndex];
        return getVideo;
    }

    public static int StringToHex(string hexString) => Int32.Parse(hexString.Substring(2), System.Globalization.NumberStyles.HexNumber);

    public static Random Random = new Random();
}