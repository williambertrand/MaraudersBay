using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class Outpost : MonoBehaviour
{

    public GameObject playerTarget;

    [SerializeField] private GameObject fireEffect;

    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject canonModel;
    [SerializeField] private Transform firePoint; 
    [SerializeField] private float reloadTime;
    [SerializeField] private int damage;
    [SerializeField] private float maxRange;
    [SerializeField] private float firingPower;
    private float lastFire;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SphereCollider>().radius = maxRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTarget != null)
        {
            Vector3 lookPoint = new Vector3(
                playerTarget.transform.position.x,
                canonModel.transform.position.y,
                playerTarget.transform.position.z
            );
            canonModel.transform.LookAt(lookPoint);
            if(Time.time - reloadTime > lastFire)
            {
                FireAtPlayer();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerTarget = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerTarget = other.gameObject;
        }
    }

    /**
     * TODO: We should be able to consolidate a lot of the "Firing" logic 
     * shared between this component and ShipFiring, but will handle that in the future
     */
    private void FireAtPlayer()
    {

        // Create projectile and set info
        GameObject proj = Instantiate(projectile, firePoint.position, Quaternion.identity);
        proj.gameObject.tag = gameObject.tag;

        BasicProjectile projectileInfo = proj.GetComponent<BasicProjectile>();
        projectileInfo.actor = gameObject;
        projectileInfo.Damage = damage;

        // Actually fire the cannon ball
        Rigidbody rb = proj.GetComponent<Rigidbody>();

        Rigidbody playerMovement = playerTarget.GetComponent<Rigidbody>();

        float distanceToPlayer = Vector3.Distance(firePoint.transform.position, playerTarget.transform.position);
        float forwardOffset = Mathf.Lerp(0, 10, distanceToPlayer / maxRange);


        Vector3 aim = (
            playerTarget.transform.position
            - new Vector3(0, 10, 0) // F
            + playerMovement.velocity.normalized * forwardOffset
        ) - firePoint.transform.position;


        rb.AddForce(firingPower * aim);

        if (fireEffect != null)
        {
            Instantiate(fireEffect, firePoint.position, Quaternion.identity);
        }

        lastFire = Time.time;
    }
}
