using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBehaviour : MonoBehaviour
{
    public Orbit orbit;
    public float gravityForce;
    private Rigidbody rb;
    PlayerMovement playerMovement;

    [SerializeField] private float RotationSpeed = 20;

    //Force
    public Vector3 gravity;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        playerMovement= GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If is in the planet orbit
        if (orbit)
        {
            Vector3 gravityDir;
            if (orbit.fixedDirection)
            {
                gravityDir = orbit.transform.up;
            }
            else
            {
                gravityDir = (transform.position - orbit.transform.position).normalized;
            }

            // Rotate the object with the surface ot the gravity
            transform.up = Vector3.Lerp(transform.up, gravityDir, RotationSpeed * Time.deltaTime);

            //Gravity force
            //rb.AddForce((-gravityDir * orbit.gravityForce) * rb.mass);

            if (!playerMovement.isGrounded)
            {
                gravity = -gravityDir * orbit.gravityForce;
            }
            else 
            {
                gravity = -gravityDir * 1;
            }
        }
    }
}
