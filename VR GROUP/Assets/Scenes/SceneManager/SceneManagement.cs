using System.Collections.Generic;
using UnityEngine;

public class SceneManagement
{
    // List of scene names
    private static List<string> scenes = new List<string>
    {
        "Han",
        "John",
        "Nic"
    };

    // pointer/extractor
    private static string currentScene;
    private static int randomNumber;

    public static string current()
    {
        return currentScene; 
    }


    // Call this to pick a random scene and remove it from the list
    public static void RandomiseScene()
    {
        if (scenes.Count > 0)
        {
            randomNumber = UnityEngine.Random.Range(0, scenes.Count); //randomise a number to call random room
            currentScene = scenes[randomNumber];
            Debug.Log("Selected scene: " + currentScene);
            Debug.Log("Total scene: " + scenes.Count);
            scenes.RemoveAt(randomNumber); // remove by index to avoid issues
        }
        else
        {
            // List is empty, fallback scene
            currentScene = "Kenneth Trophy Room";
            Debug.Log("Scene list empty, fallback to: " + currentScene);
        }
    }
}
