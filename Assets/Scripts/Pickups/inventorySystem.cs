using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventorySystem : MonoBehaviour
{
    public static inventorySystem current;
    private Dictionary<inventoryItemData, inventoryItem> m_itemDictionary;
    public List<inventoryItem> inventory; //{get; private set;}
    public delegate void inventoryChangedEvent();
    public static event inventoryChangedEvent OnInventoryChangedEvent;

    private void Awake() 
    {
        current = this;
        inventory = new List<inventoryItem>();
        m_itemDictionary = new Dictionary<inventoryItemData, inventoryItem>();
        if(OnInventoryChangedEvent != null) OnInventoryChangedEvent();
    }

    public inventoryItem Get(inventoryItemData referenceData)
    {
        if(m_itemDictionary.TryGetValue(referenceData, out inventoryItem value))
        {
            return value;
        }
        return null;
    }

    public void Add(inventoryItemData referenceData)
    {
        if(m_itemDictionary.TryGetValue(referenceData, out inventoryItem value))
        {
            // Debug.Log("Adding to stack.");
            value.addToStack();
        }
        else
        {
            // Debug.Log("Adding new item.");
            inventoryItem newItem = new inventoryItem(referenceData);
            inventory.Add(newItem);
            m_itemDictionary.Add(referenceData, newItem);
        }
        if(OnInventoryChangedEvent != null) OnInventoryChangedEvent();
    }

    public void Remove(inventoryItemData referenceData)
    {
        if(m_itemDictionary.TryGetValue(referenceData, out inventoryItem value))
        {
           
            value.removeFromStack();

            if(value.stackSize == 0)
            {
                inventory.Remove(value);
                m_itemDictionary.Remove(referenceData);
            }
            if(OnInventoryChangedEvent != null) OnInventoryChangedEvent();
        }
    }

}
