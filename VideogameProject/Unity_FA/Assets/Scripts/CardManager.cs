using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

/*
TO-DO
1. Definir variables para PU
    lista de PU en la pila
    lista de PU en la mano
    lista de PU descartados
2. Inicializar las cartas en la pila de PU
3. Ordenar la lista de manera aleatoria
4. Agregar funcionalidad de sumar las cartas a la mano cuando le das click a su botón
5. Agregar método para mover la carta a la pila de descarte cuando finalice el turno
6. 
*/

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject CardPrefab;
    
    [SerializeField] Transform parentPrefab;
    
    [SerializeField] public List<GameObject> Cartas_mano = new List<GameObject>();
    [SerializeField] public List<GameObject> Panels = new List<GameObject>();
    

   
    [SerializeField] TMP_Text turnText;
    [SerializeField] TMP_Text energyText;
    [SerializeField]  GameObject energySlider;



   

    private bool changeButtonPressed = false;
    public bool PlayerTurn = false;
    public bool CountCountEnemyTurn = false;
    public bool Change_Option = false;
    public bool Attack_Option = false;
    int card_Index1;
    int card_Index2;

    public GameObject Selected_card1 = null;
    public GameObject Selected_card2 = null;
    public GameObject Selectpowerup = null;
    public GameObject cartAttack;
    
    public int counter = 0;
    public int num_turn;

    public int energy;
    public int max_energy;
    [SerializeField]  Cards allCards;
    
    public string APIData;
    //public string pu_Cards_Data;

    [SerializeField] public bool Dataready = false;
    [SerializeField] public bool PU_Dataready = false;
    [SerializeField] bool GameStarted = false;
    [SerializeField] bool PowerUp_created = false;
    [SerializeField] bool pu_saved = false;

    

    void Start()
    {
        num_turn = 1;
        energy = 20;
        max_energy = 100;

        turnText.text = $"Turn: {num_turn}";
        energyText.text = $"{energy}";
        GetComponent<APIconection>().get_Character_cards();
        GetComponent<APIconection>().get_PU_cards();
        //InitGame();
    }

    void Update()
    {
        if ((Dataready && PU_Dataready) && !GameStarted)
        {
            InitGame();
            GameStarted = true;
        }
    }

    void InitGame()
    {
        
        StartCoroutine(Create_Board());
       
    }
    //template of all  the cards in the board 
    IEnumerator Create_Board()
    {
        int i = 0;
        
        allCards=JsonUtility.FromJson<Cards>(APIData);
        
        Debug.Log(allCards.cards);
        
        foreach (Atributos atributosCarta in allCards.cards)
        {
            Card_base(Cartas_mano, i, atributosCarta);
            yield return new WaitForSeconds(0.1f);
            i++;
        }
        
        GetComponent<PU_controller>().Init_PU_ID();
        
        GetComponent<PU_controller>().PU_button();
    
        PlayerTurn = true;
    }

    public void Card_base(List<GameObject> Lista, int i, Atributos atributosCarta)
    {
        GameObject Card = Instantiate(CardPrefab, Panels[i].transform.position, Quaternion.identity, parentPrefab);
        Card.GetComponent<CardScript>().Init(atributosCarta);
        Card.GetComponent<CardScript>().selfCard=Card;
        Card.name = "Card" + Lista.Count;
        Lista.Add(Card);
        Debug.Log("Creating: " + Card.name);
        Button button = Card.GetComponent<Button>();
        button.onClick.AddListener(() => registerCard(Card));
        
    }

    //This function changes the position of two cards

    public void Change_Cards(GameObject obj1, GameObject obj2)
    {
        int card_Index1=Cartas_mano.IndexOf(obj1);
        int card_Index2=Cartas_mano.IndexOf(obj2);
        Vector3 tempV = obj1.transform.position;
        obj1.transform.position = obj2.transform.position;
        obj2.transform.position = tempV;
        Debug.Log("index1 change " + Cartas_mano.IndexOf(obj1));
        Debug.Log("index2 change " +Cartas_mano.IndexOf(obj2));
        GameObject temp = obj1;
        Cartas_mano[card_Index1]=obj2;
        Cartas_mano[card_Index2]=temp;
    }

    public void registerCard(GameObject objeto_carta)
    {
    //We check if the power up is selected
        if(Selectpowerup != null)
        {
        //Llamamos a la función de usar power up con la carta y el power up seleccionado
            GetComponent<PU_controller>().UsePowerUp(objeto_carta, Selectpowerup);  
        } 
        
    

        else
        {
            if (Selected_card1==null)
            {
                if (Cartas_mano.IndexOf(objeto_carta) > 4)
                {
                    Debug.Log("Enemy card, can't change it");
                }
            //si no, guardamos la primera carta
                else
                {
                    Selected_card1= objeto_carta;
                    
                    Debug.Log("Selecting " + card_Index1);
                    //Debug.Log("Selecting " + Selected_card1.name);
                    if (!Selected_card1.GetComponent<CardScript>().atributos.Alive)
                    {
                        Debug.Log("This card is Death");
                        Selected_card1 = null;
                    }
                }
            }
            else
            {
                //revisamos si la opcion cambio está activada
                if (Change_Option)
                {
                    Selected_card2 = objeto_carta;
                    int card_Index1=Cartas_mano.IndexOf(Selected_card1);
                    int card_Index2=Cartas_mano.IndexOf(Selected_card2);
                    //We check if the cards are the same so you can't change the card with itself
                    Debug.Log("Changing with " + card_Index2);
                    if (Selected_card1 == Selected_card2)
                    {       //Revisamos que la carta no sea la misma
                        Debug.Log("You can't change the card with itself");
                        Selected_card1 = null;
                        Selected_card2 = null;
                        return;
                        //We check if the cards are in the hand an in attack position so you can change them and your turn end
                    }
                    else if(card_Index1>2 && card_Index2<=2 ||card_Index1<=2 && card_Index2>2 )
                    {
                        Debug.Log("Caso de Prueba 1");
                        Debug.Log("index pre" + Cartas_mano.IndexOf(Selected_card1));
                        Change_Cards(Selected_card1, Selected_card2);
                            
                        //Intercambia las posiciones de los objetos en la lista
                        Debug.Log("index 1" + card_Index1);
                        Debug.Log("index 2" + card_Index2);
                        Selected_card1 = null;
                        Selected_card2 = null;
                        Change_Option = false;
                        //We end the player turn  
                        EndTurn();

                    }
                    else
                    {
                        Debug.Log("Caso de Prueba 2");
                        //en caso de que las cartas estén en la mano del jugador no le quitara un turno
                        Change_Cards(Selected_card1, Selected_card2);
                        
                        GameObject temp = Selected_card1;

                        //Intercambia las posiciones de los objetos en la lista
                        Cartas_mano[card_Index1]=Selected_card2;
                        Cartas_mano[card_Index2]=Selected_card1;
                        Selected_card1 = null;
                        Selected_card2 = null;
                        Change_Option = false;

                    }
                    
                    
                }
                else if(Attack_Option)
                {
                    //Check the effect of the card of selected_card1 so we can know his effect in the switch
                    CardScript selectedCardScript = Selected_card1.GetComponent<CardScript>();

                    // Revisamos si esta carta ya ha sido usada para atacar previamente en el turno
                    if (Selected_card1.GetComponent<CardScript>().atributos.canAttack==false )
                    {
                        Debug.Log("You have already used this card for attack");
                        return;
                    }

                    Debug.Log("Attack option");
                    //Revisamos si la carta es alguna de las que está jugando el jugador
                    if (Selected_card1 == null && (Cartas_mano.IndexOf(objeto_carta) == 3 || Cartas_mano.IndexOf(objeto_carta) == 4))
                    {
                        Selected_card1 = objeto_carta;
                        Debug.Log("Selected card for attacking: " + Selected_card1.name);
                    }
                    
                    // switch to check the effect of the card
                    switch (selectedCardScript.atributos.effect) 
                    {
                        // in case the effect is to deal damage
                        case "dano":
                            Debug.Log("Attack option");
                            // en caso de que sea una de las carta del enemigo la asgnamos a la carta 2
                            if (Selected_card1 != null && (Cartas_mano.IndexOf(objeto_carta) == 5 || Cartas_mano.IndexOf(objeto_carta) == 6))
                            {
                    
                                Selected_card2= objeto_carta;
                                Debug.Log("Selected card to attack: " + Selected_card2.name);
                                //hacer el ataque de las cartas
                                Attack(Selected_card1, Selected_card2);
                                // cambiamos la opcion Can Attack para que ya no se pueda atacar con esa carta
                                
                                Selected_card1 = null;
                                Selected_card2 = null;
                                Attack_Option = false;
                                //En caso de que ya haya usado los dos ataques ya no permite atacar
                                
                                if (counter == 2){
                                    counter = 0;
                                    Attack_Option=false;
                                    Debug.Log("No more attacks available");
                                }
                                if (Cartas_mano[5].GetComponent<CardScript>().atributos.health <= 0 && Cartas_mano[6].GetComponent<CardScript>().atributos.health <= 0)
                                {
                                Debug.Log("You win");
                                PlayerTurn = false;
                                }
                            }
                            break;
                        //In case the effect is to heal a card
                        case "curacion":
                            Debug.Log("Heal option");
                            if (Selected_card1 != null && (Cartas_mano.IndexOf(objeto_carta) == 3 || Cartas_mano.IndexOf(objeto_carta) == 4))
                            {
                        
                                Selected_card2= objeto_carta;
                                Debug.Log("Selected card to heal: " + Selected_card2.name);
                                //hacer el ataque de las cartas
                                Heal(Selected_card1, Selected_card2);
                                // cambiamos la opcion Can Attack para que ya no se pueda atacar con esa carta
                                
                                Selected_card1 = null;
                                Selected_card2 = null;
                                Attack_Option = false;
                                //En caso de que ya haya usado los dos ataques ya no permite atacar
                                
                                if (counter == 2)
                                {
                                    counter = 0;
                                    Attack_Option=false;
                                    Debug.Log("No more attacks available");
                                }
                                if (Cartas_mano[3].GetComponent<CardScript>().atributos.health <= 0 && Cartas_mano[4].GetComponent<CardScript>().atributos.health <= 0)
                                {
                                    Debug.Log("You win");
                                    PlayerTurn = false;

                                }
                            }
                            break;
                        //In case the effect is to boost the damage of a card
                        case "mejora_dano":
                            Debug.Log("Damage boost option");
                            if (Selected_card1 != null && (Cartas_mano.IndexOf(objeto_carta) == 3 || Cartas_mano.IndexOf(objeto_carta) == 4))
                            {

                        
                                Selected_card2= objeto_carta;
                                Debug.Log("Selected card to boost damage: " + Selected_card2.name);
                                //hacer el ataque de las cartas
                                mejoradano(Selected_card1, Selected_card2);
                                // cambiamos la opcion Can Attack para que ya no se pueda atacar con esa carta
                                
                                Selected_card1 = null;
                                Selected_card2 = null;
                                Attack_Option = false;
                                //En caso de que ya haya usado los dos ataques ya no permite atacar
                                
                                if (counter == 2)
                                {
                                    counter = 0;
                                    Attack_Option=false;
                                    Debug.Log("No more attacks available");
                                }
                                if (Cartas_mano[3].GetComponent<CardScript>().atributos.health <= 0 && Cartas_mano[4].GetComponent<CardScript>().atributos.health <= 0)
                                {
                                    Debug.Log("You win");
                                    PlayerTurn = false;
                                }
                            }
                            break;

                        default:
                            Debug.Log("No se a implementado la funcion todavia");
                            break;
                    
                    }
                }
            }
        }
    }
    //If the change option is not active we send error message

        
    //This function is used to change the state of the change option(active in the change button)

    public void Change_State()
    {
        if(Attack_Option == true)
        {
            Debug.Log("You have Attack option active, you can't change now");
            
        }
        else if(PlayerTurn == false)
        {
            Debug.Log("It's not your turn");
            return;
        
        }
        else if(counter != 0)
        {
            Debug.Log("You have already attacked");
            return;
        }
        else{
            if (!changeButtonPressed)
            {
                Change_Option = true;
                changeButtonPressed = true;
            }
            else
            {
                Change_Option = false;
                changeButtonPressed = false;
            }
        }
    }

   

    //Increase Energy amount
    public void IncreaseEnegry(){
        Debug.Log("Energy increased");
        if(energy + (num_turn-1) * 10 <= max_energy)
        {
            energy += (num_turn-1) * 10;
        }
        else if(energy + (num_turn-1) * 10 > max_energy)
        {
            energy = 100;
        }

        Slider sliderComponent = energySlider.GetComponent<Slider>();
        sliderComponent.value = energy;
           
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
                Atributos atributosCarta1 = objeto_carta1.GetComponent<CardScript>().atributos;
                Atributos atributosCarta2 = objeto_carta2.GetComponent<CardScript>().atributos;
                
                if (atributosCarta1.abilityCost<=energy)
                {
                    
                    Debug.Log("objeto 1:"+Cartas_mano.IndexOf(objeto_carta1));
                    Debug.Log("objeto 2:"+Cartas_mano.IndexOf(objeto_carta2));
                    // We check if the cards are in the right position 
                    if ((Cartas_mano.IndexOf(objeto_carta1) == 3 || Cartas_mano.IndexOf(objeto_carta1) == 4) &&
                        (Cartas_mano.IndexOf(objeto_carta2) == 5 || Cartas_mano.IndexOf(objeto_carta2) == 6))
                    {
                        int damageDealt = atributosCarta1.attack - atributosCarta2.resistance;
                        // We check if the damage dealt is greater than 0
                        if (damageDealt <= 0)
                        {
                            Debug.Log("No damage dealt because the enemy card has more resistance than the player card's attack");
                            counter++;
                        }
                        else
                        {

                            // We attack the enemy card 
                            atributosCarta2.health -= damageDealt;
                            Debug.Log(objeto_carta1.name + " attacked " + objeto_carta2.name + " for " + atributosCarta1.attack + " damage.");
                            
                            //Decrease energy amount-- fix later on with the corresponding value
                            energy-= atributosCarta1.abilityCost;
                            energyText.text=$"{energy}";
                            Slider sliderComponent = energySlider.GetComponent<Slider>();
                            sliderComponent.value = energy;
                            objeto_carta1.GetComponent<CardScript>().atributos.canAttack=false;
                            counter++;
                        }
                    }
                    else
                    {
                        Debug.Log("No valid cards to attack");
                    }
                }
                else
                {
                    Debug.Log("No enough energy");
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
    //Function to heal player cards 
    public void Heal (GameObject objeto_carta1, GameObject objeto_carta2)
    {
 
        Atributos atributosCarta1 = objeto_carta1.GetComponent<CardScript>().atributos;
        Atributos atributosCarta2 = objeto_carta2.GetComponent<CardScript>().atributos;
    
        if (atributosCarta1.abilityCost<=energy)
        {
            if ((Cartas_mano.IndexOf(objeto_carta1) == 3 || Cartas_mano.IndexOf(objeto_carta1) == 4) &&
                (Cartas_mano.IndexOf(objeto_carta2) == 3 || Cartas_mano.IndexOf(objeto_carta2) == 4))
            {
                int healthBoost = atributosCarta1.attack;
                atributosCarta2.health += healthBoost;
                if (atributosCarta2.health > 100) 
                {
                    atributosCarta2.health = 100;
                }
                Debug.Log(objeto_carta1.name + " healed " + objeto_carta2.name + " for " + atributosCarta1.attack + " health.");
                energy-= atributosCarta1.abilityCost;
                energyText.text=$"{energy}";
                Slider sliderComponent = energySlider.GetComponent<Slider>();
                sliderComponent.value = energy;
                objeto_carta1.GetComponent<CardScript>().atributos.canAttack=false;
                counter++;
                objeto_carta2.GetComponent<CardScript>().UpdateHealth();
                
            }
            else
            {
                Debug.Log("No valid cards to heal");
            }
        }
        else
        {
            Debug.Log("No enough energy");
        }

    }
//Function to boost the damage of a player card
    public void mejoradano(GameObject objeto_carta1, GameObject objeto_carta2)
    {
        Atributos atributosCarta1 = objeto_carta1.GetComponent<CardScript>().atributos;
        Atributos atributosCarta2 = objeto_carta2.GetComponent<CardScript>().atributos;

        if (atributosCarta1.abilityCost<=energy)
        {
            if ((Cartas_mano.IndexOf(objeto_carta1) == 3 || Cartas_mano.IndexOf(objeto_carta1) == 4) &&
            (Cartas_mano.IndexOf(objeto_carta2) == 3 || Cartas_mano.IndexOf(objeto_carta2) == 4))
            {
                int damageBoost = atributosCarta1.attack;
                atributosCarta2.attack += damageBoost;
                Debug.Log(objeto_carta1.name + " boosted " + objeto_carta2.name + " for " + atributosCarta1.attack + " damage.");
                energy-= atributosCarta1.abilityCost;
                energyText.text=$"{energy}";
                Slider sliderComponent = energySlider.GetComponent<Slider>();
                sliderComponent.value = energy;
                objeto_carta1.GetComponent<CardScript>().atributos.canAttack=false;
                counter++;
            }
        }
    }


    // Function to change the state of the attack option (active in the attack button)
    public void Attack_button()
    {
        if(Change_Option == true)
        {
            Debug.Log("You have Change option active, you can't attack now");
            
        }
        else if (PlayerTurn == false)
        {
                Debug.Log("It's not your turn");
                return;
        }
        else
        {
            Attack_Option=!Attack_Option;
        }
    }

// Function to create the power up button




    public void EndTurn()
    {
        PlayerTurn = false;
        ++num_turn;
        Atributos activeCard1 = Cartas_mano[3].GetComponent<CardScript>().atributos;
        Atributos activeCard2 = Cartas_mano[4].GetComponent<CardScript>().atributos;
        
        GetComponent<PU_controller>().DiscardPU();

        activeCard1.canAttack = true;
        activeCard2.canAttack = true;
        activeCard1.Update_Shield();
        activeCard2.Update_Shield();



        IncreaseEnegry();
        turnText.text = $"Turn: {num_turn}";
        energyText.text = $"{energy}";
        CountCountEnemyTurn = true;
        counter = 0;
        GetComponent<PU_controller>().PowerUp_created = false;

        
        Selected_card1 = null;
        Selected_card2 = null;
        Change_Option = false;
        GetComponent<PU_controller>().pu_saved = false;
        
         GetComponent<EnemyController>().EnemyTurn();
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    
}