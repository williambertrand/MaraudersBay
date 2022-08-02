using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsModulesHandler : MonoBehaviour  {
    public static List<ShipModule> modules;
    public List<ShipModule> unityLoadModules;
    
    public static List<ShipModule> GetShopModules() {
        return modules.FindAll(module => module.isAvailableToPlayer());
    }

    void Start() {
        modules = unityLoadModules;
    }
}
