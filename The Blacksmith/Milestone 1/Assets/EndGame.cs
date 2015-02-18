using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	public GameObject thingy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		thingy.guiText.text = string.Format ("Timer: {0:F1}", Time.time);

		if (Time.time >= 60f) {
			guiText.text = "Game Over";
		}
	}


}
