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
   public static void GoTo(string sceneName){
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
