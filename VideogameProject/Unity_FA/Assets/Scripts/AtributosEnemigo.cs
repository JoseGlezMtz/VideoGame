using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AtributosEnemigo 
{
    public int id = 0; 
    public string enemy_name;
    public int health = 100; 
    public int attack = 25; 
    public int abilityCost = 10;
    public string abilities_affectted;
    public string effect;
    public int resistance;
    public bool canAttack = true;
    public int isShielded = 0;
    public int speed = 0;
    public bool Alive = true;

    public void Update_Shield()
    {
        if(isShielded > 0)
        {
            isShielded -= 1;
        }
    }
}

[System.Serializable]
public class Enemies
{
    public List<AtributosEnemigo> enemies;
}

