using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{

    // Gold related fields and UI
    [SerializeField] private int playerStartGold;
    public int playerCurrentGold;
    [SerializeField] private TMP_Text playerGoldText;


    // Cannon Ammo related fields and UI
    [SerializeField] private int ammoStart;
    public int ammoCurrent;
    [SerializeField] private TMP_Text ammoText;

    // Start is called before the first frame update
    void Start()
    {
        // Set starting health
        playerCurrentGold = playerStartGold;
        UpdateUI(playerGoldText, playerCurrentGold);

        // Set starting ammo
        ammoCurrent = ammoStart;
        UpdateUI(ammoText, ammoCurrent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGoldCollect(int amount)
    {
        playerCurrentGold += amount;
        UpdateUI(playerGoldText, playerCurrentGold);
    }

    private void UpdateUI(TMP_Text textField, int value)
    {
        if (textField != null)
        {
            textField.text = "" + value;
        }
    }


    // Return true on successfuly using up "amount" ammo
    public bool ExpendAmmo(int amount)
    {
        if(ammoCurrent >= amount)
        {
            ammoCurrent -= amount;
            UpdateUI(ammoText, ammoCurrent);
            return true;
        }
        return false;
    }
}
