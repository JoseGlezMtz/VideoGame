using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
     public CardManager cardManager;
        void Start()
    {
        if (cardManager == null)
        {
            cardManager = FindObjectOfType<CardManager>();
            if (cardManager == null)
            {
                Debug.LogError("CardManager not found in the scene.");
            }
        }
    }

    
    public void EnemyAttack(GameObject enemyCard, GameObject playerCard)
{
    // First we check if the enemy card and the player card are not null
    if (enemyCard != null && playerCard != null)
    {
        // We set the attributes of the cards enemyCardAtributos for the enemy card and playerCardAtributos for the player card
        Atributos enemyCardAtributos = enemyCard.GetComponent<CardScript>().atributos;
        Atributos playerCardAtributos = playerCard.GetComponent<CardScript>().atributos;
        // We check if the player card is not shielded
        if (playerCardAtributos.isShielded==0)
        {
            int damageDealt = enemyCardAtributos.attack - playerCardAtributos.resistance;
          if (damageDealt <= 0)
            {
                Debug.Log("No damage dealt because the player card has more resistance than the enemy card's attack");
            }else{
            playerCardAtributos.health -= damageDealt;
            Debug.Log(enemyCard.name + " attacked " + playerCard.name + " for " + enemyCardAtributos.attack + " damage.");
            //revisar si la carta que atacaron murió
            if (!playerCard.GetComponent<CardScript>().check_alive()){

                if (cardManager.Cartas_mano[0].GetComponent<CardScript>().atributos.Alive){
                cardManager.Change_Cards(playerCard, cardManager.Cartas_mano[0]);}

                else if (cardManager.Cartas_mano[1].GetComponent<CardScript>().atributos.Alive){
                cardManager.Change_Cards(playerCard, cardManager.Cartas_mano[1]);}

                else if (cardManager.Cartas_mano[2].GetComponent<CardScript>().atributos.Alive){
                cardManager.Change_Cards(playerCard, cardManager.Cartas_mano[2]);}
                
                //CONDICIÓN DE PERDER
                else{
                    Debug.Log("No more cards to change");
                    if(cardManager.Cartas_mano[3].GetComponent<CardScript>().atributos.health <= 0 && cardManager.Cartas_mano[4].GetComponent<CardScript>().atributos.health <= 0)
                    {
                        Debug.Log("You lose");
                        cardManager.PlayerTurn = false;
                    }
                    
                }
            }

            }
        }
        else
        {
            // If the player card is shielded we print a message
            Debug.Log(playerCard.name + " is shielded and cannot be attacked this turn.");
        }
    }
    else
    {
        Debug.Log("Error en el ataque del enemigo");
    }
}


public void EnemyTurn()
{
    // We check if it's the enemy turn
    if (cardManager.CountCountEnemyTurn)
    {
        // We select a random card from the enemy hand
        int playerCardIndex = Random.Range(3, 5); 
        GameObject playerCard = cardManager.Cartas_mano[playerCardIndex];
        Debug.Log("Player card selected: " + playerCard.name); 
        
        // We select a random card from the player hand
        int enemyCardIndex = Random.Range(5, 7); 
        GameObject enemyCard = cardManager.Cartas_mano[enemyCardIndex];
        Debug.Log("Enemy card selected: " + enemyCard.name); 
        //llamamos a la funcion de ataque
        EnemyAttack(enemyCard, playerCard);
        // We end the enemy turn
        cardManager.CountCountEnemyTurn = false;
        // We set the player turn to true
        cardManager.PlayerTurn = true;

    }
    }

    
    



}