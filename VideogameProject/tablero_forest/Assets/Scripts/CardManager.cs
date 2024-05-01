using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour{
    [SerializeField] GameObject CardPrefab;
    [SerializeField] Transform parentPrefab;
    public List<GameObject> Cartas_mano=new List<GameObject>();
    [SerializeField] public List<GameObject> Panels=new List<GameObject>();
    public bool Change_Option=false;
    public GameObject obj1=null;
    public GameObject obj2=null;

    

    void Start(){
        InitGame();
    }

    //template of al  the cards in the board 
    public void Card_base(List<GameObject> Lista,int i){
            GameObject Card=Instantiate(CardPrefab, Panels[i].transform.position, Quaternion.identity, parentPrefab);
            Card.AddComponent<Atributos_cartas>();
            Card.name="Card"+Lista.Count;
            Lista.Add(Card);
            Debug.Log("Creating: " + Card.name);
            Button button= Card.GetComponent<Button>();
            button.onClick.AddListener(()=> registerCard(Card));
            Card.GetComponent<Image>().color = Color.HSVToRGB((float)i/Panels.Count, 1, 1);
            
    }
    
    // creat cards in the board
    IEnumerator Creat_card(){
        int i=0;
        while(i< Panels.Count){
            Card_base(Cartas_mano,i);
            yield return new WaitForSeconds(0.1f);
            i++;
        }
        
    }

    //init the game
    void InitGame(){
        StartCoroutine(Creat_card());
    }

    //This function changes the position of two cards
    public void Change_Cards(GameObject obj1, GameObject obj2){
        Vector3 temp=obj1.transform.position;
        obj1.transform.position=obj2.transform.position;
        obj2.transform.position=temp;
    }

    public void registerCard(GameObject objeto_carta){
        //First we check if the cahnge option is active
        if (Change_Option){
            //We check if the card is in the enemy hand
            if (Cartas_mano.IndexOf(objeto_carta)>4 ){
            Debug.Log("Enemy card, can't change it");
            }
            //if it is a card in the player hand we stablish the first card to change
            else{ if (obj1==null){
                obj1 = objeto_carta;
                Debug.Log("Changing with "+obj1.name);
                }
                //If the first card is already stablished we stablished the seconf card and change the cards
                else{
                obj2=objeto_carta;
                Debug.Log("Changing with "+ obj2.name);
                Change_Cards(obj1,obj2);
                //We reset the variables
                obj1=null;
                obj2=null;
                Change_Option=false;    
                }
                }
        }
        //If the change option is not active we send error message
        else {
            Debug.Log("Invalid option");
        }
    }

    //This function is used to change the state of the change option(active in the change button)
    public void Change_State(){
        Change_Option=true;
        
        }
}   

