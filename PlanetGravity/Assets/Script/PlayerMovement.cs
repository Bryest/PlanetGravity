using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Gravity
    [SerializeField] private GameObject planet;
    [SerializeField][Range(0, 100)] private float gravity = 100;

    //Player Movement
    private Rigidbody rb;

    [SerializeField] private float speed = 4;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpHeight = 1.2f;

    // Ground
    [SerializeField] private bool isGrounded = false;
    private Vector3 floorNormal;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * transform.up.x;
        float moveY = 1 * transform.right.y;
        float moveZ = Input.GetAxis("Vertical") * transform.forward.z;

        Vector3 move = new Vector3 (moveX, moveY, moveZ);
        
        Vector3 dir = (transform.position - planet.transform.position);
        // Walk
        if (Input.GetKey(GlobalInputs.moveForwardKey) ||
            Input.GetKey(GlobalInputs.moveBackwardKey) ||
            Input.GetKey(GlobalInputs.moveLeftKey) ||
            Input.GetKey(GlobalInputs.moveRightKey))
        {
            //transform.Translate(move);
        
            //rb.velocity = Vector3.Cross(move, dir) * Time.deltaTime * speed;
            rb.velocity = move * Time.deltaTime * speed;

            //float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(transform.rotation.x, targetAngle, transform.rotation.z);
        }

        // Jump
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpHeight);
            isGrounded= false;
        }
    }

    private void FixedUpdate()
    {
        // GroundControl
        Debug.DrawRay(transform.position, -transform.up * 10f, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 100))
        {

            float distanceToGround = hit.distance;
            floorNormal = hit.normal;

            if (distanceToGround <= 0.51f)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }

        // Gravity Rotation
        Quaternion toRotation = Quaternion.FromToRotation(transform.up, floorNormal) * transform.rotation;
        transform.rotation = toRotation;

        // Gravity Force
        Vector3 gravDirection = (transform.position - planet.transform.position).normalized;
        if (isGrounded == false)
        {
            rb.AddForce((gravDirection * -gravity) * rb.mass);
        }
    }
}