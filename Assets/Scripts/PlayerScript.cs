using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public float walkSpeed = 10f;
    public float jumpSpeed = 10f;
    int layer_mask; //layer for objects that can be collided with/jump off of
    Vector3 downDirection; //used for raycasts to detect if the player should be able to jump
    Rigidbody2D rigidBody; //the RigidBody component of the player
    float maxHeight = 0; //maximum height reachable by jumping before the player falls

    // Start is called before the first frame update
    void Start()
    {
        layer_mask = LayerMask.GetMask("Ground");
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called once per second
    void FixedUpdate()
    {
        //prevent player from falling over
        if (rigidBody.rotation != 0) rigidBody.rotation = 0;
        if (rigidBody.angularVelocity != 0) rigidBody.angularVelocity = 0;

        //player movement
        if (Input.GetKey(KeyCode.A))
		{
			transform.position += Vector3.left * walkSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.position += Vector3.right * walkSpeed * Time.deltaTime;
		}
        if (Input.GetKey(KeyCode.W))
        {
            downDirection = transform.TransformDirection(Vector3.down);
            //if player is close to a ground object, calculate max height
            if (Physics2D.Raycast(transform.position, downDirection, 1.5f, layer_mask))
            {
                maxHeight = transform.position.y + 5f;
            }
            //if player has not reached max height, allow jumping
            if (Physics2D.Raycast(transform.position, downDirection, maxHeight, layer_mask))
            {
                transform.position += Vector3.up * jumpSpeed * Time.deltaTime;
            }
            
        }
    }
}
