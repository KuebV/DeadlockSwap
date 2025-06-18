

using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata;
using ClickableTransparentOverlay;
using ImGuiNET;
using Vortice.Mathematics;

namespace DeadlockSwap;

public class MainForm() : Overlay(windowWidth:3840, windowHeight:2160, windowTitle:"Deadlock Swapper")
{
	private string GetCurrentMainMenuProfileName() =>
		Utils.GetProfileNames(Paths.MainMenuInput)[StaticVariables.CurrentMainMenuProfileIndex];
	
    private void SetupStyle()
    {
	    // Thanks to Extasy Hosting on UnknownCheats for the style
	    // https://www.unknowncheats.me/forum/1547436-post1.html
	    
        ImGuiStylePtr style = ImGui.GetStyle();
	    style.WindowPadding = new Vector2(15, 15);
		style.WindowRounding = 5.0f;
		style.FramePadding = new Vector2(5, 5);
		style.FrameRounding = 4.0f;
		style.ItemSpacing = new Vector2(12, 8);
		style.ItemInnerSpacing = new Vector2(8, 6);
		style.IndentSpacing = 25.0f;
		style.ScrollbarSize = 15.0f;
		style.ScrollbarRounding = 9.0f;
		style.GrabMinSize = 5.0f;
		style.GrabRounding = 3.0f;
 	
		style.Colors[(int)ImGuiCol.Text] = new Vector4(0.80f, 0.80f, 0.83f, 1.00f);
		style.Colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.24f, 0.23f, 0.29f, 1.00f);
		style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.06f, 0.05f, 0.07f, 1.00f);
		style.Colors[(int)ImGuiCol.PopupBg] = new Vector4(0.07f, 0.07f, 0.09f, 1.00f);
		style.Colors[(int)ImGuiCol.Border] = new Vector4(0.80f, 0.80f, 0.83f, 0.88f);
		style.Colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.92f, 0.91f, 0.88f, 0.00f);
		style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(0.10f, 0.09f, 0.12f, 1.00f);
		style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.24f, 0.23f, 0.29f, 1.00f);
		style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
		style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(0.10f, 0.09f, 0.12f, 1.00f);
		style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(1.00f, 0.98f, 0.95f, 0.75f);
		style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.07f, 0.07f, 0.09f, 1.00f);
		style.Colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.10f, 0.09f, 0.12f, 1.00f);
		style.Colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.10f, 0.09f, 0.12f, 1.00f);
		style.Colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.80f, 0.80f, 0.83f, 0.31f);
		style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
		style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.06f, 0.05f, 0.07f, 1.00f);
		style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(0.80f, 0.80f, 0.83f, 0.31f);
		style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.80f, 0.80f, 0.83f, 0.31f);
		style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.06f, 0.05f, 0.07f, 1.00f);
		style.Colors[(int)ImGuiCol.Button] = new Vector4(0.10f, 0.09f, 0.12f, 1.00f);
		style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.24f, 0.23f, 0.29f, 1.00f);
		style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
		style.Colors[(int)ImGuiCol.Header] = new Vector4(0.10f, 0.09f, 0.12f, 1.00f);
		style.Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
		style.Colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.06f, 0.05f, 0.07f, 1.00f);
		style.Colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
		style.Colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
		style.Colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.06f, 0.05f, 0.07f, 1.00f);
		style.Colors[(int)ImGuiCol.PlotLines] = new Vector4(0.40f, 0.39f, 0.38f, 0.63f);
		style.Colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(0.25f, 1.00f, 0.00f, 1.00f);
		style.Colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.40f, 0.39f, 0.38f, 0.63f);
		style.Colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(0.25f, 1.00f, 0.00f, 1.00f);
		style.Colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.25f, 1.00f, 0.00f, 0.43f);
    }
    protected override void Render()
    {
	    if (Utils.GetAsyncKeyState(Utils.StringToHex(StaticVariables.VirtualKey)))
	    {
		    StaticVariables.ShowWindow = !StaticVariables.ShowWindow;
		    Thread.Sleep(250);
	    }

	    if (!StaticVariables.ShowWindow)
		    return;
	    
        ImGui.Begin("Deadlock Swapper - Written by KuebV");
        SetupStyle();

        if (ImGui.Button("Start Deadlock"))
        {
	        Process process = new Process();
	        process.StartInfo.FileName = Paths.Executable();
	        process.StartInfo.Arguments = StaticVariables.StartupArguments;
	        process.Start();
        }
        
        ImGui.SameLine();
        
        if (ImGui.Button("Hide Deadlock Swapper"))
	        StaticVariables.ShowWindow = false;
        
        if (ImGui.CollapsingHeader("Info"))
        {
	        ImGui.Indent();
            if (ImGui.Button("Open DeadlockSwap Location"))
                Process.Start(new ProcessStartInfo("explorer.exe", Directory.GetCurrentDirectory()));
            
            ImGui.SameLine();
            
            if (ImGui.Button("Open Deadlock Location"))
                Process.Start(new ProcessStartInfo("explorer.exe", new DirectoryInfo(Paths.DeadlockInstallPath).FullName)); // Stupid, but has to be done apparently
				
            ImGui.Text("Deadlock Install Path: " + Paths.DeadlockInstallPath);
            ImGui.Text("Startup Arguments: " + StaticVariables.StartupArguments);
        }

        if (ImGui.CollapsingHeader("Main Menu Swap"))
        {
	        ImGui.Combo("Current Main Menu Profile", 
		        ref StaticVariables.CurrentMainMenuProfileIndex, 
		        Utils.GetProfileNames(Paths.MainMenuInput), 
		        Utils.GetProfileNames(Paths.MainMenuInput).Length);
	        
	        // Hell
	        ImGui.Combo("Current Video", 
		        ref StaticVariables.CurrentMainMenuVideoIndex, 
		        Utils.GetProfileVideoNames(Paths.MainMenuInput, Utils.GetProfileNames(Paths.MainMenuInput)[StaticVariables.CurrentMainMenuProfileIndex]),
		        Utils.GetProfileVideoNames(Paths.MainMenuInput, Utils.GetProfileNames(Paths.MainMenuInput)[StaticVariables.CurrentMainMenuProfileIndex]).Length);
	        
	        if (ImGui.Button("Apply"))
	        {
		        File.Copy(Utils.GetProfileVideoPath(), Path.Combine(Paths.MainMenu(), "menu_streets_loop2.webm"), true);
	        }

	        ImGui.SameLine();

	        if (ImGui.Button("Randomize"))
	        {
		        int randomProfile = Utils.Random.Next(0, Utils.GetProfileNames(Paths.MainMenuInput).Length);
		        int randomVideo = Utils.Random.Next(0,
			        Utils.GetProfileVideoNames(Paths.MainMenuInput,
				        Utils.GetProfileNames(Paths.MainMenuInput)[randomProfile]).Length);
		        
		        StaticVariables.CurrentMainMenuProfileIndex = randomProfile;
		        StaticVariables.CurrentMainMenuVideoIndex = randomVideo;

	        }
        }
        
        if (ImGui.CollapsingHeader("Settings"))
        {
	        ImGui.InputText("Window Toggle Button", ref StaticVariables.VirtualKey, 4);
	        ImGui.TextLinkOpenURL("Click for Virtual Keycode Help", "https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes");
	        ImGui.Checkbox("Show Window on Startup", ref StaticVariables.ShowWindowOnStartup);

	        ImGui.Checkbox("Allow MP4", ref StaticVariables.AllowMp4);
	        ImGui.Text("NOTE: MP4's may occasionally not load, it's best to use WEBM's for best compatability");

	        if (ImGui.Button("Save Configuration"))
	        {
		        new Configuration
		        {
			        MainMenu_SelectedFile = StaticVariables.CurrentFile,
			        MainMenu_SelectedProfile = StaticVariables.CurrentProfile,
			        VirtualKey = StaticVariables.VirtualKey,
			        ShowWindowOnStartup = StaticVariables.ShowWindowOnStartup,
			        AllowMP4 = StaticVariables.AllowMp4
		        }.Write();
	        }
        }
        
        ImGui.Separator();
        
        if (ImGui.Button("Quit"))
	        Environment.Exit(0);
        
        ImGui.SameLine();
        ImGui.Indent();
        ImGui.Indent(); // Again!

        if (ImGui.Button("Reset to Default"))
			File.Copy(Path.Combine(Paths.MainMenuInput, "Default/menu_streets_loop2.webm"), Path.Combine(Paths.MainMenu(), "menu_streets_loop2.webm"), true);  File.Copy(Path.Combine(Paths.MainMenuInput, "Default/menu_streets_loop2.webm"), Path.Combine(Paths.MainMenu(), "menu_streets_loop2.webm"), true);
        

        ImGui.End();
    }
}