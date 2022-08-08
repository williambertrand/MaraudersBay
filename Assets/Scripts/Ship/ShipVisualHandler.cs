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
    public Vector3 rotation;
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
    GameObject childBase;

    void AddModule(ShipModule module, ShipModuleLocation location) {
        ShipModule mod = new ShipModule();
        mod.prefab = Instantiate(module.prefab, transform.position + location.relativeLocation, Quaternion.Euler(location.rotation), transform);
        location.module = mod;
    }    
    
    void AddModule(ShipModule module, int location) {
        AddModule(module, shipBase.modules[location]);
    }
    
    void Start() {
        if (childBase)
            return;

        childBase = Instantiate(shipBase.prefab, transform.position, Quaternion.identity, transform);
        
        //Testing
        List<ShipModule> modules = ShipsModulesHandler.GetShopModules();
        AddModule(modules[0], shipBase.modules[0]);
        AddModule(modules[1], 1);
    }
}