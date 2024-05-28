using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public Atributos atributos;
    public CardManager cardManager;

    [SerializeField] public GameObject selfCard;
    
    // Start is called before the first frame update
    public void Init(Atributos _atributos)
    {
        atributos = _atributos;
        Image imageComponent = GetComponent<Image>();
   
            if (imageComponent == null)
            {
                //Debug.LogError("Image component not found on newCard.");
            }
            else
            {
                // Image component found, proceed to set sprite
                imageComponent.sprite = Resources.Load<Sprite>($"CardImages/{atributos.id -1}");
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public bool check_alive(){
        if(atributos.health <= 0){
            
            atributos.Alive=false;
            
            return false;
        }
        else{
            return true;
        }
    }
}
