using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Gravity
    [SerializeField] private GameObject planet;
    private Rigidbody rb;

    [SerializeField] private float speed = 600f;
    [SerializeField] private float jumpHeight = 350f;

    // Ground
    public bool isGrounded = false;

    // Forces
    [NonSerialized] public Vector3 jumpForce;
    [NonSerialized] public Vector3 movementForce;
    [NonSerialized] private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        Vector3 moveX = Input.GetAxis("Horizontal") * transform.right.normalized; // to local space
        Vector3 moveZ = Input.GetAxis("Vertical") * transform.forward.normalized; // to local space

        Vector3 movementXZ = (moveX + moveZ) * Time.deltaTime * speed; // local velocity in XZ

        movementXZ = Vector3.Lerp(rb.velocity, movementXZ, 1); //The movement that we want

        if ((Input.GetKey(GlobalInputs.moveForwardKey) ||
            Input.GetKey(GlobalInputs.moveBackwardKey) ||
            Input.GetKey(GlobalInputs.moveLeftKey) ||
            Input.GetKey(GlobalInputs.moveRightKey)) && isGrounded)
        {
            movementForce = movementXZ;
        }
    }

    void Jump()
    {
        // Jump
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            jumpForce = transform.up * jumpHeight;
            isJumping = true;
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        movementForce = Vector3.zero;
    }
    private void OnCollisionExit(Collision collision)
    {
        ResetJumpForce();
        isGrounded = false;
       
        jumpForce = Vector3.zero;
        movementForce = Vector3.zero;
    }

    private void OnTriggerExit(Collider other)
    {
       jumpForce = Vector3.zero;
    }

    private void ResetJumpForce()
    {
        if (isJumping)
        {
            jumpForce = Vector3.zero;
        }
        if (jumpForce == Vector3.zero)
        {
            isJumping= false;
        }
    }
}