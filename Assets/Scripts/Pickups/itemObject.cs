using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemObject : MonoBehaviour
{
    public inventoryItemData referenceItem;

    public void onHandlePickupItem()
    {
        Debug.Log("onHandlePickupItem running");
        inventorySystem.current.Add(referenceItem);
        Destroy(gameObject);
    }
}
