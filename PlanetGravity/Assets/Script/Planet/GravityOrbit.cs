using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOrbit : MonoBehaviour
{
    public float gravityForce = 20f;
    public bool fixedDirection;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GravityBehaviour>())
        { 
            other.GetComponent<GravityBehaviour>().gravityOrbit = this.GetComponent<GravityOrbit>();
        }
    }
}
