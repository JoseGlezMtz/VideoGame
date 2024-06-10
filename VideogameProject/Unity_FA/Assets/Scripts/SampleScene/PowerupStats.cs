using UnityEngine;

public class PowerupStats{
    public int amount;
    public int PU_card_id;

    public PowerupStats(int counter, int id){
        PU_card_id = id;
        amount = counter;
    }
}