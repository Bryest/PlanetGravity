using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float gravityForce = 20f;
    public bool fixedDirection;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GravityBehaviour>())
        { 
            other.GetComponent<GravityBehaviour>().orbit = this.GetComponent<Orbit>();
        }
    }
}
