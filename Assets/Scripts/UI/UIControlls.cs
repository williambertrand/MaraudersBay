using System;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;


public class UIControlls : MonoBehaviour
{
    private bool actionDebounce = false;
    private enum Action
    {
        DOCK,
        UNDOCK
        //...
    }

    [SerializeField] private TMP_Text interactionText;
    private Action currentAction;
    private DockType currentDockType;

    private PlayerDockingControls dockingControls;

    // Start is called before the first frame update
    void Start()
    {
        dockingControls = GetComponent<PlayerDockingControls>();
    }

    public void EnableDockingOption(DockType type)
    {
        interactionText.gameObject.SetActive(true);
        interactionText.text = "Press E to Dock"; //TODO: Xbox mapping
        currentAction = Action.DOCK;
        currentDockType = type;
    }

    public void DisableDockingOption()
    {
        interactionText.gameObject.SetActive(false);
    }

    private void HandleInteract()
    {
        actionDebounce = true;

        switch (currentAction)
        {
            case Action.DOCK:
                DisableDockingOption();
                dockingControls.Dock(currentDockType);
                StartCoroutine(SwitchToUndock());
                break;
            case Action.UNDOCK:
                dockingControls.UnDock(currentDockType);
                actionDebounce = false;
                break;
            default:
                break;
        }
    }


    public void OnInteract(InputAction.CallbackContext context)
    {
        if (actionDebounce) return;

        switch (context.phase)
        {
            case InputActionPhase.Performed:
                HandleInteract();
                break;
            default:
                break;
        }
    }

    IEnumerator SwitchToUndock()
    {
        yield return new WaitForSeconds(0.75f);
        currentAction = Action.UNDOCK;
        actionDebounce = false;
    }
}
