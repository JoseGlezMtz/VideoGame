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

    
    // Start is called before the first frame update
    public void Init(Atributos _atributos)
    {
        atributos = _atributos;
        Start_Attack = atributos.attack;
        Start_cost=atributos.abilityCost;

        Image imageComponent = GetComponent<Image>();
   
            if (imageComponent == null)
            {
                //Debug.LogError("Image component not found on newCard.");
            }
            else
            {
                // Image component found, proceed to set sprite
                imageComponent.sprite = Resources.Load<Sprite>($"CardImages/{atributos.id -1}");
                
                health_text.text=atributos.health.ToString();
                health_text.fontSize = 10; 
                health_text.color = Color.black;
            }
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void Reset_card(){
        atributos.attack = Start_Attack;
        atributos.alredyboosted = false;
        atributos.abilityCost = Start_cost;
    }
    
}
