using UnityEngine;

public class itemObject : MonoBehaviour
{
    public inventoryItemData referenceItem;

    public void onHandlePickupItem()
    {
        inventorySystem.Instance.Add(referenceItem);
        Destroy(gameObject);
    }
}
