using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Transform aimTarget;

    public Vector3 moveDirection;

    public float jumpHeight = 1.0f;

    public float gravity = 1.0f;

        // The speed when walking
    public float walkSpeed = 2.0f;
    // after trotAfterSeconds of walking we trot with trotSpeed
    public float trotSpeed = 4.0f;
    // when pressing "Fire3" button (cmd) we start running
    public float runSpeed = 6.0f;

    // The gravity in controlled descent mode
    public float speedSmoothing = 10.0f;
    public float rotateSpeed = 500.0f;
    public float trotAfterSeconds = 3.0f;

    // The camera doesnt start following the target immediately but waits for a split second to avoid too much waving around.
    private float lockCameraTimer = 0.0f;

    // The current x-z move speed
    private float moveSpeed = 0.0f;

    // The last collision flags returned from controller.Move
    private CollisionFlags collisionFlags; 

    // Is the user pressing any keys?
    private bool isMoving = false;
    // When did the user start walking (Used for going into trot after a while)
    private float walkTimeStart = 0.0f;

    float hopTime = 0.0f;
    float hopMaxTimer = 0.3f;
    bool isUp = false;

    void Awake()
    {
        moveDirection = Vector3.zero;
    }

    void UpdateSmoothedMovementDirection()
    {
        Transform cameraTransform = Camera.main.transform;

        // Forward vector relative to the camera along the x-z plane	
        Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        // Right vector relative to the camera
        // Always orthogonal to the forward vector
        Vector3 right = new Vector3(forward.z, 0, -forward.x);

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        var wasMoving = isMoving;
        isMoving = Mathf.Abs(h) > 0.1 || Mathf.Abs(v) > 0.1;

        // Target direction relative to the camera
        Vector3 targetDirection = h * right + v * forward;


        // Lock camera for short period when transitioning moving & standing still
        lockCameraTimer += Time.deltaTime;
        if (isMoving != wasMoving)
            lockCameraTimer = 0.0f;

        // We store speed and direction seperately,
        // so that when the character stands still we still have a valid forward direction
        // moveDirection is always normalized, and we only update it if there is user input.
        if (targetDirection != Vector3.zero)
        {
            moveDirection = targetDirection.normalized;
        }

        // Choose target speed
        //* We want to support analog input but make sure you cant walk faster diagonally than just forward or sideways
        var targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0f);

        // Pick speed modifier
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            targetSpeed *= runSpeed;
        }
        else if (Time.time - trotAfterSeconds > walkTimeStart)
        {
            targetSpeed *= trotSpeed;
        }
        else
        {
            targetSpeed *= walkSpeed;
        }

        moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, 1);

        // Reset walk time start when we slow down
        if (moveSpeed < walkSpeed * 0.3)
            walkTimeStart = Time.time;
    }
    

	void Update () {
        UpdateSmoothedMovementDirection();

        //if (Input.GetButton("Jump") && IsGrounded())
        //{
        //    moveDirection.y += jumpHeight;
        //}

        //if(!IsGrounded())
        //    moveDirection.y -= gravity;
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            PlayerHopOnMove();
        else
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        // Calculate actual motion
        Vector3 movement = moveDirection * moveSpeed;
        movement *= Time.deltaTime;

        // Move the controller
        CharacterController controller = GetComponent<CharacterController>();
        collisionFlags = controller.Move(movement);


        transform.LookAt(new Vector3(aimTarget.position.x, 0 + movement.y, aimTarget.position.z), Vector3.up);
	}

    bool IsGrounded()
    {
        return (collisionFlags == CollisionFlags.Below);
    }

    void PlayerHopOnMove()
    {
        hopTime += Time.deltaTime;

        if (hopTime >= hopMaxTimer)
        {
            if (isUp)
            {
                transform.position += Vector3.up * 0.1f;
                isUp = false;
            }
            else
            {
                transform.position += Vector3.down * 0.1f;
                isUp = true;
            }
            hopTime = 0.0f;
        }
    }

}
