using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{
    public float Speed;
    public Rigidbody rigidBody;


    private Vector2 movementInput;
    public float turnSpeed;
    public float moveForce;

    private Vector2 _moveInput;
    //private PlayerActions playerActions;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
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
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}
