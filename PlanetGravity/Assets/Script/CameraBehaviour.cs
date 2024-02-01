using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float xSpeed = 3.5f;
    private float sensivity = 17f;

    private float minFieldOfViewZ = 0;
    private float maxFieldOfViewZ = 100;

    private Vector3 startPosition;
    private float distance= 10;

    private void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Camera position
        /*Vector3 positionCamera = target.transform.right * 0 + target.transform.up * 11 +
                                   target.transform.forward * -3.85f;
        transform.position = positionCamera;*/
        Vector3 positionCamera = startPosition - target.transform.position;
        transform.position = target.transform.position.normalized * 5;

        // Distance until the target
        Vector3 direction = (target.transform.position - transform.position);
        Vector3 directionNorm = (target.transform.position - transform.position).normalized;

        // Camera 3DMovement 
        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(target.transform.position, transform.up, Input.GetAxis("Mouse X") * xSpeed);
            transform.RotateAround(target.transform.position, transform.right, -Input.GetAxis("Mouse Y") * xSpeed);
        }

        // Camera Zoom    
        /*
        float fieldOfViewInDepth = Camera.main.fieldOfView;
        fieldOfViewInDepth += Input.GetAxis("Mouse ScrollWheel") * -sensivity;
        fieldOfViewInDepth = Mathf.Clamp(fieldOfViewInDepth, minFieldOfViewZ, maxFieldOfViewZ);
        Camera.main.fieldOfView = fieldOfViewInDepth;
        */
        // Rotation
        Quaternion rotateTo = Quaternion.LookRotation(direction,directionNorm);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotateTo, Time.deltaTime * 14);

    }

    private void LateUpdate()
    {

    }
}
