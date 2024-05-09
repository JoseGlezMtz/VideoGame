using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject CardPrefab;
    [SerializeField] Transform parentPrefab;
    [SerializeField] public List<GameObject> Cartas_mano = new List<GameObject>();
    [SerializeField] public List<GameObject> Panels = new List<GameObject>();
    public bool Change_Option = false;
    public bool Attack_Option = false;
    public GameObject obj1 = null;
    public GameObject obj2 = null;
    public GameObject card1 = null;
    public GameObject card2 = null;

    void Start()
    {
        InitGame();
    }
    //template of al  the cards in the board 

    public void Card_base(List<GameObject> Lista, int i)
    {
        GameObject Card = Instantiate(CardPrefab, Panels[i].transform.position, Quaternion.identity, parentPrefab);
        Card.AddComponent<Atributos>();
        Card.name = "Card" + Lista.Count;
        Lista.Add(Card);
        Debug.Log("Creating: " + Card.name);
        Button button = Card.GetComponent<Button>();
        button.onClick.AddListener(() => registerCard(Card));
        
        Card.GetComponent<Image>().color = Color.HSVToRGB((float)i / Panels.Count, 1, 1);
        //We set the attributes of the cards
        Atributos atributosCarta = Card.GetComponent<Atributos>();
        if (Lista.Count == 6 || Lista.Count==7)
        {
            atributosCarta.HP = 100;
        }
        else if (i == 4 || i == 3 || i==0 || i==1 || i==2 )
       
      
        {
            atributosCarta.Attack = 50;
        }
    }
    // creat cards in the board

    IEnumerator Creat_card()
    {
        int i = 0;
        while (i < Panels.Count)
        {
            Card_base(Cartas_mano, i);
            yield return new WaitForSeconds(0.1f);
            i++;
        }
    }
    //init the game

    void InitGame()
    {
        StartCoroutine(Creat_card());
    }
    //This function changes the position of two cards

    public void Change_Cards(GameObject obj1, GameObject obj2)
    {
        Vector3 temp = obj1.transform.position;
        obj1.transform.position = obj2.transform.position;
        obj2.transform.position = temp;
    }

    public void registerCard(GameObject objeto_carta)
    {
        //First we check if the cahnge option is active

        if (Change_Option)
        {
            //We check if the card is in the enemy hand

            if (Cartas_mano.IndexOf(objeto_carta) > 4)
            {
                Debug.Log("Enemy card, can't change it");
            }
            else
            {
            //if it is a card in the player hand we stablish the first card to change
                if (obj1 == null)
                {
                    obj1 = objeto_carta;
                    Debug.Log("Changing with " + obj1.name);
                }
             //If the first card is already stablished we stablished the seconf card and change the cards

                else
                {
                    obj2 = objeto_carta;
                    Debug.Log("Changing with " + obj2.name);
                    Change_Cards(obj1, obj2);
                    int index1 = Cartas_mano.IndexOf(obj1);
                    int index2 = Cartas_mano.IndexOf(obj2);
                    // Intercambia las posiciones de los objetos en la lista
                    Cartas_mano[index1] = obj2;
                    Cartas_mano[index2] = obj1;
                    obj1 = null;
                    obj2 = null;
                    Change_Option = false;
                }
            }
        }
      //If the change option is not active we send error message

        else if (Attack_Option)
        {
        // we check if the card is in the player hand
            Debug.Log("Attack option");
            if (card1 == null && (Cartas_mano.IndexOf(objeto_carta) == 3 || Cartas_mano.IndexOf(objeto_carta) == 4))
            {
                card1 = objeto_carta;
                Debug.Log("Selected card for attack: " + card1.name);
            }
        // we check if the card is in the enemy hand
            else if (card1 != null && (Cartas_mano.IndexOf(objeto_carta) == 5 || Cartas_mano.IndexOf(objeto_carta) == 6))
            {
            
                card2 = objeto_carta;
                Debug.Log("Selected card for attack: " + card2.name);
                //resets the cards
                Attack(card1, card2);
                card1 = null;
                card2 = null;
                Attack_Option = false;
            }
            }
        else
        {
          Debug.Log("Invalid option");
        }
    }
    //This function is used to change the state of the change option(active in the change button)

    public void Change_State()
    {
        Change_Option = true;
    }


//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

// Function to attack enemy cards
public void Attack(GameObject objeto_carta1, GameObject objeto_carta2)
{
    // First we check if the attack option is active
    if (Attack_Option)
    {
        // We check if the cards are in the player hand
        if (objeto_carta1 != null && objeto_carta2 != null)
        {
            // We set the attributes of the cards atrributesCarta1 for the player card and atributosCarta2 for the enemy card
            Atributos atributosCarta1 = objeto_carta1.GetComponent<Atributos>();
            Atributos atributosCarta2 = objeto_carta2.GetComponent<Atributos>();
            
            if (atributosCarta1 != null && atributosCarta2 != null)
            {
                // We check if the cards are in the right position 
                if ((Cartas_mano.IndexOf(objeto_carta1) == 3 || Cartas_mano.IndexOf(objeto_carta1) == 4) &&
                    (Cartas_mano.IndexOf(objeto_carta2) == 5 || Cartas_mano.IndexOf(objeto_carta2) == 6))
                {
                   // We attack the enemy card 
                    atributosCarta2.HP -= atributosCarta1.Attack;
                    Debug.Log(objeto_carta1.name + " attacked " + objeto_carta2.name + " for " + atributosCarta1.Attack + " damage.");
                }
                else
                {
                    Debug.Log("Invalid cards for attack");
                }
            }
        }
        else
        {
            Debug.Log("Select two cards to attack");
        }
    }
    else
    {
        Debug.Log("Invalid option");
    }
}


// Function to change the state of the attack option (active in the attack button)
 public void Attack_button()
    {
        Attack_Option = true;
    }
}