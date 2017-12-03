using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

	Text text;

	int currentTime = 0;

	public bool paused = false;
	public bool reset = false;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		InvokeRepeating ("UpdateText", 0, 1);
	}

	public void Reset()
	{
		currentTime = 0;
		reset = false;
	}

	void UpdateText()
	{
		if (reset) {
			Reset ();
		}

		string minutes = Mathf.Floor (currentTime / 60).ToString ("00");
		string seconds = (currentTime % 60).ToString ("00");

		text.text = "Time: " + minutes + ":" + seconds;
		if (!paused)
			currentTime++;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
