using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class RegisterMaster : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] Button registerBtn;

    private APIconection apiConnection;

    private void Start()
    {
        registerBtn.onClick.AddListener(RegisterButtonClicked);
        apiConnection = GetComponent<APIconection>();
    }

    private void RegisterButtonClicked()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;


        apiConnection.Register(username, password);
    }
}
