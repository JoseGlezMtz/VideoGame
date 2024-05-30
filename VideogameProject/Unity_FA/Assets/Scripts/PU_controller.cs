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

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<APIconection>().get_PU_cards();
        //puCards =JsonUtility.FromJson<PUs>(pu_Cards_Data);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        //Agregar for loop para crear los 30 power ups
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
        
        Debug.Log("Creating: " + PU.name);
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
    }

    if (pu_Pile.Count == 0)
    {
        Init_PU_ID();
    }
}

 public void SelectPowerUp(GameObject powerUpObject)
{
    cardManager.Selectpowerup = powerUpObject;
    Debug.Log("Selected PowerUp: " + cardManager.Selectpowerup.name);
}


public void UsePowerUp(GameObject cardObject, GameObject powerUpObject)
{
    CardScript cardScript = cardObject.GetComponent<CardScript>();
    PUscript powerUpScript = powerUpObject.GetComponent<PUscript>();
    string PU_ability_effect=powerUpScript.atributosPU.ability_effect;
    int PU_ability_amount=powerUpScript.atributosPU.ability_amount;
    
    Debug.Log("Using power up: " + powerUpObject.name);
    // We check if the power up is null
    if (powerUpObject == null)
    {
        Debug.Log("No power-up selected");
        return;
    }
    // Accedemos al script de la carta y del power-up y lo guardamos en variables
    
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
    
    Debug.Log("Power-up effect: " + PU_ability_effect);
    // Revisamos el efecto del power-up y aplicamos el efecto correspondiente accediendo a los atributos de los power-ups para ver el efecto
    switch (PU_ability_effect)
    {
        // En caso de que el efecto sea curación
        case "curacion":
            Debug.Log("Applying healing power-up");
            int healthBoost = PU_ability_amount;
            cardScript.atributos.health += healthBoost;
            Debug.Log("Health boosted by: " + healthBoost);
            break;
        // En caso de que el efecto sea restar de energía
        case  "restaura_energia":
            Debug.Log("Applying energy restore power-up");
            int energyBoost = PU_ability_amount;
            cardScript.atributos.abilityCost -= energyBoost;
            Debug.Log("Energy boosted by: " + energyBoost);
            break;
        
        // En caso de que el efecto sea aumentar el daño
        case "mejora_dano":
            Debug.Log("Applying damage boost power-up");
            int damageBoost = PU_ability_amount;
            cardScript.atributos.attack += damageBoost;
            Debug.Log("Damage boosted by: " + damageBoost);
            break;
        // En caso de que el efecto sea aumentar la resistencia
        case "mejora_resistencia":
            Debug.Log("Applying resistance boost power-up");
            int resistanceBoost = PU_ability_amount;
            cardScript.atributos.resistance += resistanceBoost;
            Debug.Log("Resistance boosted by: " + resistanceBoost);
            break;

        // En caso de que el efecto aplicar un escudo
        case "escudo":
            Debug.Log("Applying shield power-up");
            cardScript.atributos.isShielded = PU_ability_amount;
            Debug.Log("Shield applied");
            break;
        
         case "bloquea_dano":
            Debug.Log("Applying damage block power-up");
            break;
        
        case "mejo_velocidad":
            Debug.Log("Applying speed boost power-up");
            int speedBoost = PU_ability_amount;
            cardScript.atributos.speed += speedBoost;
            Debug.Log("Speed boosted by: " + speedBoost);
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
    cardManager.Selectpowerup = null;
    cardManager.Selected_card1 = null;
    Debug.Log("Power-up applied successfully");
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
}
