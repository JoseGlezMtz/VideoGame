using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atributos : MonoBehaviour
{
    [SerializeField] private int hp; 
    [SerializeField] private int attack; 
    [SerializeField] private int curation;

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
