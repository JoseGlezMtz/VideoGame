using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Resources;

public class APIconection : MonoBehaviour
{
    [SerializeField] string url;
    [SerializeField] string getEndpoint;
    [SerializeField] TMP_Text statusTxtLogin;
    [SerializeField] TMP_Text statusTxtRegister;

    public LoginResponse loginCredentials;

    public string serverURL = "http://localhost:4444";
    public string loginEndpoint = "/api/login";
    
    public bool allowLogin;

    CardManager controller;
    PU_controller pu_controller;

    // Start is called before the first frame update
    void Start()
    {
        controller=GetComponent<CardManager>();
        pu_controller=GetComponent<PU_controller>();
    }

    // Update is called once per frame
    public void get_Character_cards(){
        StartCoroutine(Request_Character_Cards("http://localhost:4444/api/Character_card"));
    }

    //aqui pasas como parametro el end point de lo que requieres
    IEnumerator Request_Character_Cards(string url){
        using(UnityWebRequest www = UnityWebRequest.Get(url)){
            yield return www.SendWebRequest();
            if(www.isNetworkError || www.isHttpError){
                Debug.Log("request error" + www.error);
            }else{
                string result=www.downloadHandler.text;
                Debug.Log( result);
                controller.Characters_Data=result;
                controller.Character_Dataready=true;
                //controller.Create_Board();
            }
        }
    }

    public void get_PU_cards(){
        StartCoroutine(Request_PU_Cards("http://localhost:4444/api/Pu_card"));
    }

    IEnumerator Request_PU_Cards(string url){
        using(UnityWebRequest www = UnityWebRequest.Get(url)){
            yield return www.SendWebRequest();
            if(www.isNetworkError || www.isHttpError){
                Debug.Log("request error" + www.error);
            }else{
                string result=www.downloadHandler.text;
                Debug.Log( result);
                pu_controller.pu_Cards_Data=result;
                controller.PU_Dataready=true;
            }
        }
    }

    public void get_Deck_cards(int id){
        StartCoroutine(Request_Deck_Cards($"http://localhost:4444/api/Deck_id/{id}"));
    }

    IEnumerator Request_Deck_Cards(string url){
        Debug.Log("ID "+url);   
        using(UnityWebRequest www = UnityWebRequest.Get(url)){
            yield return www.SendWebRequest();
            if(www.isNetworkError || www.isHttpError){
                Debug.Log("request error" + www.error);
            }else{
                string result=www.downloadHandler.text;
                Debug.Log( result);
                controller.Deck_Data=result;
                controller.Deck_Dataready=true;
                //controller.Create_Board();
            }
        }
    }

    public void Login(string username, string password){
        StartCoroutine(RequestLogin("http://localhost:4444/api/login", username, password));
    }

    IEnumerator RequestLogin(string url, string username, string password)
{
        // Create login data object
        LoginData loginData = new LoginData(username, password);

        // Convert login data object to JSON
        string jsonData = JsonUtility.ToJson(loginData);

        // Log JSON data and URL for debugging
        Debug.Log("JSON data: " + jsonData);
        Debug.Log("URL: " + url);

        // Create a new UnityWebRequest for POST
        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            // Convert JSON string to byte array
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

            // Create an UploadHandlerRaw to send JSON data
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            // Send request and wait for response
            yield return www.SendWebRequest();

            // Check for errors
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                // Log error if request fails
                Debug.LogError("Request error: " + www.error);
                statusTxtLogin.text = "Login Failed :c!";
            }
            else
            {
                // Log response data if request succeeds
                string result = www.downloadHandler.text;
                Debug.Log("Response: " + result);
                
               
                loginCredentials = JsonUtility.FromJson<LoginResponse>(result);

                if(loginCredentials != null){
                    loginData.playerId = loginCredentials.user_id;
                    Debug.Log("Player ID: " + loginData.playerId);
                    loginCredentials.loginSuccesful = true;
                    statusTxtLogin.text = "Login Succesful!";
                    allowLogin = true;
                    PlayerPrefs.SetInt("id", loginCredentials.user_id);
                    PlayerPrefs.SetInt("allowLogin", 1);

                    Debug.Log("Credentials: " + PlayerPrefs.GetInt("id"));
                    SceneChanger.GoTo("Home");
                }
                else{
                    Debug.LogError("Login failed: " + loginCredentials.message);
                    statusTxtLogin.text = "Login Failed :c!";
                }
            }
        }
    }
    
    public void Register(string username, string password){
        StartCoroutine(RequestRegister("http://localhost:4444/api/register", username, password));
    }

    IEnumerator RequestRegister(string url, string username, string password)
    {
        RegisterData registerData = new RegisterData(username, password);
        string jsonData = JsonUtility.ToJson(registerData);

        Debug.Log("JSON data: " + jsonData);

        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Request error: " + www.error);
                statusTxtRegister.text = "That username is already taken";
            }
            else
            {
                string result = www.downloadHandler.text;
                Debug.Log("Response: Todo bien");
                 SceneChanger.GoTo("Login");

                
            }
            

        }
    }

    public void SaveDeck(int player, int card1, int card2, int card3, int card4, int card5){
        Debug.Log("Calling Coroutine Update Deck...");
        StartCoroutine(UpdateDeck("http://localhost:4444/api/update_deck", player, card1, card2, card3, card4, card5));
    }

    IEnumerator UpdateDeck(string url, int playerId, int card1, int card2, int card3, int card4, int card5)
{
    // Create deck update data object
    DeckData deckData = new DeckData(playerId, card1, card2, card3, card4, card5);
    Debug.Log($"Deck DATA: \n {playerId}, {card1}, {card2}, {card3}, {card4}, {card5}");
    Debug.Log($"Deck Data card 3: {deckData.card1}");

    // Convert deck data object to JSON
    string jsonData = JsonUtility.ToJson(deckData);
    Debug.Log("Updated json data VALENTINA: " + jsonData);

    // Log JSON data and URL for debugging
    Debug.Log("JSON data: " + jsonData);
    Debug.Log("URL: " + url);

    // Create a new UnityWebRequest for POST
    using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
    {
        // Convert JSON string to byte array
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        Debug.Log("Json Data Deck Test : " + jsonToSend);

        // Create an UploadHandlerRaw to send JSON data
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        // Send request and wait for response
        yield return www.SendWebRequest();

        // Check for errors
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            // Log error if request fails
            Debug.LogError("Request error: " + www.error);
        }
        else
        {
            // Log response data if request succeeds
            string result = www.downloadHandler.text;
            Debug.Log("Response: " + result);
        }
    }
}
    

    public void characterStats(int cardId, int cardCounter){
       // Debug.Log("Calling Coroutine Update Character Stats");
        StartCoroutine(PostCharacterStats("http://localhost:4444/api/update_characterStats", cardId, cardCounter));
    }

    IEnumerator PostCharacterStats(string url, int card, int counter){
        CharacterStats characterStats = new CharacterStats(counter, card);
        string jsonData = JsonUtility.ToJson(characterStats);
        
        using(UnityWebRequest www = new UnityWebRequest(url, "POST")){
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            
            Debug.LogError("Request error: " + www.error);
        }
        
        }
        
    }

    public void puStats(int cardId, int cardCounter){
        //Debug.Log("Calling coroutine Update PU");
        StartCoroutine(PostPUStats("http://localhost:4444/api/update_powerupStats", cardId, cardCounter));
    }

    IEnumerator PostPUStats(string url, int card, int counter){
        PowerupStats powerupStats = new PowerupStats(counter, card);
        string jsonData = JsonUtility.ToJson(powerupStats);
       // Debug.Log(jsonData);

        using(UnityWebRequest www = new UnityWebRequest(url, "POST")){
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            
            Debug.LogError("Request error: " + www.error);
        }
        
        }
    }

    public void GameResults(int playerId, int num_rounds){
        StartCoroutine(PostResults("http://localhost:4444/api/registerGame", playerId, num_rounds));
    }

   IEnumerator PostResults(string url, int playerId, int numRounds)
{

    GameResults gameResults = new GameResults(playerId, numRounds);
    string jsonData = JsonUtility.ToJson(gameResults);

    using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
    {
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Request error: " + www.error);
        }
        else if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Request succeeded: " + www.downloadHandler.text);
        }
        else
        {
            Debug.LogWarning("Unexpected response: " + www.responseCode);
        }
    }
}

}
