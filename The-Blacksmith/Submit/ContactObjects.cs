using UnityEngine;
using System.Collections;

public class ContactObjects : MonoBehaviour {

	Color originalColor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		originalColor = other.gameObject.renderer.material.color;
		//if (gameObject.tag == "Player" && other.gameObject.tag == "Barrel") {
			other.gameObject.renderer.material.color = new Color(0,255,225,255);
		//}
	}

	void OnTriggerExit (Collider other) {
		other.gameObject.renderer.material.color = originalColor;
	}
}
