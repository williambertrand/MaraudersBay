using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public enum ShipModuleType {
    Canon,
    Mast
    //TODO: Add all modules types
}

[Serializable]
public struct ShipModule {
    public GameObject prefab;
    public double cost;
    public ShipModuleType type;

    public bool isAvailableToPlayer() {
        //TODO: Add checkes
        return true;
    }
}

[Serializable]
public struct ShipModuleLocation {
    public ShipModuleType type;
    //Relative location to ship base prefab;
    public Vector3 relativeLocation;
    //If null modules is not being used
    public ShipModule? module;
}

[Serializable]
public struct ShipBase {
    public GameObject prefab;
    public List<ShipModuleLocation> modules;
}

public class ShipVisualHandler : MonoBehaviour {

    //Base of  the ship
    public ShipBase shipBase;
}
