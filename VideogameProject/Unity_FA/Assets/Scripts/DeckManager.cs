using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    [SerializeField] List<GameObject> cards;
    [SerializeField] List<GameObject> deck;

    int numCards = 7;
    int maxDeck = 5;

    [SerializeField] GameObject displayedCard;
    [SerializeField] GameObject selectedCard;
    [SerializeField] Transform cardsParent;
    [SerializeField] Transform deckParent;
    [SerializeField] GameObject cardsPrefab;
    [SerializeField] GameObject AddBtn;

    void Start()
    {
        cards = new List<GameObject>();
        deck = new List<GameObject>(); 
        InitializeCards();
    }

    void InitializeCards()
    {
        for (int i = 0; i < numCards; i++)
        {
            int index = i;

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
                imageComponent.sprite = Resources.Load<Sprite>($"CardImages/{index}");
            }

            OptionsCards optionsCardsComponent = newCard.GetComponent<OptionsCards>();
            if (optionsCardsComponent == null)
            {
                Debug.LogError("OptionsCards component not found on newCard.");
            }
            else
            {
                // OptionsCards component found, proceed to initialize it
                optionsCardsComponent.cardIndex = index;
                cards.Add(newCard);
            }

            Button buttonComponent = newCard.GetComponent<Button>();
            if (buttonComponent == null)
            {
                Debug.LogError("Button component not found on newCard.");
            }
            else
            {
                // Button component found, proceed to add onClick listener
                buttonComponent.onClick.AddListener(() => DisplayCard(index));
            }
        }
    }

    public void DisplayCard(int index)
    {
        Button addButton = AddBtn.GetComponent<Button>();

        for (int i = 0; i < cards.Count; i++)
        {
            OptionsCards optionsCardsComponent = cards[i].GetComponent<OptionsCards>();
            if (index == optionsCardsComponent.cardIndex)
            {
                selectedCard = cards[i];
                OptionsCards selectedCardComponent = selectedCard.GetComponent<OptionsCards>();
                Image displayedCardImage = displayedCard.GetComponent<Image>();
                displayedCardImage.sprite = Resources.Load<Sprite>($"CardImages/{index}");
                addButton.onClick.AddListener(() => AddDeck(selectedCard)); // Pass the selected card object
            }
        }
        Debug.Log($"Card Index: {index}");
    }

    void AddDeck(GameObject cardToAdd)
    {
        OptionsCards optionsCardsComponent = cardToAdd.GetComponent<OptionsCards>();
        Debug.Log($"Adding card with index: {optionsCardsComponent.cardIndex}");
        if (deck.Count < maxDeck)
        {
            if (!deck.Contains(cardToAdd))
            {
                deck.Add(cardToAdd);
                cardToAdd.transform.SetParent(deckParent);
                cards.Remove(cardToAdd);
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
    }
}
