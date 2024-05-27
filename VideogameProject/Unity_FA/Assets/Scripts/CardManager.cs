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
    [SerializeField] GameObject PuButtonPrefab;
    [SerializeField] GameObject PUPrefab;

    [SerializeField] Transform parentPrefab;
    [SerializeField] Transform PUParentPrefab;

    [SerializeField] Transform PUSlot;

    [SerializeField] public List<GameObject> Cartas_mano = new List<GameObject>();
    [SerializeField] public List<GameObject> Panels = new List<GameObject>();
    

    [SerializeField] List<GameObject> pu_Hand;
    [SerializeField] List<int> pu_Pile;
    [SerializeField] List<int> pu_Discarded;

    [SerializeField] GameObject AddPUBtn;

    int maxPuPile = 30;

    [SerializeField] TMP_Text turnText;
    [SerializeField] TMP_Text energyText;
    [SerializeField]  GameObject energySlider;



    public bool puAdded;

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
    [SerializeField]  PUs puCards;
    public string APIData;
    public string pu_Cards_Data;

    [SerializeField] public bool Dataready = false;
    [SerializeField] public bool PU_Dataready = false;
    [SerializeField] bool GameStarted = false;
    [SerializeField] bool PowerUp_created = false;
    [SerializeField] bool pu_saved = false;

    /*
    PlayerTurn()
    atributos:
        GameObject activeCard1
            -attack
            -HP (slider o text)
        GameObject activeCard2
            -attack
            -HP
        GameObject selectedCard

        methods:
        attack() ->
        change() -> 
        end() -> end turn 


        FLOW
        1. Select an ally card
        2. Choose to
            a) attack
            b) change
        3. Select an enemy/ally card
        4. Check if there are more options??
            power ups? attack with the other card?
        5. End Turn 

    */

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
    //template of all  the cards in the board 

    public void Card_base(List<GameObject> Lista, int i, Atributos atributosCarta)
    {
        GameObject Card = Instantiate(CardPrefab, Panels[i].transform.position, Quaternion.identity, parentPrefab);
       // Card.AddComponent<Atributos>().SetAtributos(atributosCarta);
        Card.GetComponent<CardScript>().Init(atributosCarta);
        Card.name = "Card" + Lista.Count;
        Lista.Add(Card);
        Debug.Log("Creating: " + Card.name);
        Button button = Card.GetComponent<Button>();
        button.onClick.AddListener(() => registerCard(Card));
        
        
    }

    //template of all the power ups in the board
    //INITIALIZE PU BASED ON ID
    public void Create_PU()
    {
        if (PowerUp_created)
        {
            Debug.Log("Power up already created");
            return;
        }
        else if (!PlayerTurn)
        {
            Debug.Log("It's not your turn");
        }
        else{
        //Agregar for loop para crear los 30 power ups
        //Agregar los objects a la lista de pile
        GameObject PU = Instantiate(PUPrefab, PUParentPrefab.transform.position, Quaternion.identity, PUParentPrefab);
        Debug.Log(pu_Pile[0]);
        foreach (AtributosPU pu in puCards.powerUps)
        {
            Debug.Log("in for loop");
            if (pu.id == pu_Pile[0])
            {
                PU.GetComponent<PUscript>().Init(pu);
            }
        }
        
        Debug.Log("Creating: " + PU.name);
        pu_Pile.RemoveAt(0);
        PowerUp_created = true;
        }

    }


    // create cards in the board

    IEnumerator Create_Board()
    {
        int i = 0;
        //Debug.Log("log"+APIData);
        allCards=JsonUtility.FromJson<Cards>(APIData);
        puCards =JsonUtility.FromJson<PUs>(pu_Cards_Data);
        Debug.Log(allCards.cards);
        Debug.Log(puCards.powerUps);
        foreach (Atributos atributosCarta in allCards.cards)
        {
            //Debug.Log(atributosCarta);
            Card_base(Cartas_mano, i, atributosCarta);
            yield return new WaitForSeconds(0.1f);
            i++;
        }
        /*while (i < Panels.Count)
        {
            Card_base(Cartas_mano, i);
            yield return new WaitForSeconds(0.1f);
            i++;
        }*/
        Init_PU_ID();
        //We create the power up button
        PU_button();
        //We set the player turn to true
        PlayerTurn = true;
    }
    //init the game

    void InitGame()
    {
        
        StartCoroutine(Create_Board());
       
    }

     //all pu logic, discard, add, use

    public void AddPU()
{
    GameObject PowerUp = PUParentPrefab.transform.GetChild(1).gameObject;

    if (pu_Hand.Count == 3)
    {
        Debug.LogError("Can't add more power ups");
    }
    else
    {
        PowerUp.transform.SetParent(PUSlot);
        pu_Hand.Add(PowerUp);
        pu_saved = true;

        // Asignamos el listener para seleccionar el power-up
        Button powerUpButton = PowerUp.GetComponent<Button>();
        powerUpButton.onClick.AddListener(() => SelectPowerUp(PowerUp));
    }

    if (pu_Pile.Count == 0)
    {
        Init_PU_ID();
    }
}

    public void DiscardPU(){
        if (PowerUp_created && !pu_saved){
            GameObject PowerUp = PUParentPrefab.transform.GetChild(1).gameObject;
            Debug.Log(PowerUp);
            Destroy(PowerUp);
            pu_Hand.Remove(PowerUp);

            //AtributosPU atributosPU = PowerUp.GetComponent<AtributosPU>();
            //pu_Discarded.Add(atributosPU.pu_id);
        }
        else{
            Debug.LogError("No power up to discard");
        }
        
    }
    public void PU_button()
    {
        GameObject PU = Instantiate(PuButtonPrefab, PUParentPrefab.transform.position, Quaternion.identity, PUParentPrefab);
        Button button = PU.GetComponent<Button>();
        //We add that we the button is clicked we create a power up
        button.onClick.AddListener(() => Create_PU());
        
    }

    public void Init_PU_ID(){
        for(int i = 14; i <= 35; i++){
            RandomId();
            pu_Pile.Add(i);
        }
    }

    public int RandomId(){
        return Random.Range(8, 35);
    }
    public void SelectPowerUp(GameObject powerUpObject)
{
    Selectpowerup = powerUpObject;
    Debug.Log("Selected PowerUp: " + Selectpowerup.name);
}

    
    // funcion para usar los power ups
    
   public void UsePowerUp(GameObject cardObject, GameObject powerUpObject)
{
    
    Debug.Log("Using power up: " + powerUpObject.name);
    // We check if the power up is null
    if (powerUpObject == null)
    {
        Debug.Log("No power-up selected");
        return;
    }
    // Accedemos al script de la carta y del power-up y lo guardamos en variables
    CardScript cardScript = cardObject.GetComponent<CardScript>();
    PUscript powerUpScript = powerUpObject.GetComponent<PUscript>();
    // Revisamos si los scripts son nulos esto se uso para debuggear
    if (cardScript == null)
    {
        Debug.LogError("CardScript is null");
        return;
    }

    if (powerUpScript == null)
    {
        Debug.LogError("PUscript is null");
        return;
    }
    
    Debug.Log("Power-up effect: " + powerUpScript.atributosPU.ability_effect);
    // Revisamos el efecto del power-up y aplicamos el efecto correspondiente accediendo a los atributos de los power-ups para ver el efecto
    switch (powerUpScript.atributosPU.ability_effect)
    {
        // En caso de que el efecto sea curación
        case "curacion":
            Debug.Log("Applying healing power-up");
            int healthBoost = powerUpScript.atributosPU.ability_amount;
            cardScript.atributos.health += healthBoost;
            Debug.Log("Health boosted by: " + healthBoost);
            break;
        // En caso de que el efecto sea restar de energía
        case  "restaene":
            Debug.Log("Applying energy restore power-up");
            int energyBoost = powerUpScript.atributosPU.ability_amount;
            cardScript.atributos.AbilityCost -= energyBoost;
            Debug.Log("Energy boosted by: " + energyBoost);
            break;
        
        // En caso de que el efecto sea aumentar el daño
        case "mejora_dano":
            Debug.Log("Applying damage boost power-up");
            int damageBoost = powerUpScript.atributosPU.ability_amount;
            cardScript.atributos.Attack += damageBoost;
            Debug.Log("Damage boosted by: " + damageBoost);
            break;
        // En caso de que el efecto sea aumentar la resistencia
        case "mejora_resistencia":
            Debug.Log("Applying resistance boost power-up");
            int resistanceBoost = powerUpScript.atributosPU.ability_amount;
            cardScript.atributos.resistance += resistanceBoost;
            Debug.Log("Resistance boosted by: " + resistanceBoost);
            break;

        // En caso de que el efecto aplicar un escudo
        case "escudo":
            Debug.Log("Applying shield power-up");
            cardScript.atributos.isShielded = powerUpScript.atributosPU.ability_amount;
            
            break;

            


        
        
        

        // En caso de que el efecto sea desconocido se imprime un mensaje de error esto tambien se uso para debuggear
        default:
            Debug.Log("Unhandled power-up effect: " + powerUpScript.atributosPU.ability_effect);
            break;
    }
    // Se destruye el power-up y se remueve de la lista de power-ups en la mano

    Destroy(powerUpObject);
    pu_Hand.Remove(powerUpObject);

    // Se setea el power-up seleccionado a null al igual que la carta seleccionada
    Selectpowerup = null;
    Selected_card1 = null;

    
    

    Debug.Log("Power-up applied successfully");
}
// end of pu logic





    //This function changes the position of two cards

    public void Change_Cards(GameObject obj1, GameObject obj2)
    {
        Vector3 temp = obj1.transform.position;
        obj1.transform.position = obj2.transform.position;
        obj2.transform.position = temp;
        Debug.Log("index1 change " + Cartas_mano.IndexOf(obj1));
        Debug.Log("index2 change " +Cartas_mano.IndexOf(obj2));
    }

    public void registerCard(GameObject objeto_carta)
{
    //We check if the power up is selected
     if(Selectpowerup != null)
    {
        //Llamamos a la función de usar power up con la carta y el power up seleccionado
        UsePowerUp(objeto_carta, Selectpowerup);  
        
    
    } 
        
    

     else{
            if (Selected_card1==null){
                if (Cartas_mano.IndexOf(objeto_carta) > 4)
                {
                    Debug.Log("Enemy card, can't change it");
                }
            //si no, guardamos la primera carta
                else{
                    Selected_card1= objeto_carta;
                    
                    Debug.Log("Selecting " + card_Index1);
                    //Debug.Log("Selecting " + Selected_card1.name);
                }
                }
            else{
            //revisamos si la opcion cambio está activada
                if (Change_Option){
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
                    else if(card_Index1>2 && card_Index2<=2 ||card_Index1<=2 && card_Index2>2 ){
                            Debug.Log("Caso de Prueba 1");
                            Debug.Log("index pre" + Cartas_mano.IndexOf(Selected_card1));
                            Change_Cards(Selected_card1, Selected_card2);
                            
                            //Intercambia las posiciones de los objetos en la lista
                            Debug.Log("index 1" + card_Index1);
                            Debug.Log("index 2" + card_Index2);
                            GameObject temp = Selected_card1    ;
                            Cartas_mano[card_Index1]=Selected_card2;
                            Debug.Log("Card Index al que se mueve carta 2:" + card_Index1);
                            Cartas_mano[card_Index2]=temp;
                            Selected_card1 = null;
                            Selected_card2 = null;
                            Change_Option = false;
                            //We end the player turn
                            
                            EndTurn();

                        }
                    else{
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
            else if(Attack_Option){
                // Revisamos si esta carta ya ha sido usada para atacar previamente en el turno
                if (Selected_card1.GetComponent<CardScript>().atributos.canAttack==false ){
                    Debug.Log("You have already used this card for attack");
                    return;
                }
                Debug.Log("Attack option");
                //Revisamos si la carta es alguna de las que está jugando el jugador
                if (Selected_card1 == null && (Cartas_mano.IndexOf(objeto_carta) == 3 || Cartas_mano.IndexOf(objeto_carta) == 4)){
                    Selected_card1 = objeto_carta;
                    Debug.Log("Selected card for attacking: " + Selected_card1.name);
                }
                // en caso de que sea una de las carta del enemigo la asgnamos a la carta 2
                else if (Selected_card1 != null && (Cartas_mano.IndexOf(objeto_carta) == 5 || Cartas_mano.IndexOf(objeto_carta) == 6)){
            
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
        
        }else if(counter != 0)
        {
            Debug.Log("You have already attacked");
            return;
        }else{
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

    public void Turn(){
        /*
        is called when the player turn is true

        1. Add energy to the bar
        2. Use button listener to either:
            a) Use a card's ability
            b) Change a card
            c) add the new power up to the hand
        3. Increase number of turn
        4. End Turn (set playerTurn to false)
        5. Call CountCountEnemyTurn

        */
    }

    //Increase Energy amount
    public void IncreaseEnegry(){
        Debug.Log("Energy increased");
        if(energy + (num_turn-1) * 10 <= max_energy){
            energy += (num_turn-1) * 10;
        }
        else if(energy + (num_turn-1) * 10 > max_energy){
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
            
            if (atributosCarta1.AbilityCost<=energy)
            {
                Debug.Log("objeto 1:"+Cartas_mano.IndexOf(objeto_carta1));
                Debug.Log("objeto 2:"+Cartas_mano.IndexOf(objeto_carta2));
                // We check if the cards are in the right position 
                if ((Cartas_mano.IndexOf(objeto_carta1) == 3 || Cartas_mano.IndexOf(objeto_carta1) == 4) &&
                    (Cartas_mano.IndexOf(objeto_carta2) == 5 || Cartas_mano.IndexOf(objeto_carta2) == 6))
                {
                   // We attack the enemy card 
                    atributosCarta2.health -= atributosCarta1.Attack;
                    Debug.Log(objeto_carta1.name + " attacked " + objeto_carta2.name + " for " + atributosCarta1.Attack + " damage.");
                    
                    //Decrease energy amount-- fix later on with the corresponding value
                    energy-= atributosCarta1.AbilityCost;
                    energyText.text=$"{energy}";
                    Slider sliderComponent = energySlider.GetComponent<Slider>();
                    sliderComponent.value = energy;
                    objeto_carta1.GetComponent<CardScript>().atributos.canAttack=false;
                    counter++;
                }
                else
                {
                    Debug.Log("No valid cards to attack");
                }
            }
            else{
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


// Function to change the state of the attack option (active in the attack button)
 public void Attack_button(){
    if(Change_Option == true)
    {
        Debug.Log("You have Change option active, you can't attack now");
        
        }
   else if (PlayerTurn == false)
        
        {
            Debug.Log("It's not your turn");
            return;}

        else{
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
    
    DiscardPU();

    activeCard1.CanAttack = true;
    activeCard2.CanAttack = true;
    activeCard1.Update_Shield();
    activeCard2.Update_Shield();



    IncreaseEnegry();
    turnText.text = $"Turn: {num_turn}";
    energyText.text = $"{energy}";
    CountCountEnemyTurn = true;
    counter = 0;
    PowerUp_created = false;

    
    Selected_card1 = null;
    Selected_card2 = null;
    Change_Option = false;
    pu_saved = false;
    
    //We call the function to end the player turn
    EnemyTurn();
    
    
    
    
    
}

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //enemy turn
    

   // Function to attack player cards 
    public void EnemyAttack(GameObject enemyCard, GameObject playerCard)
{
    // First we check if the enemy card and the player card are not null
    if (enemyCard != null && playerCard != null)
    {
        // We set the attributes of the cards enemyCardAtributos for the enemy card and playerCardAtributos for the player card
        Atributos enemyCardAtributos = enemyCard.GetComponent<CardScript>().atributos;
        Atributos playerCardAtributos = playerCard.GetComponent<CardScript>().atributos;
        // We check if the player card is not shielded
        if (playerCardAtributos.isShielded==0)
        {
            playerCardAtributos.health -= enemyCardAtributos.Attack;
            Debug.Log(enemyCard.name + " attacked " + playerCard.name + " for " + enemyCardAtributos.Attack + " damage.");
        }
        else
        {
            // If the player card is shielded we print a message
            Debug.Log(playerCard.name + " is shielded and cannot be attacked this turn.");
        }
    }
    else
    {
        Debug.Log("Error en el ataque del enemigo");
    }
}


void EnemyTurn()
{
    // We check if it's the enemy turn
    if (CountCountEnemyTurn==true)
    {
        // We select a random card from the enemy hand
        int playerCardIndex = Random.Range(3, 5); 
        GameObject playerCard = Cartas_mano[playerCardIndex];
        Debug.Log("Player card selected: " + playerCard.name); 
        // We select a random card from the player hand
        int enemyCardIndex = Random.Range(5, 7); 
        GameObject enemyCard = Cartas_mano[enemyCardIndex];
        Debug.Log("Enemy card selected: " + enemyCard.name); 
        //llamamos a la funcion de ataque
        EnemyAttack(enemyCard, playerCard);
        // We end the enemy turn
        CountCountEnemyTurn = false;
        // We set the player turn to true
        PlayerTurn = true;

    }
}

    
    





}