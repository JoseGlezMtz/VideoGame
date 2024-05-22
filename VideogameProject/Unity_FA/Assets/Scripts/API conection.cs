using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIconection : MonoBehaviour
{
    [SerializeField] string url;
    [SerializeField] string getEndpoint;
    CardManager controller;
    // Start is called before the first frame update
    void Start()
    {
        controller=GetComponent<CardManager>();
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
                controller.APIData=result;
                controller.Dataready=true;
                //controller.Create_Board();
            }
        }
    }
}
