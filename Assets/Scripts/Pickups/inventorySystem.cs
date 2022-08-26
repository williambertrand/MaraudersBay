using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventorySystem : MonoBehaviour
{
    private Dictionary<inventoryItemData, inventoryItem> m_itemDictionary;
    public List<inventoryItem> inventory; //{get; private set;}
    public delegate void inventoryChangedEvent();
    public static event inventoryChangedEvent OnInventoryChangedEvent;


    #region Singleton
    public static inventorySystem Instance;
    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    private void Start()
    {
        inventory = new List<inventoryItem>();
        m_itemDictionary = new Dictionary<inventoryItemData, inventoryItem>();
        if (OnInventoryChangedEvent != null) OnInventoryChangedEvent();
    }

    public inventoryItem Get(inventoryItemData referenceData)
    {
        if(m_itemDictionary.TryGetValue(referenceData, out inventoryItem value))
        {
            return value;
        }
        return null;
    }

    public int GetCount(inventoryItemData referenceData)
    {
        if (m_itemDictionary.TryGetValue(referenceData, out inventoryItem value))
        {
            return value.stackSize;
        }
        return 0;
    }

    public void Add(inventoryItemData referenceData)
    {
        if(m_itemDictionary.TryGetValue(referenceData, out inventoryItem value))
        {
            value.addToStack();
        }
        else
        {
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

    public void RemoveAll(inventoryItemData referenceData)
    {
        if (m_itemDictionary.TryGetValue(referenceData, out inventoryItem value))
        {
            inventory.Remove(value);
            m_itemDictionary.Remove(referenceData);
            if (OnInventoryChangedEvent != null) OnInventoryChangedEvent();
        }
    }

    public void Clear()
    {
        inventory.Clear();
        m_itemDictionary.Clear();
        if (OnInventoryChangedEvent != null) OnInventoryChangedEvent();
    }

}
