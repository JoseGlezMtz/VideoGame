using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject CardPrefab;
    [SerializeField] GameObject PuButtonPrefab;
    [SerializeField] GameObject PUPrefab;

    [SerializeField] Transform parentPrefab;
    [SerializeField] Transform PUParentPrefab;

    [SerializeField] public List<GameObject> Cartas_mano = new List<GameObject>();
    [SerializeField] public List<GameObject> Panels = new List<GameObject>();
    [SerializeField] public List<GameObject> PUs = new List<GameObject>();

    [SerializeField] TMP_Text turnText;
    [SerializeField] TMP_Text energyText;
    [SerializeField]  GameObject energySlider;

    private bool changeButtonPressed = false;
    public bool PlayerTurn = false;
    public bool Change_Option = false;
    public bool Attack_Option = false;
    int card_Index1;
    int card_Index2;

    public GameObject Selected_card1 = null;
    public GameObject Selected_card2 = null;
    public GameObject cartAttack;
    
    public int counter = 0;
    public int num_turn;

    public int energy;
    public int max_energy;

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

        

        InitGame();
    }
    //template of all  the cards in the board 

    public void Card_base(List<GameObject> Lista, int i)
    {
        GameObject Card = Instantiate(CardPrefab, Panels[i].transform.position, Quaternion.identity, parentPrefab);
        Card.AddComponent<Atributos>();
        Card.name = "Card" + Lista.Count;
        Lista.Add(Card);
        Debug.Log("Creating: " + Card.name);
        Button button = Card.GetComponent<Button>();
        button.onClick.AddListener(() => registerCard(Card));
        
        Image imageComponent = Card.GetComponent<Image>();
            if (imageComponent == null)
            {
                //Debug.LogError("Image component not found on newCard.");
            }
            else
            {
                // Image component found, proceed to set sprite
                imageComponent.sprite = Resources.Load<Sprite>($"CardImages/{i}");
            }
        //We set the attributes of the cards
        //Atributos atributosCarta = Card.GetComponent<Atributos>();
        /*if (Lista.Count == 6 || Lista.Count==7)
        {
            atributosCarta.HP = 100;
        }
        else if (i == 4 || i == 3 || i==0 || i==1 || i==2 )
       
      
        {
            atributosCarta.Attack = 50;
            atributosCarta.AbilityCost = 20;
        }*/
    }

    //template of all the power ups in the board
    public void PU_Base(List<GameObject> Lista)
    {
        GameObject PU = Instantiate(PUPrefab, PUParentPrefab.transform.position, Quaternion.identity, PUParentPrefab);
        PU.AddComponent<Atributos>();
        PU.name = "PU" + Lista.Count;
        Lista.Add(PU);
        Debug.Log("Creating: " + PU.name);
        Button button = PU.GetComponent<Button>();
        
    }
    // creat cards in the board

    IEnumerator Creat_Board()
    {
        int i = 0;
        while (i < Panels.Count)
        {
            Card_base(Cartas_mano, i);
            yield return new WaitForSeconds(0.1f);
            i++;
        }
        //We create the power up button
        PU_button();
        //We set the player turn to true
        PlayerTurn = true;
    }
    //init the game

    void InitGame()
    {
        StartCoroutine(Creat_Board());
       
    }
    //This function changes the position of two cards

    public void Change_Cards(GameObject obj1, GameObject obj2)
    {
        Vector3 temp = obj1.transform.position;
        obj1.transform.position = obj2.transform.position;
        obj2.transform.position = temp;
    }

    public void registerCard(GameObject objeto_carta)
    { //REvisamos si ya tenemos alguna carata guardada
        if (Selected_card1==null){
            if (Cartas_mano.IndexOf(objeto_carta) > 4)
            {
                Debug.Log("Enemy card, can't change it");
            }
            //si no, guardamos la primera carta
            else{
            Selected_card1= objeto_carta;
            int card_Index1=Cartas_mano.IndexOf(Selected_card1);
            Debug.Log("Selecting " + Selected_card1.name);
            }
        }

        else{
            //revisamos si la opcion cambio está activada
            if (Change_Option){
                Selected_card2 = objeto_carta;
                int card_Index2=Cartas_mano.IndexOf(Selected_card2);
                    //We check if the cards are the same so you can't change the card with itself
                Debug.Log("Changing with " + Selected_card2.name);
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
                        Change_Cards(Selected_card1, Selected_card2);
                        
                        //Intercambia las posiciones de los objetos en la lista
                        Cartas_mano[card_Index1]=Selected_card2;
                        Cartas_mano[card_Index2]=Selected_card1;
                        Selected_card1 = null;
                        Selected_card2 = null;
                        Change_Option = false;
                        //We end the player turn
                        EndTurn();

                    }else{
                        //en caso de que las cartas estén en la mano del jugador no le quitara un turno
                        Change_Cards(Selected_card1, Selected_card2);
                        
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
                if (Selected_card1.GetComponent<Atributos>().canAttack==false ){
                    Debug.Log("You have already used this card for attack");
                    return;
                }
                Debug.Log("Attack option");
                //Revisamos si la carta es alguna de las que está jugando el jugador
                if (Selected_card1 == null && (Cartas_mano.IndexOf(objeto_carta) == 3 || Cartas_mano.IndexOf(objeto_carta) == 4)){
                    Selected_card1 = objeto_carta;
                    Debug.Log("Selected card for attack: " + Selected_card1.name);
                }
                // en caso de que sea una de las carta del enemigo la asgnamos a la carta 2
                else if (Selected_card1 != null && (Cartas_mano.IndexOf(objeto_carta) == 5 || Cartas_mano.IndexOf(objeto_carta) == 6)){
            
                    Selected_card2= objeto_carta;
                    Debug.Log("Selected card for attack: " + Selected_card2.name);
                    //hacer el ataque de las cartas
                    Attack(Selected_card1, Selected_card2);
                    // cambiamos la opcion Can Attack para que ya no se pueda atacar con esa carta
                    Selected_card1.GetComponent<Atributos>().canAttack=false;
                    Selected_card1 = null;
                    Selected_card2 = null;
                    Attack_Option = false;
                    //En caso de que ya haya usado los dos ataques ya no permite atacar
                    counter++;
                    if (counter == 2){
                        counter = 0;
                        Attack_Option=false;
                        Debug.Log("No more attacks available");
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
        5. Call enemyTurn

        */
    }

    //Increase Energy amount
    public void IncreaseEnegry(){
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
            Atributos atributosCarta1 = objeto_carta1.GetComponent<Atributos>();
            Atributos atributosCarta2 = objeto_carta2.GetComponent<Atributos>();
            
            if (atributosCarta1.AbilityCost>energy)
            {
                // We check if the cards are in the right position 
                if ((Cartas_mano.IndexOf(objeto_carta1) == 3 || Cartas_mano.IndexOf(objeto_carta1) == 4) &&
                    (Cartas_mano.IndexOf(objeto_carta2) == 5 || Cartas_mano.IndexOf(objeto_carta2) == 6))
                {
                   // We attack the enemy card 
                    atributosCarta2.HP -= atributosCarta1.Attack;
                    Debug.Log(objeto_carta1.name + " attacked " + objeto_carta2.name + " for " + atributosCarta1.Attack + " damage.");
                    
                    //Decrease energy amount-- fix later on with the corresponding value
                    energy-= atributosCarta1.AbilityCost;
                    energyText.text=$"{energy}";
                    Slider sliderComponent = energySlider.GetComponent<Slider>();
                    sliderComponent.value = energy;
                }
                else
                {
                    Debug.Log("No enough energy");
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

public void PU_button()
    {
        GameObject PU = Instantiate(PuButtonPrefab, PUParentPrefab.transform.position, Quaternion.identity, PUParentPrefab);
        Button button = PU.GetComponent<Button>();
        //We add that we the button is clicked we create a power up
        button.onClick.AddListener(() => PU_Base(PUs));
        
    }


public void EndTurn()
{
    PlayerTurn = false;
    ++num_turn;
    Atributos activeCard1 = Cartas_mano[3].GetComponent<Atributos>();
    Atributos activeCard2 = Cartas_mano[4].GetComponent<Atributos>();

    activeCard1.CanAttack = true;
    activeCard2.CanAttack = true;

    IncreaseEnegry();
    turnText.text = $"Turn: {num_turn}";
    energyText.text = $"{energy}";
    
    
}

}