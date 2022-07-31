using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DockType
{
    OUTPOST,
    ISLAND
}

public class Dock : MonoBehaviour
{

    [SerializeField] private DockType dockType;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIControlls ui = other.gameObject.GetComponent<UIControlls>();
            ui.EnableDockingOption(dockType);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIControlls ui = other.gameObject.GetComponent<UIControlls>();
            ui.DisableDockingOption();
        }
    }
}
