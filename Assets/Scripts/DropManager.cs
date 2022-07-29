using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{

    [SerializeField] GameObject goldDrop;
    #region Singleton
    public static DropManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    #endregion

    // TODO: Call on enemy ship death to drop gold at the ship location
    void SpawnGoaldAt(Vector3 pos, int amount)
    {
        GameObject goldObj = Instantiate(goldDrop, pos, Quaternion.identity);
        GoldPickup pickup = goldObj.GetComponent<GoldPickup>();
        if (pickup != null) pickup.amount = amount;
    }
}
