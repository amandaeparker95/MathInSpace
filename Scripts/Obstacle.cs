using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector3 moveDir; //direction of the obstacle
    public float moveSpeed; //movement speed of the obstacle

    private float aliveTime = 8.0f; // time before obstacle is destroyed

    // Start is called before the first frame update
    void Start()
    {
       Destroy(gameObject, aliveTime); //this will destroy the object once aliveTime is over
    }

    // Update is called once per frame
    void Update() //basically moves and rotates the obstacle across the screen
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime; //position of obstacle in whatever direction over a certain period of time
        transform.Rotate(Vector3.back * moveDir.x * (moveSpeed * 20) * Time.deltaTime); //obstacle rotation
    }
}
