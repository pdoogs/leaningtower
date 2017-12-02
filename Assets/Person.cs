using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {

	private GameObject target;
	public float velocity;
	public float walkDelay;
	private float dir;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player");
		rb = GetComponent<Rigidbody2D> ();
		dir = Mathf.Sign ((target.transform.position - transform.position).x);
		InvokeRepeating ("Bump", walkDelay, walkDelay);
	}
	
	void Bump()
	{
		//rb.AddForce (new Vector2 (dir * velocity, 0));
		rb.AddForceAtPosition(new Vector2 (dir * velocity, 0), transform.position);
	}
				

	// Update is called once per frame
	void Update () {
		

	}
}
