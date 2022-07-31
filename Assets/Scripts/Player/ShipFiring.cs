using UnityEngine;
using UnityEngine.InputSystem;

public class ShipFiring : MonoBehaviour
{

    public enum Side
    {
        PORT,
        STARBOARD
    }

    [Header("Fields for player vs ship controlled")]
    [SerializeField] private bool isPlayerControlled;
    [SerializeField] private bool isInventoryLimited;
    private PlayerInventory inventory;

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
        if(isInventoryLimited)
        {
            inventory = GetComponent<PlayerInventory>();
            if (inventory == null)
            {
                Debug.LogError("Inventory component required if ship has limited firing inventory");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FireCannon()
    {

        if (Time.time - lastFireTime <= reloadTime) return;

        if (isInventoryLimited)
        {
            bool canFire = inventory.ExpendAmmo(1);
            if (!canFire) return;
        }

        Transform initPoint = toFireSide == Side.PORT ? PORT_Firing : STARBOARD_Firing;


        // Create projectile and set info
        GameObject proj = Instantiate(projectile, initPoint.position, Quaternion.identity);
        proj.gameObject.tag = gameObject.tag;

        BasicProjectile projectileInfo = proj.GetComponent<BasicProjectile>();
        projectileInfo.actor = gameObject;

        // Actually fire the cannon ball
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

        if (!isPlayerControlled) return;

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
