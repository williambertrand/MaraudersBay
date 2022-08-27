using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerCameraControl : MonoBehaviour
{

    private bool isInCrowsNestState = false;

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineTransposer transposer;
    private CinemachineComposer composer;

    [SerializeField] private Transform focusObjectTransform;
    [SerializeField] private ShipFiring playerShipFiring;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.gameObject.TryGetComponent<CinemachineBrain>(out var brain);
        if(brain == null)
        {
            Debug.LogError("No cinemachine brain on main camera!");
        }

        brain.m_DefaultBlend.m_Time = 5;

        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        if (cinemachineVirtualCamera == null)
        {
            Debug.LogError("No cinemachineVirtualCamera found in scene!");
        }

        transposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        composer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineComposer>();

        if (playerShipFiring == null)
        {
            Debug.LogError("Player camera controls requires ship firing connection for disabling in crows nest");
        }

    }

    private void UpdateCamera(bool toCrowsNest)
    {
        if(toCrowsNest)
        {
            playerShipFiring.SetActive(false);
            transposer.m_FollowOffset = new Vector3(0.0f, 220.0f, 80.0f);
            composer.m_ScreenY = 0.95f;
        } else
        {
            playerShipFiring.SetActive(true);
            transposer.m_FollowOffset = new Vector3(0.0f, 120.0f, 120.0f);
            composer.m_ScreenY = 0.6f;
        }
    }


    public void ToggleCrowNestView(InputAction.CallbackContext ctx)
    {
        switch(ctx.phase)
        {
            case InputActionPhase.Performed:
                isInCrowsNestState = !isInCrowsNestState;
                UpdateCamera(isInCrowsNestState);
                break;
        }
    }

}
