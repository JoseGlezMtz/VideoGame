using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
     public CardManager cardManager;
        void Start()
    {
        
            cardManager = FindObjectOfType<CardManager>();
            
            
        
            
        
    }
     

    private IEnumerator delaymesage( )
    {
        string message = $"Turn: {cardManager.num_turn}"; 
        yield return new WaitForSeconds(4f);
        cardManager.turnText.text = message;
    }

    
    public void EnemyAttack(GameObject enemyCard, GameObject playerCard)
{
    Atributos enemyCardAtributos = enemyCard.GetComponent<CardScript>().atributos;
    Atributos playerCardAtributos = playerCard.GetComponent<CardScript>().atributos;
    // First we check if the enemy card and the player card are not null
    if (enemyCard != null && playerCard != null)
    {
        // We check if the player card is not shielded
        if (playerCardAtributos.isShielded==0)
        {
            int damageDealt = enemyCardAtributos.attack - playerCardAtributos.resistance;
          if (damageDealt <= 0)
            {
                Debug.Log("No damage dealt because the player card has more resistance than the enemy card's attack");
                StartCoroutine(delaymesage());
            }else{
            playerCardAtributos.health -= damageDealt;
            Debug.Log(enemyCardAtributos.character_name + " attacked " + playerCardAtributos.character_name + " dealing " + damageDealt + " damage."); 
            StartCoroutine(delaymesage());
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
            StartCoroutine(delaymesage());
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
       Atributos playerCardAtributos = cardManager.Cartas_mano[3].GetComponent<CardScript>().atributos;
       
       Atributos playerCardAtributos2 = cardManager.Cartas_mano[4].GetComponent<CardScript>().atributos;

       Atributos enemyCardAtributos = cardManager.Cartas_mano[5].GetComponent<CardScript>().atributos;

       Atributos enemyCardAtributos2 = cardManager.Cartas_mano[6].GetComponent<CardScript>().atributos;
       int index ; 
       GameObject playerCard = null; 

        if (playerCardAtributos.health < playerCardAtributos2.health )
        {
            if(playerCardAtributos.health > 0){
            index = 3;
            }
            else{
                index = 4;
            }
        }
        else if (playerCardAtributos.health > playerCardAtributos2.health)
        {
            if(playerCardAtributos2.health > 0){
            index = 4;
            }
            else{
                index = 3;
            }
        }
        else if (playerCardAtributos.resistance < playerCardAtributos2.resistance)
        {
            index = 3;
        }
        else if (playerCardAtributos.resistance > playerCardAtributos2.resistance)
        {
            index = 4;
        }
        else 
        {
            index= Random.Range(3, 5);

        }

        // Assign playerCard after index is determined
        playerCard = cardManager.Cartas_mano[index];
        
        // Log the selected card

        

        int indexenemy=0;
        GameObject enemyCard = null;

        if (enemyCardAtributos.health >0 && enemyCardAtributos2.health > 0){

        indexenemy= Random.Range(5, 7);

        }  
         else if (enemyCardAtributos.health > 0){
            indexenemy = 5;
        }
        else if (enemyCardAtributos2.health > 0){
            indexenemy = 6;
        }
        
        enemyCard = cardManager.Cartas_mano[indexenemy];
        /*
        // We select a random card from the player hand
        int enemyCardIndex = Random.Range(5, 7); 
        GameObject enemyCard = cardManager.Cartas_mano[enemyCardIndex];
        //llamamos a la funcion de ataque*/
        EnemyAttack(enemyCard, playerCard);
        // We end the enemy turn
        cardManager.CountCountEnemyTurn = false;
        // We set the player turn to true
        cardManager.PlayerTurn = true;
        

    }
    }

    
    



}
