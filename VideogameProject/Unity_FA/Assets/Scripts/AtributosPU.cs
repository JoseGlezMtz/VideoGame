using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtributosPU : MonoBehaviour
{
    public int pu_id;
    public string name;
    public string description;
    public int ability_amount;    
    public string ability_effect;
    public GameObject pu_cardAffected;

    //
    public Atributos atributosCardAffected;

    

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
