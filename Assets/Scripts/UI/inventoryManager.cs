using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class inventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_slotPrefab;

    public void Start()
    {
        inventorySystem.OnInventoryChangedEvent += OnUpdateInventory;
    }

    private void OnDisable() {
        inventorySystem.OnInventoryChangedEvent -= OnUpdateInventory;
    }

    private void OnUpdateInventory()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        DrawInventory();
    }

    public void DrawInventory()
    {
        foreach(inventoryItem item in inventorySystem.Instance.inventory)
        {
            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(inventoryItem item)
    {
        GameObject obj = Instantiate(m_slotPrefab);
        obj.transform.SetParent(transform, false);
        itemSlot slot = obj.GetComponent<itemSlot>();
        slot.Set(item);
    }

}