using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.Networking;
using System.Text;

[System.Serializable]
public class LoginData
{
    public string username;
    public string password;

    public LoginData(string username, string password)
    {
        this.username= username;
        this.password= password;
    }
}

public class LoginManager : MonoBehaviour
{
    [SerializeField] string username;
    [SerializeField] string password;
    [SerializeField] string url;
    [SerializeField] string endpoint;
    [SerializeField] UnityEngine.UI.Button signInButton;
    [SerializeField] UnityEngine.UI.Button registerButton;

    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_InputField passwordInput;


    private void Start()
    {
        signInButton.onClick.AddListener(OnRegisterButtonClicked);
    }

    private void OnRegisterButtonClicked()
    {
        string username= usernameInput.text;
        string password= passwordInput.text;

        //StartCoroutine(SendRequest(username, password, url + endpoint));
    }

    /*
        private IEnumerator SendRequest(string username, string password, string url)
    {
        LoginData loginData= new LoginData(username, password);
        string jsonData= JsonUtility.ToJson(loginData);

        using(UnityWebRequest= WWW = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw= Encoding.UTF8.GetBytes(jsonData);
            
        }

    }
    */

    


}
