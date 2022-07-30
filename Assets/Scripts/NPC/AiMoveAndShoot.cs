using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMoveAndShoot : MonoBehaviour
{
    private Vector3 sailPoint;
    public float speed = 10f;
    public float sailPointRange = 100f;
    private bool sailPointSet;
    public float RotationSpeed = 1f;
    private Quaternion _lookRotation;
    private Vector3 _direction;
    private GameObject playerObj = null;
    public float sightRange = 20f;
    public float attackRange = 12f;

    public ShipFiring shipFiring;
    // public ShipLifeHandler shipLifeHandler;
    public float timeBetweenAttacks = 2f;
    bool alreadyAttacked;

    private void Start()
    {

        //If you want to find it by TAG. For this you have to make sure you give your player object the tag "Player".
        if (playerObj == null)
            playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Vector3 distanceToPlayer = transform.position - playerObj.transform.position;
        if (distanceToPlayer.magnitude > (sightRange * 10)) patrol();
        else attack(distanceToPlayer);
    }

    private void attack(Vector3 distanceToPlayer)
    {
        if (distanceToPlayer.magnitude > (attackRange * 10))
        {
            moveShipTo(playerObj.transform.position);
        }
        else
        {
            if (!alreadyAttacked)
            {
                ///Attack code here
                shipFiring.FireCannon();

                alreadyAttacked = true;
                Invoke(nameof(resetAttack), timeBetweenAttacks);
            }
        }

    }
    private void resetAttack()
    {
        alreadyAttacked = false;
    }

    private void moveShipTo(Vector3 newLocation)
    {
        //find the vector pointing from our position to the target
        _direction = (newLocation - transform.position).normalized;
        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);
        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
        // Move the object to new position
        transform.position = Vector3.MoveTowards(transform.position, newLocation, Time.deltaTime * speed);
    }

    private void patrol()
    {
        if (!sailPointSet)
        {
            getDestination();
            return;
        }

        moveShipTo(sailPoint);
        Vector3 distanceTosailPoint = transform.position - sailPoint;


        //sailPoint reached
        if (distanceTosailPoint.magnitude < 1f)
            sailPointSet = false;
    }

    private void getDestination()
    {
        float randomZ = Random.Range(-sailPointRange, sailPointRange);
        float randomX = Random.Range(-sailPointRange, sailPointRange);

        sailPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        sailPointSet = true;
    }
}
