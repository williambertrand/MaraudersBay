using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{
    public float Speed;
    public Rigidbody rigidBody;


    private Vector2 movementInput;
    public float turnSpeed;
    public float moveForce;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.maxAngularVelocity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float inputVertical = movementInput.y;
        float inputHorizontal = movementInput.x;


        float turn = inputHorizontal * turnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
        rigidBody.MoveRotation(rigidBody.rotation * turnRotation);
        Vector3 movement = transform.forward * -1 * inputVertical * moveForce;
        rigidBody.AddForceAtPosition(movement, rigidBody.transform.position);

        // Add a slight slow down force when turning
        if (Mathf.Abs(turn) > 0f)
        {
            rigidBody.AddForceAtPosition(rigidBody.velocity * -0.1f, rigidBody.transform.position);
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public float GetSpeedSqr()
    {
        return rigidBody.velocity.sqrMagnitude;
    }
}
