using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Side
{
    PORT,
    STARBOARD
}

[System.Serializable]
public class Cannon
{
    public string name;
    public bool enabled { get; set; } = false;
    public Transform initPoint;
}

public class ShipFiring : MonoBehaviour
{

    [Header("Fields for player vs ship controlled")]
    [SerializeField] private bool isPlayerControlled;
    [SerializeField] private bool isInventoryLimited;
    private PlayerGoldAndAmmoInventory inventory;

    [SerializeField] private List<Cannon> PORT_Cannons;
    [SerializeField] private List<Cannon> STARBOARD_Cannons;
    public List<Cannon> allCannons;

    [SerializeField] private GameObject projectile;
    [SerializeField] private float firingPower;

    [SerializeField] private float reloadTime;
    private Dictionary<Side, float> lastFireTimes;

    [SerializeField] private float accuracyValue;

    [SerializeField] private GameObject fireEffect;

    // Start is called before the first frame update
    void Start()
    {
        if(isInventoryLimited)
        {
            inventory = GetComponent<PlayerGoldAndAmmoInventory>();
            if (inventory == null)
            {
                Debug.LogError("Inventory component required if ship has limited firing inventory");
            }
        }

        lastFireTimes = new Dictionary<Side, float>();
        lastFireTimes.Add(Side.PORT, 0.0f);
        lastFireTimes.Add(Side.STARBOARD, 0.0f);

        allCannons = new List<Cannon>();
        for(int i = 0; i < PORT_Cannons.Count; i++)
        {
            allCannons.Add(PORT_Cannons[i]);
        }
        for (int i = 0; i < STARBOARD_Cannons.Count; i++)
        {
            allCannons.Add(STARBOARD_Cannons[i]);
        }


        // Begin with 1 cannon enabled on each side of ship
        PORT_Cannons[0].enabled = true;
        STARBOARD_Cannons[0].enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FireCannons(Side fireSide)
    {

        if (Time.time - lastFireTimes[fireSide] <= reloadTime) return;

        List<Cannon> cannonsToFire = fireSide == Side.PORT ? PORT_Cannons : STARBOARD_Cannons;


        foreach(Cannon cannon in cannonsToFire)
        {
            if (!cannon.enabled) continue;

            if (isInventoryLimited)
            {
                bool canFire = inventory.ExpendAmmo(1);
                if (!canFire) break;
            }

            // Create projectile and set info
            GameObject proj = Instantiate(projectile, cannon.initPoint.position, Quaternion.identity);
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
            Vector3 aim = cannon.initPoint.transform.forward + accuracyOffset;
            rb.AddForce(firingPower * aim);

            if (fireEffect != null)
            {
                Instantiate(fireEffect, cannon.initPoint.position, Quaternion.identity);
            }
        }

        lastFireTimes[fireSide] = Time.time;
    }

    public void OnFirePort(InputAction.CallbackContext context)
    {

        if (!isPlayerControlled) return;

        switch (context.phase)
        {
            case InputActionPhase.Performed:
                FireCannons(Side.PORT);
                break;
            default:
                break;
        }
    }

    public void OnFireStarboard(InputAction.CallbackContext context)
    {

        if (!isPlayerControlled) return;

        switch (context.phase)
        {
            case InputActionPhase.Performed:
                FireCannons(Side.STARBOARD);
                break;
            default:
                break;
        }
    }

    public void EnableCannon(int i)
    {
        allCannons[i].enabled = true;
    }
}
