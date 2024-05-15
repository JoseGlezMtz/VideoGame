using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atributos : MonoBehaviour
{
    [SerializeField] private int hp; 
    [SerializeField] private int attack; 
    [SerializeField] private int abilityCost;
    [SerializeField] private int curation;
    [SerializeField] private bool canAttack;

    public int HP 
    { 
        get { return hp; } 
        set { hp = value; } 
    }

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

    public int Curation 
    { 
        get { return curation; } 
        set { curation = value; } 
    }

    public void Heal(int amount)
    {
        HP += amount;
    }
}
