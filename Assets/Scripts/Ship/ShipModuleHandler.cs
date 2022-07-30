using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipModuleHandler : MonoBehaviour  {
    static List<ShipModule> modules;

    static List<ShipModule> GetShopModules() {
        return modules.FindAll(module => module.isAvailableToPlayer());
    }
}
