using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipFiring : MonoBehaviour
{

    public enum Side
    {
        PORT,
        STARBOARD
    }

    [SerializeField] private Transform PORT_Firing;
    [SerializeField] private Transform STARBOARD_Firing;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float firingPower;

    [SerializeField] private float reloadTime;
    private float lastFireTime;

    [SerializeField] private float accuracyValue;

    private Side toFireSide = Side.PORT;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FireCannon()
    {

        if (Time.time - lastFireTime <= reloadTime) return;

        Transform initPoint = toFireSide == Side.PORT ? PORT_Firing : STARBOARD_Firing;

        GameObject proj = Instantiate(projectile, initPoint.position, Quaternion.identity);

        proj.gameObject.tag = gameObject.tag;

        Rigidbody rb = proj.GetComponent<Rigidbody>();


        Vector3 accuracyOffset = new Vector3(
            Random.Range(-accuracyValue, accuracyValue),
            Random.Range(-accuracyValue, accuracyValue),
            0.0f
        );

        Vector3 aim = initPoint.transform.forward + accuracyOffset;

        rb.AddForce(firingPower * aim);

        // Swap firing to other side
        toFireSide = toFireSide == Side.PORT ? Side.STARBOARD : Side.PORT;

        lastFireTime = Time.time;
    }

    public void OnFire(InputAction.CallbackContext context)
    {

        switch (context.phase)
        {
            case InputActionPhase.Performed:
                FireCannon();
                break;
            default:
                break;
        }


    }

}
