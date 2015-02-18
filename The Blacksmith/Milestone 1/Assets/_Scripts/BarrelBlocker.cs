using UnityEngine;
using System.Collections;

public class BarrelBlocker : MonoBehaviour {

	public float speed = 1f;

	private bool forward = true;
	private Rigidbody barrelRigidBody;
	private Vector3 movement;

	// Use this for initialization
	void Awake () {
		barrelRigidBody = GetComponent <Rigidbody> ();
		movement = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float x = 0;

		if (transform.position.x >= -10 && transform.position.x <= -5) {
			if (forward) {
				x = 1;
				Move (x);
			}
			if (!forward) {
				x = -1;
				Move (x);
			}
			if (transform.position.x < -9.9)
				forward = false;
			if (transform.position.x > -5.1)
				forward = true;
		}
	}

	void Move (float x) {
		movement.Set (x, 0f, 0f);
		movement = movement.normalized * speed * Time.deltaTime;
		barrelRigidBody.MovePosition(movement);
	}
}
