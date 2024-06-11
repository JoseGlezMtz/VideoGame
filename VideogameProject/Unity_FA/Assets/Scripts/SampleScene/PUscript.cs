using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PUscript : MonoBehaviour
{
    public AtributosPU atributosPU;
    
    // Start is called before the first frame update
    public void Init(AtributosPU pu)
    {   
        atributosPU = pu;
        Image imageComponent = GetComponent<Image>();
        if (imageComponent == null)
            {
                //Debug.LogError("Image component not found on newCard.");
            }
            else
            {
                // Image component found, proceed to set sprite
                imageComponent.sprite = Resources.Load<Sprite>($"Powerups/{atributosPU.id}");
                
            }
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
