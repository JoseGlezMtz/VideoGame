/*
Deck Manager 
Valentina Gonzalez
07/05/2024

Funcionalidades
- obtener la lista de cartas como un serializefield 
- inicializar las cartas en el panel de Cards
- Agregar funcionalidad de OnClick para las cartas dentro del panel Cards para que se agreguen al panel de MainCard (utilizar componente image?)
-Agregar funcionalidad de initialize cards??

*/

using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using System;

public class DeckManager : MonoBehaviour
{
    [SerializeField]List<OptionsCards> cards;
    int numCards = 7;
    int numCardsDeck;
    
    [SerializeField] GameObject displayedCard;

    [SerializeField] Transform cardsParent;
    [SerializeField] GameObject cardsPrefab;

    // Start is called before the first frame update
    void Start()
    {
        initializeCards();
    }

void initializeCards(){
    for(int i = 0; i < numCards; i++){
        int index = i;
        GameObject newCard = Instantiate(cardsPrefab, cardsParent);
        newCard.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>($"CardImages/{index+1}");
    }
}


    // Update is called once per frame
    void Update()
    {
        
    }

    
}
