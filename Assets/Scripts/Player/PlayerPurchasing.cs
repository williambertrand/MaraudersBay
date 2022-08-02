using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPurchasing : MonoBehaviour
{

    // Fields / Behaviors needed for buying/selling
    ShipLifeHandler lifeHandler;
    PlayerGoldAndAmmoInventory playerInventory;

    [Header("Ship Purchasing and Selling")]
    public int repairCost;
    public int repairValue;

    [Header("Ammo purhasing")]
    public int ammoCost;
    public int ammoValue;


    void Start()
    {
        lifeHandler = GetComponent<ShipLifeHandler>();
        playerInventory = GetComponent<PlayerGoldAndAmmoInventory>();
    }



    public void OnPurchaseRepair()
    {
        if(playerInventory.playerCurrentGold >= repairCost)
        {
            lifeHandler.ApplyRepair(repairValue);
            playerInventory.SpendGold(repairCost);
        }
    }

    public void OnPurchaseCannonball()
    {
        if (playerInventory.playerCurrentGold >= ammoCost)
        {
            playerInventory.SpendGold(ammoCost);
            playerInventory.AddAmmo(ammoValue);
        }
    }


    public void OnSellChest()
    {
        // TODO: Hook up to actual inventory for getting chest count and selling
    }
}
