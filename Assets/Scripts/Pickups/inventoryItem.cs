using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class inventoryItem
{
    public inventoryItemData data;// {get; private set;}
    public int stackSize;// {get; private set;}

    public inventoryItem(inventoryItemData source)
    {
        data = source;
        addToStack();
    }

    public void addToStack()
    {
        stackSize++;
    }

    public void removeFromStack()
    {
        stackSize--;
    }

}
