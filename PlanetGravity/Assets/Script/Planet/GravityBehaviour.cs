using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBehaviour : MonoBehaviour
{
    public GravityOrbit gravityOrbit;
    public float gravityForce;
    private Rigidbody rb; 
    
    [SerializeField] private float RotationSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If is in the planet orbit
        if(gravityOrbit)
        {
            Vector3 gravityDir;
            if(gravityOrbit.fixedDirection)
            {
                gravityDir = gravityOrbit.transform.up;
            }
            else
            {
                gravityDir = (transform.position - gravityOrbit.transform.position).normalized;
            }

            // Rotate the object with the surface ot the gravity
            transform.up = Vector3.Lerp(transform.up, gravityDir, RotationSpeed * Time.deltaTime);
            
            //Gravity force
            rb.AddForce((-gravityDir * gravityOrbit.gravityForce) * rb.mass);
        }
    }
}
