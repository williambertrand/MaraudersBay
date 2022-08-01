using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerReputation : MonoBehaviour
{
    // Reputation related fields and UI
    [SerializeField] private int startReputation;
    public int playerCurrentReputation;
    [SerializeField] private TMP_Text playerReputationText;

    // Start is called before the first frame update
    void Start()
    {
        playerCurrentReputation = startReputation;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateReputationBy(int amount)
    {
        playerCurrentReputation += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (playerReputationText != null)
        {
            playerReputationText.text = "" + playerCurrentReputation;
        }
    }
}
