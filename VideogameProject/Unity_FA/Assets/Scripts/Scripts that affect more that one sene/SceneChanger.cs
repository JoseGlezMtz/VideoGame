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
    [SerializeField] public Canvas pauseCanvas ;
    
    

    public static void GoTo(string sceneName)
    {
        
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void Pause_canvas_Active()
    {
        pauseCanvas.gameObject.SetActive(true);
    }
    public void Pause_canvas_Unactive()
    {
        pauseCanvas.gameObject.SetActive(false);
    }
}
