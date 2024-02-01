using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Gravity
    [SerializeField] private GameObject Planet;
    [SerializeField][Range(0, 100)] private float gravity = 100;

    //Player Movement
    private Rigidbody rigidbody;

    [SerializeField] private float speed = 4;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpHeight = 1.2f;

    // Ground
    [SerializeField] private bool OnGround = false;

    private float distanceToGround;
    Vector3 floorNormal;

    private Vector3 move;
    private float moveX;
    private float moveZ;

    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        moveZ = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        move = new Vector3 (moveX, 0, moveZ);
        moveDirection = move.normalized;

        // Walk
        if (Input.GetKey(GlobalInputs.moveForwardKey) ||
            Input.GetKey(GlobalInputs.moveBackwardKey) ||
            Input.GetKey(GlobalInputs.moveLeftKey) ||
            Input.GetKey(GlobalInputs.moveRightKey))
        {
            transform.Translate(move);
            //rigidbody.velocity = move * Time.deltaTime * speed;

            //float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(transform.rotation.x, targetAngle, transform.rotation.z);
        }
    
        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(transform.up * 40000 * jumpHeight * Time.deltaTime);
        }

        // GroundControl
        RaycastHit hit = new RaycastHit();
        Debug.DrawRay(transform.position, -transform.up, Color.green);
        if(Physics.Raycast(transform.position, -transform.up ,out hit, 100))
        {

            distanceToGround = hit.distance;
            floorNormal = hit.normal;

            if(distanceToGround <= 2f)
            {
                OnGround = true;
            }
            else
            {
                OnGround = false;
            }
        }

        // Gravity and Rotation
        Vector3 gravDirection = (transform.position - Planet.transform.position).normalized;
        
        if(OnGround == false)
        {
            rigidbody.AddForce(gravDirection * -gravity);
        }

        // 

        Quaternion toRotation = Quaternion.FromToRotation(transform.up, floorNormal) * transform.rotation;
        transform.rotation = toRotation;
    }
}