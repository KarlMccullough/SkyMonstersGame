using UnityEngine;
using UnityEditor;

public class BuildScript
{
    public static void BuildAndroid()
    {
        string[] scenes = { "Assets/GameScene.unity" };
        string buildPath = "../build/SkyMonsters.apk";
        
        // Ensure build directory exists
        System.IO.Directory.CreateDirectory("../build");
        
        BuildPipeline.BuildPlayer(scenes, buildPath, BuildTarget.Android, BuildOptions.None);
        
        Debug.Log("Android build completed: " + buildPath);
    }
}