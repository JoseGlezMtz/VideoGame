using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] Button loginBtn;
    [SerializeField] Button playBtn;

    private APIconection apiConnection;

    public LoginResponse loginStatus;

    public void Start(){
        loginBtn.onClick.AddListener(()=> LoginButtonClicked());
        apiConnection = GetComponent<APIconection>();
        
        
    }

    public void LoginButtonClicked()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
       if(apiConnection!=null){
        apiConnection.Login(username, password);
       }
    }

    public void PlayButtonClicked(){

        if(apiConnection.allowLogin){
            Debug.Log("Changing Scene...");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
        }
    }
}
