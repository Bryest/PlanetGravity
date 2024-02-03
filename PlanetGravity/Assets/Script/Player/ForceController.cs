using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] GravityBehaviour gravityBehaviour;
    PlayerMovement playerMovement;
    
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        gravityBehaviour = GetComponent<GravityBehaviour>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 force = playerMovement.movementForce + gravityBehaviour.gravity + playerMovement.jumpForce;
     
        rb.AddForce(force, ForceMode.Force);
    }
}
