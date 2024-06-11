using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class PU_controller : MonoBehaviour
{
    public CardManager cardManager;
    [SerializeField] List<GameObject> pu_Hand;
    [SerializeField] GameObject PuButtonPrefab;
    [SerializeField] GameObject PUPrefab;
    [SerializeField] Transform PUParentPrefab;
     [SerializeField] Transform PUSlot;
     [SerializeField] List<int> pu_Pile;
    public bool puAdded;
    [SerializeField] public bool PowerUp_created = false;
    [SerializeField] public bool pu_saved = false;
    int maxPuPile = 28;
    public string pu_Cards_Data;
    [SerializeField]  PUs puCards;

    public void InitializePUCounters(){
        for(int i = 8; i <= 36; i++){
            PlayerPrefs.SetInt($"pu{i}Counter", 0);
        }
    }

    public void UpdatePUCounter(int id){
        PlayerPrefs.SetInt($"pu{id}Counter", +1);
        Debug.Log($"Updating power up with id: {id}");
    }

    public void Init_PU_ID(){
        HashSet<int> usedIds = new HashSet<int>();
      while(pu_Pile.Count < maxPuPile){
          int id = RandomId();
          if (!usedIds.Contains(id)){
              pu_Pile.Add(id);
              usedIds.Add(id);
          }
      }

    }
    
    public void PU_button()
    {
        GameObject PU = Instantiate(PuButtonPrefab, PUParentPrefab.transform.position, Quaternion.identity, PUParentPrefab);
        Button button = PU.GetComponent<Button>();
        //We add that we the button is clicked we create a power up
        button.onClick.AddListener(() => Create_PU());
        puCards =JsonUtility.FromJson<PUs>(pu_Cards_Data);
        
    }
    
    public void Create_PU()
    {
        if (PowerUp_created)
        {
            Debug.Log("Power up already created");
            
            return;
        }
        else if (!cardManager.PlayerTurn)
        {
            Debug.Log("It's not your turn");
        }
        else{
        //Agregar for loop para crear los 27 power ups
        //Agregar los objects a la lista de pile
        
        GameObject PU = Instantiate(PUPrefab, PUParentPrefab.transform.position, Quaternion.identity, PUParentPrefab);
        Debug.Log(pu_Pile[0]);
        foreach (AtributosPU pu in puCards.powerUps)
        {
            
            if (pu.id == pu_Pile[0])
            {
                PU.GetComponent<PUscript>().Init(pu);
            }
        }
        
        Debug.Log("Power UP " + PU.GetComponent<PUscript>().atributosPU.name + " (Press button green to save power up)");
        pu_Pile.RemoveAt(0);
        PowerUp_created = true;
        }

    }

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
            Deletingrident();

        }

        if (pu_Pile.Count == 0)
        {
            Init_PU_ID();
        }
    }

    public void SelectPowerUp(GameObject powerUpObject)
    {
        cardManager.Selectpowerup = powerUpObject;
        Debug.Log("Power up description: " + powerUpObject.GetComponent<PUscript>().atributosPU.description 
        + ". By (" + powerUpObject.GetComponent<PUscript>().atributosPU.ability_amount + ") points/turns");
    }

    public void UsePowerUp(GameObject cardObject, GameObject powerUpObject)
    {
        CardScript cardScript = cardObject.GetComponent<CardScript>();
        PUscript powerUpScript = powerUpObject.GetComponent<PUscript>();
        string PU_ability_effect=powerUpScript.atributosPU.ability_effect;
        int PU_ability_amount=powerUpScript.atributosPU.ability_amount;
        int PU_ability_id=powerUpScript.atributosPU.id;
    
    
        // Accedemos al script de la carta y del power-up y lo guardamos en variables
    
        // Revisamos si los scripts son nulos esto se uso para debuggear
    
      if (cardScript.check_alive() == false && PU_ability_id != 36)
        {
            Debug.Log("Card is dead, can't use power-up");
            cardManager.Selectpowerup = null;
            cardManager.Selected_card1 = null;
            return;
            
        }
        if(PU_ability_id < 11){
            Debug.Log("Can't use this power up you need Cookie, Chocolate and marshmellow to create smore and use the power up");
            
            cardManager.Selectpowerup = null;
            cardManager.Selected_card1 = null;
            return;
        }
        if (PU_ability_effect == "bloquea_dano" && cardManager.Cartas_mano.IndexOf(cardObject) < 5)
        {
            Debug.Log("Can't use this power up on your own cards");
            cardManager.Selectpowerup = null;
            cardManager.Selected_card1 = null;
            return;
        }else if (PU_ability_effect != "bloquea_dano" && cardManager.Cartas_mano.IndexOf(cardObject) >= 5)
        {

            Debug.Log("Can't use this power up on opponent's cards");
            cardManager.Selectpowerup = null;
            cardManager.Selected_card1 = null;
            return;
        }
        //INSERTAR MÉTODO PARA CONTAR SI EL POWER UP ES USADO
        // powerUpObject.GetComponent<PUscript>().atributosPU.description

        UpdatePUCounter(PU_ability_id);


        // Revisamos el efecto del power-up y aplicamos el efecto correspondiente accediendo a los atributos de los power-ups para ver el efecto
        switch (PU_ability_effect)
        {
            // En caso de que el efecto sea curación
                case "curacion":
        
                    Debug.Log("Applying healing power-up");
                    //VALENTINA: ADD ANIMATION FOR HEALING
                    int healthBoost = PU_ability_amount;
                    if (cardScript.atributos.health + healthBoost > 100)
                    {
                        cardScript.atributos.health = 100;
                    }
                    else
                    {
                        cardScript.atributos.health += healthBoost;
                        Debug.Log("Health boosted of card: " + cardScript.atributos.character_name + " by " + healthBoost);
                        cardScript.UpdateHealth();
                    }
                break;

                // En caso de que el efecto sea restar de energía
                case  "restaura_energia":
                    Debug.Log("Applying energy restore power-up");
                    int energyBoost = PU_ability_amount;
                    cardScript.atributos.abilityCost -= energyBoost;
                    Debug.Log("Energy cost reduced of card: " + cardScript.atributos.character_name + " by " + energyBoost);
                    cardScript.UpdateEnergy();
                break;
        
                // En caso de que el efecto sea aumentar el daño
                case "mejora_dano":
                    Debug.Log("Applying damage boost power-up");
                    int damageBoost = PU_ability_amount ;
                    cardScript.atributos.attack += damageBoost ;
                    cardScript.UpdateAmount();
                    Debug.Log("Damage boosted of card: " + cardScript.atributos.character_name + " by " + PU_ability_amount); 
                break;
            // En caso de que el efecto sea aumentar la resistencia
            case "mejora_resistencia":
                    Debug.Log("Applying resistance boost power-up");
                    int resistanceBoost = PU_ability_amount;
                    cardScript.atributos.resistance += resistanceBoost;
                    Debug.Log("Resistance boosted of card: " + cardScript.atributos.character_name + " by " + resistanceBoost);
                break;

            // En caso de que el efecto aplicar un escudo
            case "escudo":
                    Debug.Log("Applying shield power-up");
                    cardScript.atributos.isShielded = PU_ability_amount;
                    Debug.Log("Shield applied to card: " + cardScript.atributos.character_name + " for " + PU_ability_amount + " turns");
                break;
        
            case "bloquea_dano":
                    Debug.Log("Applying cannot attack power-up");
                    cardScript.atributos.cannotAttack = PU_ability_amount +1;
                    Debug.Log("Cannot attack applied to card: " + cardScript.atributos.character_name + " for " + PU_ability_amount + " turns");
                break;
        
            case "revive":
                    if (cardScript.atributos.Alive){
                        Debug.Log("Card is already alive, can't use revive power-up");
                        return;
                    } else{
                        Debug.Log("Applying revive power-up");
                        cardObject.GetComponent<Image>().color=Color.white;
                        cardScript.atributos.health = 70;
                        cardScript.atributos.Alive = true;
                        Debug.Log("Card: " + cardScript.atributos.character_name + " has been revived");
                    }
                break;

            
                // En caso de que el efecto sea desconocido se imprime un mensaje de error esto tambien se uso para debuggear
                default:
                    Debug.Log("This power-up has no effect yet");
                break;
        }
            // Se destruye el power-up y se remueve de la lista de power-ups en la mano

            Destroy(powerUpObject);
            pu_Hand.Remove(powerUpObject);
            cardObject.GetComponent<CardScript>().UpdateHealth();
            // Se setea el power-up seleccionado a null al igual que la carta seleccionada
            cardManager.Selectpowerup = null;
            cardManager.Selected_card1 = null;
    }

    public void DiscardPU(){
            if (PowerUp_created && !pu_saved){
                GameObject PowerUp = PUParentPrefab.transform.GetChild(1).gameObject;
                Debug.Log(PowerUp);
                Destroy(PowerUp);
                pu_Hand.Remove(PowerUp);

            }
            else{
                Debug.LogError("No power up to discard");
            }
        
        }
    
    public int RandomId(){
        return Random.Range(8, 36);
    }

    // Nuevo método para verificar y reemplazar los power-ups
    public void Deletingrident()
    {
        List<int> requiredIds = new List<int> { 8, 9, 10 };
        List<GameObject> powerUpsToRemove = new List<GameObject>();

        foreach (GameObject powerUp in pu_Hand)
        {
            int powerUpId = powerUp.GetComponent<PUscript>().atributosPU.id;
            if (requiredIds.Contains(powerUpId))
            {
                powerUpsToRemove.Add(powerUp);
            }
        }

        if (powerUpsToRemove.Count == requiredIds.Count)
        {
            foreach (GameObject powerUp in powerUpsToRemove)
            {
                pu_Hand.Remove(powerUp);
                Destroy(powerUp);
           }

           Addsmore(36);
           Debug.Log("All ingridients found, power-up smore created");
       }


    
    }

    // Método auxiliar para agregar un power-up específico por ID
    private void Addsmore(int id)
    {
        foreach (AtributosPU pu in puCards.powerUps)
        {
            if (pu.id == id)
            {
                GameObject PU = Instantiate(PUPrefab, PUParentPrefab.transform.position, Quaternion.identity, PUParentPrefab);
                PU.GetComponent<PUscript>().Init(pu);
                PU.transform.SetParent(PUSlot);
                pu_Hand.Add(PU);
                // Asignamos el listener para seleccionar el power-up
                Button powerUpButton = PU.GetComponent<Button>();
                powerUpButton.onClick.AddListener(() => SelectPowerUp(PU));
                break;
            }
        }
    }
    
}
