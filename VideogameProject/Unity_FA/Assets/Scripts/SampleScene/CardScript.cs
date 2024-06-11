using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardScript : MonoBehaviour
{
    public Atributos atributos;
    public int Start_Attack;
    public int Start_cost;
    public CardManager cardManager;

    [SerializeField] public GameObject selfCard;
    [SerializeField] TMP_Text health_text;
    [SerializeField] TMP_Text energy_text;
    [SerializeField] TMP_Text amount_text;
    
    // Start is called before the first frame update
    public void Init(Atributos _atributos, int Index)
    {
        atributos.SetAtributos(_atributos);
        Start_Attack = atributos.attack;
        Start_cost=atributos.abilityCost;
        Image imageComponent = GetComponent<Image>();
        
                //Debug.LogError("Image component not found on newCard.");
            
            if (Index>4){
                imageComponent.sprite = Resources.Load<Sprite>($"EnemyImages/{atributos.id -1}");  
            }
            
            
            else
            {
                // Image component found, proceed to set sprite
                imageComponent.sprite = Resources.Load<Sprite>($"CardImages/{atributos.id -1}");
            }
            health_text.text=atributos.health.ToString();
            health_text.fontSize = 10; 
            health_text.color = Color.black;

            energy_text.text=atributos.abilityCost.ToString();
            energy_text.fontSize = 10; 
            energy_text.color = Color.white;

            amount_text.text=atributos.attack.ToString();
            amount_text.fontSize = 10; 
            amount_text.color = Color.white;
        
    }

    public bool check_alive(){
        UpdateHealth();
        if(atributos.health <= 0){
            
            atributos.Alive=false;
            Image img = GetComponent<Image>();
            img.color = new Color32(166, 166, 166, 255); // 255 is the alpha value for full opacity

            
            return false;
        }
        else{
            return true;
        }
    }
    
    public void UpdateHealth(){
        if(atributos.health<=0){
            health_text.text="0";
        }
        else{
        health_text.text=atributos.health.ToString();
    }}

    public void UpdateEnergy(){
        energy_text.text = atributos.abilityCost.ToString();
    }

    public void UpdateAmount(){
        amount_text.text = atributos.attack.ToString();
    }

    public void UpdateCard(){
        UpdateHealth();
        UpdateEnergy();
        UpdateAmount();
    }

    public void Reset_card(){
        atributos.attack = Start_Attack;
        atributos.alredyboosted = false;
        atributos.abilityCost = Start_cost;
        UpdateCard();
    }

    public void Size_increase(){
        GetComponent<RectTransform>().sizeDelta = new Vector2(96, 144);
    }
    public void Size_decrease(){
        GetComponent<RectTransform>().sizeDelta = new Vector2(80, 120);
        
    }
    
}
