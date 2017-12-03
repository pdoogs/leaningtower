using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	private GameObject gameOverTextObj;
	//private Text gameOverText;
	private UnityEngine.UI.Image gameOverImage;

	private GameTimer timer;

	private AudioSource audioSource;

	private AudioSource collapseAudioSource;

	// Use this for initialization
	void Start () {
		timer = GameObject.Find ("Time").GetComponent<GameTimer> ();

		audioSource = GameObject.Find ("Audio Source").GetComponent<AudioSource> ();
		collapseAudioSource = GameObject.Find ("CollapseAudioSource").GetComponent<AudioSource> ();

		gameOverImage = GameObject.Find ("GameOverImage").GetComponent<Image> ();
		gameOverImage.enabled = false;

		rb = GetComponent<Rigidbody2D>();
		rb.centerOfMass = rb.centerOfMass + new Vector2(0, 0.5f);

		origPosition = gameObject.transform.position;
		origRotation = gameObject.transform.rotation;

		InvokeRepeating ("ApplyForceRight", forceRightDelay, forceRightInterval);
		InvokeRepeating ("ApplyForceLeft", forceLeftDelay, forceLeftInterval);
	}

	IEnumerator Reset()
	//void Reset()
	{
		collapseAudioSource.Play ();

		// Pause Timer
		timer.paused = true;

		// Pause music
		audioSource.Pause ();

		CancelInvoke ();

		// Reset Tower
		gameObject.transform.position = origPosition + new Vector3(0, 0.2f, 0);
		gameObject.transform.rotation = origRotation;
		forceRight = 0;
		forceLeft = 0;
		rb.Sleep ();

		// Display Game Over Text
		gameOverImage.enabled = true;

		// Pause spawning of persons
		GameObject spawnerObj = GameObject.Find ("Spawner");
		Spawner spawner = spawnerObj.GetComponent<Spawner> ();
		spawner.spawnEnabled = false;

		// Destroy all persons
		foreach (GameObject personObj in GameObject.FindGameObjectsWithTag("Person"))
		{
			Destroy (personObj);
		}

		yield return new WaitForSeconds(5);

		//Time.timeScale = 1;

		// Renable spawning
		spawner.spawnEnabled = false;

		// Hide gameover text
		gameOverImage.enabled = false;

		// Reset and resume timer
		timer.Reset ();
		timer.paused = false;

		// Unpause Music
		audioSource.Play();
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		StartCoroutine ("Reset");
		///Reset();
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
