using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Atributos 
{
     public int id = 0; 
     public string character_name;
     public int health = 100; 
     public int attack = 20; 
     public int abilityCost = 5;
     public string cards_affectted;
     public string effect;
     public int resistance;
     public bool canAttack=true;
     public int isShielded = 0;
     public int speed = 0;

/*    public int Id 
    { 
        get { return id; } 
        set { id = value; } 
    }

    /*public int HP 
    { 
        get { return health; } 
        set { health = value; } 
    }*/

    public int Attack 
    { 
        get { return attack; } 
        set { attack = value; } 
    }

    public bool CanAttack 
    { 
        get { return canAttack; } 
        set { canAttack = value; } 
    }

    public int AbilityCost
    { 
        get { return abilityCost; } 
        set { attack = abilityCost; } 
    }

    /*public int Curation 
    { 
        get { return curation; } 
        set { curation = value; } 
    }*/

    /*public int Shield 
    { 
        get { return shield; } 
        set { shield = value; } 
    }

    public void Heal(int amount)
    {
        health += amount;
    }*/
    
    

    public void Update_Shield(){
        if(isShielded>0){
            isShielded-=1;
        }
    }

    
}
[System.Serializable]
public class Cards{
    public List<Atributos> cards;
}

public class Carta : MonoBehaviour
{
     public int id = 0; 
     public string character_name;
     public int health = 100; 
     public int attack = 20; 
     public int abilityCost = 5;
     public string cards_affectted;
     public string effect;
     public int resistance;
     public int speed = 0;

    public void SetAtributos(Carta atributos){
        this.id=atributos.id;
        this.health=atributos.health;
        this.attack=atributos.attack;
        this.abilityCost=atributos.abilityCost;
        this.effect=atributos.effect;
        
        this.speed=atributos.speed;

    }
}