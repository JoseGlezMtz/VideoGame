using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterData : MonoBehaviour
{
    public string registered_name;
    public string registered_password;

    public RegisterData(string username, string password)
    {
        this.registered_name = username;
        this.registered_password = password;
    }
}

