using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{

    // Gold related fields and UI
    [SerializeField] private int playerStartGold;
    public int playerCurrentGold;
    [SerializeField] private TMP_Text playerGoldText;

    // Start is called before the first frame update
    void Start()
    {
        playerCurrentGold = playerStartGold;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGoldCollect(int amount)
    {
        playerCurrentGold += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (playerGoldText != null)
        {
            playerGoldText.text = "" + playerCurrentGold;
        }
    }
}
