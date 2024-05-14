using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [SerializeField] string username;
    [SerializeField] string password;

    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_InputField passwordInput;

    string adminUsername = "adminUser";
    string adminPassword = "adminPassword";

    public Boolean ValidateLogin(){
        //Comparar con usuarios dentro de la BD
        if(username == adminUsername && password==adminPassword){
            return true;
        }
        else{
            return false;
        }
    }

    public void GetUsername(){
        Debug.Log(usernameInput.GetComponent<TextField>());
    }

    public void GetPassword(){
        Debug.Log(passwordInput.GetComponent<TextField>());
    }


}
