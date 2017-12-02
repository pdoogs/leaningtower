using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject enemy;                // The enemy prefab to be spawned.
	public float spawnTime = 3f;            // How long between each spawn.
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Spawn()
	{
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		GameObject obj = Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		if (spawnPointIndex == 0) {
			SpriteRenderer renderer = obj.GetComponent<SpriteRenderer> ();
			renderer.flipX = true;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
