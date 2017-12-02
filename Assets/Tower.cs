using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
	public Rigidbody2D rb;

	Vector3 origPosition;
	Quaternion origRotation;

	public float personForce = 0;
	public float forceRightInterval = 3;
	public float forceLeftInterval = 3;
	public float forceRightDelay = 0;
	public float forceLeftDelay = 1.5f;

	private float forceRight = 0;
	private float forceLeft = 0;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		rb.centerOfMass = rb.centerOfMass + new Vector2(0, 0.5f);

		origPosition = gameObject.transform.position;
		origRotation = gameObject.transform.rotation;

		InvokeRepeating ("ApplyForceRight", forceRightDelay, forceRightInterval);
		InvokeRepeating ("ApplyForceLeft", forceLeftDelay, forceLeftInterval);
	}

	void Reset()
	{
		CancelInvoke ();
		gameObject.transform.position = origPosition + new Vector3(0, 0.2f, 0);
		gameObject.transform.rotation = origRotation;
		forceRight = 0;
		forceLeft = 0;
		rb.Sleep ();
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Reset();
	}

	void ApplyForceRight()
	{
		rb.AddForce (new Vector2 (forceRight, 0));
	}

	void ApplyForceLeft()
	{
		rb.AddForce (new Vector2 (forceLeft, 0));
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Person") {
			Vector3 personPos = collision.gameObject.transform.position;
			Vector3 diff = personPos - gameObject.transform.position;
			if (diff.x > 0) {
				forceRight += personForce;
				ApplyForceRight ();
			} else {
				forceLeft += personForce;
				ApplyForceLeft ();
			}

			Destroy (collision.gameObject);
		}
	}
}
