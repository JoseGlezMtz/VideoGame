using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atributos : MonoBehaviour
{
    [SerializeField] private int hp; 
    [SerializeField] private int attack; 

    // Propiedades para los atributos de la carta
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
}
