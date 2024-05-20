using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtributosPU : MonoBehaviour
{
    public int pu_id;
    public string pu_name;
    public int pu_amount;    
    public string pu_effect;
    public GameObject pu_cardAffected;

    //
    public Atributos atributosCardAffected;

    public AtributosPU(int Id, string Name, int Amount, string Effect){

        pu_id = Id;
        pu_name = Name;
        pu_amount = Id;
        pu_effect = Effect;
        pu_amount = Amount;
        
    }    

    /*
    Ability Options:
    - HP
    - Increase Damage
    - Damage
    - Shield
    - Restore Energy
    - Increase speed

        public void HealEffect(){
        atributosCardAffected.HP += pu_amount;
    }

    

    */

}
