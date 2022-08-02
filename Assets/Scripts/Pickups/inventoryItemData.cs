using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Item")]
public class inventoryItemData : ScriptableObject {

    public string id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;
    
}

