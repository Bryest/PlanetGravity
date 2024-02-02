using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Gravity
    [SerializeField] private GameObject planet;
    private Rigidbody rb;

    [SerializeField] private float speed = 600f;
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private float jumpHeight = 350f;

    // Ground
    [SerializeField] private bool isGrounded = false;
 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * speed * transform.right.normalized.x;
        //float moveY = rb.velocity.y;
        //float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * speed * transform.forward.normalized.z;
        
        Vector3 moveX = Input.GetAxis("Horizontal") * transform.right.normalized ; // to local space
        Vector3 moveZ = Input.GetAxis("Vertical") * transform.forward.normalized; // to local space
        
        Vector3 movementXZ = (moveX + moveZ) * Time.deltaTime * speed; // local velocity in XZ
        Vector3 limitMovementXZ = Vector3.ClampMagnitude(movementXZ,5);
        
        
        /*Vector3 positionCamera = target.transform.right * 0 + target.transform.up * 11 +
                                target.transform.forward * -3.85f;*/
        /*transform.position = positionCamera; */
        //Vector3 move = new Vector3 (moveX, moveY, moveZ);
        Vector3 localrb = transform.InverseTransformDirection(rb.velocity);
        
        

        Vector3 dir = (transform.position - planet.transform.position);
        // Walk
        if (Input.GetKey(GlobalInputs.moveForwardKey) ||
            Input.GetKey(GlobalInputs.moveBackwardKey) ||
            Input.GetKey(GlobalInputs.moveLeftKey) ||
            Input.GetKey(GlobalInputs.moveRightKey))
        {
            //transform.Translate(movementXZ);

            //rb.velocity = Vector3.Cross(move, dir) * Time.deltaTime * speed;
            //rb.velocity = new Vector3 (movementXZ.x, movementXZ.y, movementXZ.z);
            rb.AddForce(limitMovementXZ , ForceMode.VelocityChange);
            //float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(transform.rotation.x, targetAngle, transform.rotation.z);
        }
        

        // Jump
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode.VelocityChange);

            isGrounded = false;
        }


        rb.velocity = Vector3.ClampMagnitude(rb.velocity, 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}