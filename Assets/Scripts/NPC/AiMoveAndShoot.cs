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
        shipFiring = GetComponent<ShipFiring>();
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
            // TODO: Making a note here to update this desired location
            // to be a bit forward and to the side of the palyer ship,
            // to simulate an enemy ship trying to broadside in order to attack
            moveShipTowards(playerObj.transform.position);
        }
        else
        {
            //Rotate the enemy to attack position
            rotateShipToAttackPosition(playerObj.transform.position);

            if (!alreadyAttacked)
            {

                // Determine port vs starboard
                // if VectorResult.X < 0, firing port, else starboard
                Vector3 VectorResult;
                float DotResult = Vector3.Dot(transform.forward, playerObj.transform.forward);
                if (DotResult > 0)
                {
                    VectorResult = transform.forward + playerObj.transform.forward;
                }
                else
                {
                    VectorResult = transform.forward - playerObj.transform.forward;
                }

                // Currently a ship will always rotate to put the
                // player on the starboard side, even if that is a longer turn
                Side fireSide = Side.STARBOARD;

                //Attack code here
                shipFiring.FireCannons(fireSide);


                alreadyAttacked = true;
                Invoke(nameof(resetAttack), timeBetweenAttacks);
            }
        }

    }
    private void resetAttack()
    {
        alreadyAttacked = false;
    }

    private void rotateShipToAttackPosition(Vector3 newLocation) 
    {
        
        //find the vector pointing from our position to the target
        _direction = (newLocation - transform.position).normalized;
        //create the rotation we need to be in to be side on to the target
        _lookRotation = Quaternion.LookRotation(_direction) * Quaternion.Euler(0, 90, 0);
        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
    }

    private void moveShipTowards(Vector3 newLocation)
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

        moveShipTowards(sailPoint);
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
