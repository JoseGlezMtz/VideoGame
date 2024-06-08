/*
Valentina Gonzalez
30-04-2024
Script added to the game manager of each scene to assign
it to a button with the number of the scene it wants to change to
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    private static string previusScene;
    private static string currentScene;
    private void Start() {
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }
   public static void GoTo(string sceneName){
        if (sceneName=="AudioScene")
        {
            previusScene = currentScene;
        }
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    public static void GoBack(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(previusScene);
    }
}
