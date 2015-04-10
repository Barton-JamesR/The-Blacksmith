using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 6f;

	Vector3 movement;
	Rigidbody playerRigidBody;
	int floorMask;
	float camRayLength = 100f;


	void Awake() {
		floorMask = LayerMask.GetMask ("Floor");
		playerRigidBody = GetComponent <Rigidbody> ();
	}

	void FixedUpdate () {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		if (Input.GetKeyDown (KeyCode.LeftShift)) { // hold leftshift to run
			h = h * 1.5f;
			v = v * 1.5f;
		}

		Move (h,v);
		Turning ();
	}

	void Move (float h, float v) {
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidBody.MovePosition(movement + transform.position);
	}

	void Turning () {
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitter;

		if (Physics.Raycast (camRay, out hitter, camRayLength)) {
			Vector3 playerToMouse = hitter.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidBody.MoveRotation(newRotation);
		}
	}
}
