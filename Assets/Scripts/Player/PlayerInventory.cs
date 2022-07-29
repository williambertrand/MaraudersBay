using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{

    // Gold related fields and UI
    [SerializeField] private int playerStartGold;
    public int playerCurrentGold;
    [SerializeField] private Text playerGoldText;

    // Start is called before the first frame update
    void Start()
    {
        playerCurrentGold = playerStartGold;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGoldCollect(int amount)
    {
        playerCurrentGold += amount;
        // Trigger UI Update
        if (playerGoldText != null)
        {
            playerGoldText.text = "" + playerCurrentGold;
        }
    }
}
