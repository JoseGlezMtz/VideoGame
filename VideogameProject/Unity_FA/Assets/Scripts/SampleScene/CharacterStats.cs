using UnityEngine;

public class CharacterStats{
    public int amount;
    public int character_card_id;

    public CharacterStats(int character_counter, int character_cardId){
        character_card_id = character_cardId;
        amount = character_counter;
    }
}