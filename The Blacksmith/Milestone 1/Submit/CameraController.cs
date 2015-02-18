using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public GameObject player;

	private Vector3 offsetPosition;
	private Quaternion offsetRotation;

	// Use this for initialization
	void Start () {
		offsetPosition = player.transform.position;
		offsetPosition.y = offsetPosition.y + 0.7f;
		offsetRotation = player.transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.rotation = player.transform.rotation;
		transform.position = player.transform.position + offsetPosition; 
	}
}
