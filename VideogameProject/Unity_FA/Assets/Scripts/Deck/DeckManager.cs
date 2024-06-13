using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    //Lists to store the cards
    [SerializeField] List<GameObject> cards;
    [SerializeField] List<GameObject> deck;

    public List<int> deck_ids;

    int numCards = 7;
    int maxDeck = 5;

    //variables to identify which card is selected and what card will be displayed
    [SerializeField] GameObject displayedCard;
    [SerializeField] GameObject selectedCard;

    //Panels to arrange the cards
    [SerializeField] Transform cardsParent;
    [SerializeField] Transform deckParent;

    //Prefab to initialize cards
    [SerializeField] GameObject cardsPrefab;

    //Add and remove buttons
    [SerializeField] GameObject AddBtn;
    [SerializeField] GameObject RemoveBtn;
    [SerializeField] GameObject FavoriteBtn;

    [SerializeField] GameObject SaveBtn;

    public APIconection apiConnection;

    //Start the scene, intialize the empty lists and cards
    void Start()
    {
        cards = new List<GameObject>();
        deck = new List<GameObject>(); 
        InitializeCards();

        Button saveButton = SaveBtn.GetComponent<Button>();
        saveButton.onClick.AddListener(() => SaveDeck());
        apiConnection = GetComponent<APIconection>();

    }

    //Generate the cards 
    void InitializeCards()
    {
        for (int i = 0; i < numCards; i++)
        {
            int index = i;
            
            //Instantiate the prefab and assign the corresponding image
            GameObject newCard = Instantiate(cardsPrefab, cardsParent);
            newCard.name = $"card{index}";
            Image imageComponent = newCard.GetComponent<Image>();
            if (imageComponent == null)
            {
                Debug.LogError("Image component not found on newCard.");
            }
            else
            {
                // Image component found, proceed to set sprite
                imageComponent.sprite = Resources.Load<Sprite>($"Deck/{index}");
            }

            OptionsCards optionsCardsComponent = newCard.GetComponent<OptionsCards>();
            if (optionsCardsComponent == null)
            {
                Debug.LogError("OptionsCards component not found on newCard.");
            }
            else
            {
                // OptionsCards component found, initialize it
                //Assing the index to the card
                optionsCardsComponent.cardIndex = index;
                cards.Add(newCard);
            }

            Button buttonComponent = newCard.GetComponent<Button>();
            if (buttonComponent == null)
            {
                //Debug.LogError("Button component not found on newCard.");
            }
            else
            {
                // Button component found add onClick listener
                buttonComponent.onClick.AddListener(() => DisplayCard(newCard));
            }

            //Button favoriteButton = newCard.GetComponentInChildren<Button>();
            //favoriteButton.onClick.AddListener(() => AddFavorite(newCard));
            //cardButton.onClick.AddListener(() => DisplayCard(cardToAdd));


        }
    }

    //Method to visualize the selected card on the big card
    public void DisplayCard(GameObject card)
    {
        OptionsCards cardCardsComponent = card.GetComponent<OptionsCards>();
        int index = cardCardsComponent.cardIndex;
        //Debug.Log($"Card Index: {index}");

        //define the buttons
        Button addButton = AddBtn.GetComponent<Button>();
        Button removeButton = RemoveBtn.GetComponent<Button>();
        Button favoriteButton = FavoriteBtn.GetComponent<Button>();

        //assign the clicked card to the variable: selectedCard
        OptionsCards cardComponent = card.GetComponent<OptionsCards>();
        selectedCard = card;

        //ad the corresponding image
        Image displayedCardImage = displayedCard.GetComponent<Image>();
        displayedCardImage.sprite = Resources.Load<Sprite>($"Deck/{index}");

        //Detect clicks on the add or remove buttons and call methods
        addButton.onClick.AddListener(() => AddDeck(selectedCard));
        removeButton.onClick.AddListener(() => RemoveDeck(selectedCard));
        favoriteButton.onClick.AddListener(() =>AddFavorite(selectedCard));
        
    }

    //Method to move a card from the options available to the deck
    void AddDeck(GameObject cardToAdd)
    {
        OptionsCards optionsCardsComponent = cardToAdd.GetComponent<OptionsCards>();
        //Debug.Log($"Adding card with index: {optionsCardsComponent.cardIndex}");

        //Check if the size of the deck is less than the maximum allowed
        if (deck.Count < maxDeck)
        {
            //Check if the card to add is not already in the deck
            if (!deck.Contains(cardToAdd))
            {
                //If both conditions are met, add the card to the deck
                deck.Add(cardToAdd);
                cardToAdd.transform.SetParent(deckParent);
                cards.Remove(cardToAdd);

                Button cardButton = cardToAdd.GetComponent<Button>();
                if (cardButton == null)
                    {
                        //Debug.LogError("Button component not found on newCard.");
                    }
                    else
                    {
                        //Debug.Log("Button found, adding functionality...");
                        // Button component found, proceed to add onClick listener
                        cardButton.onClick.AddListener(() => DisplayCard(cardToAdd));

                    }

                Debug.Log("Card Added Successfully");
            }
            else
            {
                Debug.LogError("Card Already in Deck");
            }
        }
        else
        {
            Debug.LogError("Deck is Full");
        }
        UpdateIcons();
    }

    void AddFavorite(GameObject cardToAdd){
        deck.Remove(cardToAdd);
        deck.Add(cardToAdd);

        UpdateIcons();
    }

     void UpdateIcons()
    {
        Debug.Log("Updating Icons...");
        for (int i = 0; i < deck.Count; i++)
        {
            OptionsCards cardComponent = deck[i].GetComponent<OptionsCards>();
            if (i >= maxDeck - 2)
            {
                cardComponent.favSprite.gameObject.SetActive(true);
            }
            else
            {
                cardComponent.favSprite.gameObject.SetActive(false);
            }
        }
        
        // Ensure all cards not in deck have their favSprite deactivated
        foreach (GameObject card in cards)
        {
            OptionsCards cardComponent = card.GetComponent<OptionsCards>();
            cardComponent.favSprite.gameObject.SetActive(false);
        }
    }

    //Method to remove card
    void RemoveDeck(GameObject cardToRemove){
        
        //Check if the card is in the deck, if so, remove
        if(deck.Contains(cardToRemove)){
            cardToRemove.GetComponentInChildren<TMP_Text>().text = "";
            cards.Add(cardToRemove);
            cardToRemove.transform.SetParent(cardsParent);
            deck.Remove(cardToRemove);
            UpdateIcons();
        }

    }

    public void updateDeckDB(){
        Debug.Log("PLAYER PREFS CARDS: ");
        int p_id = PlayerPrefs.GetInt("id");
        int c1 = PlayerPrefs.GetInt("card1");
        int c2 = PlayerPrefs.GetInt("card2");
        int c3 = PlayerPrefs.GetInt("card3");
        int c4 = PlayerPrefs.GetInt("card4");
        int c5 = PlayerPrefs.GetInt("card5");
    
        Debug.Log($"Adding card: {c1} ");
        Debug.Log($"Adding card: {c2} ");
        Debug.Log($"Adding card: {c3} ");
        Debug.Log($"Adding card: {c4} ");
        Debug.Log($"Adding card: {c5} ");
        
        apiConnection.SaveDeck(p_id, c1, c2, c3, c4, c5);
    }

    void SaveDeck(){
        deck_ids.Clear();
        if(deck.Count != 5){
            Debug.Log("Deck does not meet size requirements");
        }
        else{
            for(int i = 0; i <= maxDeck; i++){
                OptionsCards cardAtributos = deck[i].GetComponent<OptionsCards>();
                Debug.Log(cardAtributos.cardIndex + 1);
                deck_ids.Add(cardAtributos.cardIndex + 1);

                PlayerPrefs.SetInt($"card{i+1}",cardAtributos.cardIndex + 1);
                updateDeckDB();
                UnityEngine.SceneManagement.SceneManager.LoadScene("Game_Scene");
            }
            
        }
        UpdateIcons();
    }
}