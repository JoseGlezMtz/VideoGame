using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject CardPrefab;
    
    [SerializeField] Transform parentPrefab;
    
    [SerializeField] public List<GameObject> Cartas_mano = new List<GameObject>();
    [SerializeField] public List<GameObject> Panels = new List<GameObject>();
   
    [SerializeField] public TMP_Text gameText;
    [SerializeField] public TMP_Text turnText;
    [SerializeField] public TMP_Text RoundText;
    [SerializeField] TMP_Text energyText;
    [SerializeField]  GameObject energySlider;

    [SerializeField] GameObject explosion;

    [SerializeField] Transform position1;
    [SerializeField] Transform position2;

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
    [SerializeField]  Cards DeckCards;
    
    public string Characters_Data;
    public string Deck_Data;
    
    

    [SerializeField] public bool Character_Dataready = false;
    [SerializeField] public bool PU_Dataready = false;
    public bool Deck_Dataready;
    [SerializeField] bool GameStarted = false;
    [SerializeField] bool PowerUp_created = false;
    [SerializeField] bool pu_saved = false;
    [SerializeField] public int Rounds;


    void Start()
    {
        num_turn = 1;
        Rounds = 1;
        energy = 20;
        max_energy = 100;

        turnText.text = $"Turn: {num_turn}";
        energyText.text = $"{energy}";
        RoundText.text = $"Round: {Rounds}";
        
        GetComponent<APIconection>().get_Deck_cards(PlayerPrefs.GetInt("id"));
        GetComponent<APIconection>().get_PU_cards();
        GetComponent<APIconection>().get_Character_cards();
        //InitGame();

        Application.logMessageReceived += HandleLog;
    }

    void Update()
    {
        if ((Character_Dataready && PU_Dataready && Deck_Dataready) && !GameStarted)
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
        
        DeckCards=JsonUtility.FromJson<Cards>(Deck_Data);
        allCards=JsonUtility.FromJson<Cards>(Characters_Data);
        Debug.Log(allCards.cards);

        Debug.Log("Game started");
        foreach (Atributos atributosCarta in DeckCards.cards)
        {
            Card_base(Cartas_mano, i, atributosCarta);
            yield return new WaitForSeconds(0.1f);
            i++;
        }

        GetComponent<EnemyController>().Start_Enemy_Cards(allCards);
        GetComponent<PU_controller>().Init_PU_ID();
        GetComponent<PU_controller>().PU_button();
    
        PlayerTurn = true;
    }
    void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        gameText.text = logString;
    }
    

    public void Card_base(List<GameObject> Lista, int i, Atributos atributosCarta)
    {
        GameObject Card = Instantiate(CardPrefab, Panels[i].transform.position, Quaternion.identity, parentPrefab);
        Card.GetComponent<CardScript>().Init(atributosCarta);
        Card.GetComponent<CardScript>().selfCard=Card;
        Card.name = "Card" + Lista.Count;
        Lista.Add(Card);
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
        Debug.Log("Character " + obj1.GetComponent<CardScript>().atributos.character_name + " changed with " + obj2.GetComponent<CardScript>().atributos.character_name);
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
                    Debug.Log("Enemy card, can't select it");
                }
            //si no, guardamos la primera carta
                else
                {
                    Selected_card1= objeto_carta;
                    
                    Debug.Log("Selecting " + Selected_card1.GetComponent<CardScript>().atributos.character_name);
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
                //revisamos si la carta seleccionada es la misma que la que ya estaba seleccionada
                if (!Change_Option && !Attack_Option)
                {   
                    if (Cartas_mano.IndexOf(objeto_carta) > 4)
                    {
                        Debug.Log("Enemy card, can't select it");
                    }
                    else{
                        if (Selected_card1 == objeto_carta)
                        {
                            Selected_card1 = null;
                            Debug.Log("Unselecting " + objeto_carta.GetComponent<CardScript>().atributos.character_name);
                            return;
                        }
                        else if (Selected_card1 != objeto_carta)
                        {
                            Selected_card1 = objeto_carta;
                            Debug.Log("Selecting " + Selected_card1.GetComponent<CardScript>().atributos.character_name);
                        }
                    }
                }
                //revisamos si la opcion cambio está activada
                if (Change_Option)
                {
                    Selected_card2 = objeto_carta;
                    int card_Index1=Cartas_mano.IndexOf(Selected_card1);
                    int card_Index2=Cartas_mano.IndexOf(Selected_card2);
                    //We check if the cards are the same so you can't change the card with itself
                    Debug.Log("Changing with " + card_Index2);
                    if(card_Index1>4 && card_Index2<=4 ||card_Index1<=4 && card_Index2>4 )
                    {
                        Debug.Log("You can't change the card with the enemy");
                        Selected_card1 = null;
                        Selected_card2 = null;
                        return;
                    }
                    else
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
                        Selected_card1 = null;
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

                            // en caso de que sea una de las carta del enemigo la asgnamos a la carta 2
                            if (Selected_card1 != null && (Cartas_mano.IndexOf(objeto_carta) == 5 || Cartas_mano.IndexOf(objeto_carta) == 6))
                            {
                    
                                Selected_card2= objeto_carta;
                                //corroboramos que la carta del enemigo no este muerta
                                if (Selected_card2.GetComponent<CardScript>().atributos.Alive == false)
                                {
                                    Debug.Log("This card is Death");
                                    Selected_card2 = null;
                                    return;
                                }
                                Debug.Log("Selected card to attack: " + Selected_card2.name);
                                //hacer el ataque de las cartas
                                Attack(Selected_card1, Selected_card2);
                                
                                ResetSelection();
                            }
                            break;
                        //In case the effect is to heal a card
                        case "curacion":
                            if (Selected_card1 != null && (Cartas_mano.IndexOf(objeto_carta) == 3 || Cartas_mano.IndexOf(objeto_carta) == 4))
                            {
                        
                                Selected_card2= objeto_carta;
                                Debug.Log("Selected card to heal: " + Selected_card2.name);
                                //hacer el ataque de las cartas
                                Heal(Selected_card1, Selected_card2);
                                // cambiamos la opcion Can Attack para que ya no se pueda atacar con esa carta
                                ResetSelection();
                            }
                            else
                            {
                                Debug.Log("You can't heal the enemy card");
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
                                ResetSelection();
                               
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
     void ResetSelection()
    {
        Selected_card1 = null;
        Selected_card2 = null;
        Attack_Option = false;
        counter++;

        if (counter == 2)
        {
            
            StartCoroutine(Waitformessage());
        }
        if (Cartas_mano[5].GetComponent<CardScript>().atributos.health <= 0 && Cartas_mano[6].GetComponent<CardScript>().atributos.health <= 0)
        {
            Debug.Log("You win");
            Start_New_Round();
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
       // Debug.Log("Energy increased");
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
                    
                    
                    // We check if the cards are in the right position 
                    if ((Cartas_mano.IndexOf(objeto_carta1) == 3 || Cartas_mano.IndexOf(objeto_carta1) == 4) &&
                        (Cartas_mano.IndexOf(objeto_carta2) == 5 || Cartas_mano.IndexOf(objeto_carta2) == 6))
                    {
                        int damageDealt = atributosCarta1.attack - atributosCarta2.resistance;
                        // We check if the damage dealt is greater than 0
                        if (damageDealt <= 0)
                        {
                            Debug.Log("No damage dealt because the enemy card has more resistance than the player card's attack");
                            
                        }
                        else
                        {

                            // We attack the enemy card 
                            atributosCarta2.health -= damageDealt;
                            Debug.Log(atributosCarta1.character_name + " dealt " + damageDealt + " damage to " + atributosCarta2.character_name);
                            //Decrease energy amount-- fix later on with the corresponding value
                            energy-= atributosCarta1.abilityCost;
                            energyText.text=$"{energy}";
                            Slider sliderComponent = energySlider.GetComponent<Slider>();
                            sliderComponent.value = energy;
                            objeto_carta1.GetComponent<CardScript>().atributos.canAttack=false;
                            
                            objeto_carta2.GetComponent<CardScript>().check_alive();
                            
                            playAnimation(Cartas_mano.IndexOf(objeto_carta2));
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
                Debug.Log(atributosCarta1.character_name + " healed " + atributosCarta2.character_name + " for " + atributosCarta1.attack + " health.");
                energy-= atributosCarta1.abilityCost;
                energyText.text=$"{energy}";
                Slider sliderComponent = energySlider.GetComponent<Slider>();
                sliderComponent.value = energy;
                objeto_carta1.GetComponent<CardScript>().atributos.canAttack=false;
                
                objeto_carta2.GetComponent<CardScript>().check_alive();
                
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
            if(Cartas_mano.IndexOf(objeto_carta1) == Cartas_mano.IndexOf(objeto_carta2))
            {
                Debug.Log("You can't boost the damage of the same card");
            }
            else
            if (atributosCarta2.alredyboosted)
            {
                Debug.Log("You can't boost the damage of a card that has already been boosted");
            }
            else
            
            {
                int damageBoost = atributosCarta1.attack;
                atributosCarta2.attack += damageBoost;
                Debug.Log(atributosCarta1.character_name + " boosted " + atributosCarta2.character_name + " for " + atributosCarta1.attack + " damage.");
                energy-= atributosCarta1.abilityCost;
                energyText.text=$"{energy}";
                Slider sliderComponent = energySlider.GetComponent<Slider>();
                sliderComponent.value = energy;
                objeto_carta1.GetComponent<CardScript>().atributos.canAttack=false;
                objeto_carta2.GetComponent<CardScript>().atributos.alredyboosted = true;
                
            }
            else 
            {
                Debug.Log("No valid cards to boost damage");
            }
        }else {
            Debug.Log("No enough energy");
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
        if (PlayerTurn ){
        PlayerTurn = false;
        ++num_turn;
        Atributos activeCard1 = Cartas_mano[3].GetComponent<CardScript>().atributos;
        Atributos activeCard2 = Cartas_mano[4].GetComponent<CardScript>().atributos;
        Atributos enemyCard1 = Cartas_mano[5].GetComponent<CardScript>().atributos;
        Atributos enemyCard2 = Cartas_mano[6].GetComponent<CardScript>().atributos;
        
        GetComponent<PU_controller>().DiscardPU();

        activeCard1.canAttack = true;
        activeCard2.canAttack = true;
        activeCard1.Update_Shield();
        activeCard2.Update_Shield();
        enemyCard1.Update_CannotAttack();
        enemyCard2.Update_CannotAttack();
        
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
        Debug.Log("Enemy turn started");
        
        StartCoroutine(Waitforenemy());

        }
    }
     IEnumerator Waitforenemy()
    {
         
        yield return new WaitForSeconds(4f);
        GetComponent<EnemyController>().EnemyTurn();
    }  
    IEnumerator Waitformessage()
    {
        
        yield return new WaitForSeconds(3f);
        gameText.text = "No more attacks available";
    }

    public void playAnimation(int position){
        if(position == 5){
            Instantiate(explosion, position1);
        }
        else if (position == 6){
            Instantiate(explosion, position2);
        }
        
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    public void Start_New_Round(){
        PlayerTurn = false;
        GetComponent<EnemyController>().Destroy_Cards();
        foreach (GameObject card in Cartas_mano)
        {
            card.GetComponent<CardScript>().Reset_card();
        }
        StartCoroutine(New_Round());
    }
    IEnumerator New_Round(){
        yield return new WaitForSeconds(4f);
        Debug.Log("New Round");
        Rounds++;
        num_turn=0;

        yield return new WaitForSeconds(4f);
        Debug.Log("New Enemies are coming");
        GetComponent<EnemyController>().Start_Enemy_Cards(allCards);

        Atributos activeCard1 = Cartas_mano[3].GetComponent<CardScript>().atributos;
        Atributos activeCard2 = Cartas_mano[4].GetComponent<CardScript>().atributos;
        activeCard1.canAttack = true;
        activeCard2.canAttack = true;

        IncreaseEnegry();
        turnText.text = $"Turn: {num_turn}";
        energyText.text = $"{energy}";
        RoundText.text = $"Round: {Rounds}";

        GetComponent<PU_controller>().PowerUp_created = false;
        Selected_card1 = null;
        Selected_card2 = null;
        Change_Option = false;
        GetComponent<PU_controller>().pu_saved = false;

        PlayerTurn = true;
        CountCountEnemyTurn = false;
        Debug.Log("Player's turn");
    }
}