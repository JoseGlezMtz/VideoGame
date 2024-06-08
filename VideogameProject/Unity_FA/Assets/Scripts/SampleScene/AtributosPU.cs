using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class AtributosPU 
{
    public int id;
    public string name;
    public string description;
    public string ability_effect;
    public int ability_amount;    
    public GameObject pu_cardAffected;
    

    //

    

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

[System.Serializable]
public class PUs{
    public List<AtributosPU> powerUps;
}
