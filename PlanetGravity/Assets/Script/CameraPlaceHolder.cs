using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlaceHolder : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject planet;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {
        // Position
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.1f);

        Vector3 gravityDirection = (transform.position - planet.transform.position).normalized;

        // Rotation
        Quaternion toRotation = Quaternion.FromToRotation(transform.up, gravityDirection) * transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation,toRotation,0.1f);
    }
}
