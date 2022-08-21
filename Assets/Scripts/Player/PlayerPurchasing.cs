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

    [Header("Chest selling")]
    public inventoryItemData chestItemData;
    public int chestValue;

    [Header("Cannons Purchasing")]
    public List<GameObject> canonPurchasingButtons;
    public ShipFiring playerShipFiring;
    public int cannonCost;


    void Start()
    {
        lifeHandler = GetComponent<ShipLifeHandler>();
        playerInventory = GetComponent<PlayerGoldAndAmmoInventory>();
        playerShipFiring = GetComponent<ShipFiring>();
        UpdateCannonUI();
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
        int playerChestCount = inventorySystem.Instance.GetCount(chestItemData);

        if(playerChestCount > 0)
        {
            inventorySystem.Instance.Remove(chestItemData);
            playerInventory.OnGoldCollect(chestValue);
        }
    }

    public void OnPurchaseCannon(int index)
    {
        if(index >= playerShipFiring.allCannons.Count)
        {
            Debug.LogError("Index to buy cannon out of bounds");
            return;
        }
        Cannon cannonToBuy = playerShipFiring.allCannons[index];

        if(cannonToBuy.enabled)
        {
            return;
        }

        if (playerInventory.playerCurrentGold >= cannonCost)
        {
            playerInventory.SpendGold(ammoCost);
            playerShipFiring.EnableCannon(index);
            UpdateCannonUI();
        }
    }

    private void UpdateCannonUI()
    {
        for(int  i = 0; i < canonPurchasingButtons.Count; i++)
        {
            if(playerShipFiring.allCannons[i].enabled)
            {
                Transform openIcon = canonPurchasingButtons[i].transform.Find("Open");
                openIcon.gameObject.SetActive(false);

                Transform checkedIcon = canonPurchasingButtons[i].transform.Find("Checked");
                checkedIcon.gameObject.SetActive(true);
            }
        }
    }
}
