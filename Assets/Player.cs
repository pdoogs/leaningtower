using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed;             //Floating point variable to store the player's movement speed.

	private Rigidbody2D rb;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

	//bool jumping;

	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb = GetComponent<Rigidbody2D> ();
	}

	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void FixedUpdate()
	{
		rb.AddForce(new Vector2(Input.GetAxis ("Horizontal") * speed, 0));

		/*
		//Store the current horizontal input in the float moveHorizontal.
		float moveHorizontal = Input.GetAxis ("Horizontal") * speed;

		//Store the current vertical input in the float moveVertical.
		float moveVertical = Input.GetAxis ("Vertical") * speed;

		//Use the two store floats to create a new Vector2 variable movement.
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
		//rb2d.AddForce (movement * speed);
		transform.position = transform.position + new Vector3(movement.x, movement.y, 0);
		*/
	}
}
