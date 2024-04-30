using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour{
    [SerializeField] GameObject CardPrefab;
    

    public List<GameObject> Cartas_mano=new List<GameObject>();
    public List<GameObject> Cartas_jugando=new List<GameObject>();
    public bool Change_Option=false;
    public GameObject obj1=null;
    public GameObject obj2=null;

    

    void Start(){
        InitGame();
    }

//referencia para crear todas las cartas 
    public void Card_base(int x,int y,List<GameObject> Lista){
            GameObject Card=Instantiate(CardPrefab);
            Card.transform.position = new Vector3(x,y,0);
            Card.AddComponent<Atributos_cartas>();
            Card.name="Card"+Lista.Count;
            Lista.Add(Card);
            Button button= Card.GetComponent<Button>();
            button.onClick.AddListener(()=> registerCard(Card));
            Material material = Card.GetComponent<Renderer>().material;
            material.color = new Color(0, 1, 0);
    }
    
    // Crea las cartas en la banca
    IEnumerator Creat_card(){
        int x=-2;
        int y=-3;
        int i=0;
        while (i<3){
            yield return new WaitForSeconds(1.0f);
            Card_base(x,y,Cartas_mano);
            x+=2; 
            i++;
        }
        x=6;
        y=0;
        i=0;

        // Genera las cartas iniciale a jugar
        while (i<2){
            yield return new WaitForSeconds(1.0f);
            Card_base(x,y,Cartas_mano);
            y+=3; 
            i++;
        }
        x=-6;
        y=-0;
        i=0;

        //Genera las cartas del enemigo
        while (i<2){
            yield return new WaitForSeconds(1.0f);
            GameObject Card=Instantiate(CardPrefab);
            Card.transform.position = new Vector3(x,y,0);
            Card.AddComponent<Atributos_cartas>();
            Card.name="Card_Enemy"+i;
            y+=3; 
            i++;
        }
        
    }
    void InitGame(){
        StartCoroutine(Creat_card());
    }

    public void Change_Cards(GameObject obj1, GameObject obj2){
        Vector3 temp=obj1.transform.position;
        obj1.transform.position=obj2.transform.position;
        obj2.transform.position=temp;
    }

    public void registerCard(GameObject objeto_carta){
        if (Change_Option==true){
            if (obj2==null){
                obj2 = objeto_carta;
                Change_Cards(obj1,obj2);
            }

            else{
                obj1=objeto_carta;
            }
        }
    }
    public void Change_State(){
        Change_Option=true;
        
        }
}   

