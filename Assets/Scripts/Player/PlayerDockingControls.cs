using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct DockUI
{
    public GameObject UI;
    public DockType forDockType;
}


public class PlayerDockingControls : MonoBehaviour
{

    private ShipFiring firing;
    private ShipMovement movement;
    Rigidbody rigidBody;

    public List<DockUI> DockingUis;

    // Start is called before the first frame update
    void Start()
    {
        firing = GetComponent<ShipFiring>();
        movement = GetComponent<ShipMovement>();
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Dock(DockType type)
    {
        firing.enabled = false;
        movement.enabled = false;
        rigidBody.velocity = Vector3.zero;

        DockUI uiToShow = DockingUis.Find(ui => ui.forDockType == type);
        uiToShow.UI.SetActive(true);
    }

    public void UnDock(DockType type)
    {
        firing.enabled = true;
        movement.enabled = true;

        DockUI uiToHide= DockingUis.Find(ui => ui.forDockType == type);
        uiToHide.UI.SetActive(false);
    }
}
